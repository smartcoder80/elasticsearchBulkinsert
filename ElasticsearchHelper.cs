using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkOperationDemo
{
    public static class ElasticsearchHelper
    {
        public static ElasticClient GetESClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;
            var nodes = new Uri[] {
                new Uri("http://da6591de6c48.ngrok.io") //Provide ES cluster addresses)
            };
            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;
        }

        public static void CreateBulkDocument(ElasticClient elasticClient, string indexName, List<Product> products)
        {
            var bulkAllObservable = elasticClient.BulkAll(products, b => b
    .Index("myindex")
    // how long to wait between retries
    .BackOffTime("30s")
    // how many retries are attempted if a failure occurs
    .BackOffRetries(2)
    // refresh the index once the bulk operation completes
    .RefreshOnCompleted()
    // how many concurrent bulk requests to make
    .MaxDegreeOfParallelism(100)
    // number of items per bulk request
    .Size(1000)
)
// Perform the indexing, waiting up to 15 minutes. 
// Whilst the BulkAll calls are asynchronous this is a blocking operation
.Wait(TimeSpan.FromMinutes(15), next =>
{
    // do something on each response e.g. write number of batches indexed to console
});

        }

    }
}
