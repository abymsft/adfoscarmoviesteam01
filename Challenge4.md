
    1. The data generator successfully sends orders into Event Hubs, and you create a process to consume the events and write the data into your database.

    The team’s event sourcing should produce the following results:

       2.  Create and populate materialized views for reporting, using one or both of the following criteria:
          3.  Top 10 most popular movies purchased of all time. 
          4. Most popular movie categories.

        Save aggregate data over time windows (x per hour, day, etc.), using one or both of the following criteria:
            Count of orders placed for this hour.
            Summary of activity (number of orders and total revenue) for this day so far.
        Create at least one dashboard or report that displays the materialized views created in this challenge.
        Implement some form of caching of categories. You do not have to modify the web application in any way to show this.

        Implement dynamic cache invalidation when changes are made to movie categories. Cache should be updated when movie categories are inserted, updated, or deleted. This can be tested by attendees by executing queries against the cache, update one or more category items, then re-execute their cache query to ensure the cache was refreshed.



# Contoso would like to create report style views summarizing the activity of users on the website. As new events arrive they would like to see some of the statistics update in near real time. Other statistics they want to update during specific windows, like hourly and daily

1. Examples of these statistics include:

    Count of orders placed for this hour.
    Summary of activity (number of orders and total revenue) for this day so far.
    Top 10 most popular movies purchased of all time.
    Most popular movie categories.

# Additionally, Contoso would like to ensure they have a layer of caching that reduces reads against the data store when the commonly retrieved movie categories are accessed. 


## In their current deployment, they had explored doing this but got stuck figuring out how they could invalidate the cache when the underlying records had been changed, and do that in response to the change. They were particularly concerned with ensuring that the cache would be properly adjusted, no matter how the change to the categories was made (e.g., through an admin website or a direct edit against the database).





# DataGen events to EventHub (Data Gen)

# EventHub to CosmosDB (Stream Analytics)

# Aggregate data
## Count of orders placed for this hour. (Real time report of above stats)

# Create and populate materialized views
## Create at least one dashboard or report 

# Implement some form of caching of categories.
## Implement dynamic cache invalidation when changes are made to movie categories.