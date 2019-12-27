using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace GrowthTrigal.Common.Models
{
    public class HomeResponse
    {
        [PrimaryKey]
        public int Id { get; set; }


        public string BlockNumber { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<FlowerResponse> Flowers { get; set; }

        [ForeignKey(typeof(UPResponse))]
        public int UP_Id { get; set; }

        [ManyToOne]
        public UPResponse UP { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
