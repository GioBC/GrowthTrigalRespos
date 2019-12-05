using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace GrowthTrigal.Common.Models
{
    public class HomeResponse
    {
        //[PrimaryKey]
        public int Id { get; set; }

        public string BlockNumber { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public ICollection<FlowerResponse> Flowers { get; set; }


        //[OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        //public List<FlowerResponse> Flowers { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}
    }
}
