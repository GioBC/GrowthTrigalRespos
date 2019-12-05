using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace GrowthTrigal.Common.Models
{
    public class FlowerResponse
    {
        //[PrimaryKey]
        public int Id { get; set; }

        public string Type { get; set; }

        public string VarietyName { get; set; }


        public string BedName { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public ICollection<MeasurementResponse> Measurements { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

    }
}
