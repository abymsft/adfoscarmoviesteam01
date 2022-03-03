using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Shared;

namespace Movies.Function
{
    public static class mviewCFFunction
    {
        private static readonly string _endpointUrl = "https://oscarmoviesteam01.documents.azure.com:443/";
        private static readonly string _primaryKey = "6ZwbdWbkmuZYGZeH6wnoj15XYqAXaOZAupx4CG4m8reVmlWpo5fWtJxhRM8fqVC6hFo6Difdq98QFFHYERsnhg==";
        private static readonly string _databaseId = "Moviesver2";
        private static readonly string _containerId = "Matview";
        private static CosmosClient cosmosClient = new CosmosClient(_endpointUrl, _primaryKey);
        
        [FunctionName("mviewCFFunction")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "Moviesver2",
            collectionName: "Orderhub",
            ConnectionStringSetting = "DBConnection",
            CreateLeaseCollectionIfNotExists = true,
            LeaseCollectionName = "materializedViewLeases")]IReadOnlyList<Document> input,
             [CosmosDB(
                databaseName: "Moviesver2",
                collectionName: "Matview",
                ConnectionStringSetting = "DBConnection")] DocumentClient documentClient, ILogger log)
        {
            double result;
            //todo - Next steps go here
            var tasks = new List<Task>();
            var db = cosmosClient.GetDatabase(_databaseId);
            var container = db.GetContainer(_containerId);
            if (input != null && input.Count > 0)
            {
                
                var stateDict = new List<OrderDetails>();
                
                OrderDetails itemObj = new OrderDetails();
                foreach (var doc in input)
                {
                    var action = JsonConvert.DeserializeObject<Orderhub>(doc.ToString());

                    /*Order Details*/
                    foreach (var item in action.Details)
                    {
                        OrderDetails _itemObj = new OrderDetails();
                        _itemObj.ProductId = item.ProductId;
                        _itemObj.Quantity = item.Quantity;
                        stateDict.Add(_itemObj);

                    }
                }
                foreach (var objItem in stateDict)
                {
                    try
                    {

                        var docUri = UriFactory.CreateDocumentUri(_databaseId, _containerId, objItem.ProductId.ToString());
                        Document document = await documentClient.ReadDocumentAsync<Document>(docUri, new Microsoft.Azure.Documents.Client.RequestOptions { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(objItem.ProductId.ToString()) });
                        MatView docmo = (dynamic)document;
                        Double.TryParse(objItem.Quantity, out result);
                        docmo.Count += result;
                        await documentClient.ReplaceDocumentAsync(document.SelfLink, docmo);


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception Error" + e.ToString());
                        var productDocUri = UriFactory.CreateDocumentUri(_databaseId, _containerId, objItem.ProductId.ToString());
                        log.LogInformation("Item doesn't exist; creating it now...");
                        Double.TryParse(objItem.Quantity, out result);
                        MatView docmo = new MatView() { ProductId = objItem.ProductId.ToString(), Count = result };

                        //Lookup the ID and resolve the name once - add it to the MO object too.  //var productItemResult = await documentClient.ReadDocumentAsync<Document>(productDocUri, new RequestOptions { PartitionKey = new PartitionKey(orderDetail.ProductId.ToString()) });  //ProductObject productObject = (dynamic)productItemResult;  MaterialObject docmo = new MaterialObject() { id = orderDetail.ProductId.ToString(), Quantity = orderDetail.Quantity, ProductName = "lookedUpProductName" };
                        await documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _containerId), docmo);
                    }
                    log.LogInformation("Upserting materialized view document");
                    tasks.Add(container.UpsertItemAsync(objItem, new Microsoft.Azure.Cosmos.PartitionKey(objItem.ProductId)));

                }
            }
                
                
                
                

                await Task.WhenAll(tasks);
            }
    }
}
