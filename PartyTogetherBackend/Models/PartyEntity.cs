using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace PartyTogetherBackend.Models
{
    public class PartyEntity : TableEntity
    {
        public PartyEntity(string class_name, string add_time)
        {
            this.PartitionKey = class_name;
            this.RowKey = add_time;
        }

        public PartyEntity() { }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Date { set; get; }

        public Boolean Will_Attend { set; get; }

    }
}