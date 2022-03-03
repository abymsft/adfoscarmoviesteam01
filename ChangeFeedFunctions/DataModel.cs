using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Converters;
using System.Collections;


namespace Movies.Function
{
    public class MatView
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public double Count { get; set; }
        //public double TotalSales { get; set; }

        public MatView()
        {
            Id = Guid.NewGuid().ToString();
        }
    }


    public class Orderhub
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public int Count { get; set; }
        //public double TotalSales { get; set; }

        public Orderhub()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Region { get; set; }
        public string OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string SMSOptIn { get; set; }
        public string SMSStatus { get; set; }
        public string Email { get; set; }
        public string ReceiptUrl { get; set; }
        public string Total { get; set; }
        public string PaymentTransactionId { get; set; }
        public string HasBeenShipped { get; set; }
        public List<OrderDetails> Details { get; set; }
    }

    public class OrderDetails
    {
        public string Email { get; set; }
        public string ProductId { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }

    }

    /*public class SubDetails
    {
        
    }*/
}