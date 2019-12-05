using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class UPResponse
    {
        //[PrimaryKey]
        public int Id { get; set; }

        public string AliasFarm { get; set; } 

        public string FarmName { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public ICollection<HomeResponse> Homes { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}
    }
}
