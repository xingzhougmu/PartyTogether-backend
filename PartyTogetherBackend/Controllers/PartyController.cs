using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PartyTogetherBackend.Models;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using System.Configuration;

namespace PartyTogetherBackend.Controllers
{
    public class PartyController : ApiController
    {
        // GET: api/Party
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Party/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Party
        public string Post(PartyEntity party)
        {
            try
            {
                // Parse the connection string and return a reference to the storage account.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    ConfigurationManager.AppSettings["StorageConnectionString"]);

                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Retrieve a reference to the table.
                CloudTable table = tableClient.GetTableReference("partydate");

                // Create the table if it doesn't exist.
                table.CreateIfNotExists();

                /*
                PartyEntity party = new PartyEntity("03015001", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                party.Name = "周星";
                party.Phone = "15721132919";
                party.Date = "2017-07-12";
                // party.AddDateTime = DateTime.Now;
                */
                // in case party is null for malformated request
                if (party == null)
                {
                    party = new PartyEntity("03015001", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
                else
                {
                    party.PartitionKey = "03015001";
                    party.RowKey = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }

                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(party);

                // Execute the insert operation.
                table.Execute(insertOperation);

                return "created";
            }
            catch (StorageException se)
            {
                return "failed";
            }

        }

        // PUT: api/Party/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Party/5
        public void Delete(int id)
        {
        }
    }
}
