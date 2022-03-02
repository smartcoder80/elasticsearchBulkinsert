using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace BulkOperationDemo
{
    public class DbHelper
    {
        
        public static List<Product> GetProductCatalog()
        {
            List<Product> result;
            using (var reader = new StreamReader("path\\to\\file.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                result = csv.GetRecords<Product>().ToList();
            }

            return result;
        }
       
        private static List<KeyWord> GetKeyWordList(string csvKeyWordString)
        {
            List<KeyWord> keyWordObjList = new List<KeyWord>();
            if (!string.IsNullOrEmpty(csvKeyWordString))
            {
                List<string> keyWordStringArray = csvKeyWordString.Split(',').ToList();
                foreach (string keywordString in keyWordStringArray)
                {
                    KeyWord KeyWord = new KeyWord()
                    {
                        input = new List<string>() { keywordString }
                    };
                    keyWordObjList.Add(KeyWord);
                }
            }
            return keyWordObjList;
        }
    }
}
