using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkOperationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string INDEX_NAME = "catalog";
            //1. Get Product list from the database.
            List<Product> products = DbHelper.GetProductCatalog();

            //2.Connect to Elastic Search.
            ElasticClient elasticClient = ElasticsearchHelper.GetESClient();
            if (products!=null && products.Count > 0)
            {
                //2. Insert in to Elastic search
                ElasticsearchHelper.CreateBulkDocument(elasticClient, INDEX_NAME, products);
            }
            Console.ReadKey();
        }
    }
}
