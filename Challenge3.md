
 1.   Retrieve list of top 10 movies by popularity (home page): ~500 requests/second
        -- VoteAverage (Items) Top 10 movies in 
        -- 3.24 RUs : * 500 = 1620 RUs
        -- SELECT top 10 * FROM c order by c.VoteAverage desc
  2.  Retrieve list of last 10 movies by release date: ~75 requests/second
        -- ReleaseDate (Items) Top 10  desc '
        -- SELECT top 10 * FROM c order by c.ReleaseDate desc
        -- 3.28 RUs
   3. Page through lists of movies: ~30 requests/second
        -- select top 50 * from Items
        -- 3.46 RUs
   4. Retrieve list of categories: ~500 requests/second
        -- Category container 
        -- 2.6 RUs
   5.  Filter movies by category: ~200 requests/second
        -- select  * from c where c.CategoryId=9648
        -- 3.74 RUs
   6. Retrieve details for a specific movie: ~160 requests/second
        -- select  * from c where c.ProductName="Random Lake"
        -- 2.83 RUs
   7. Retrieve orders with details showing products with quantities: ~10 requests/second
        -- SELECT * FROM c where c.OrderId = 50 (OrderDetails)
        -- 2.99 RUs
        -- Only Ids are visible (can't join)
   8. Add a movie to the shopping cart: ~5 requests/second
   9. Complete a purchase transaction: ~2 requests/second


Embed:



Reference:

Orders : Orderdetails 1:N
	Orders : Item/Product 1:1
Item/Product : Category 1:1
Item/Product : ItemAggregate 1:1
Category: User 1:1
User: Event 1:N (Missing OrderId and ItemId)
Carts
    CartId (PerUser) : CartItemId : 1:N
    Cart:Items - 1:N