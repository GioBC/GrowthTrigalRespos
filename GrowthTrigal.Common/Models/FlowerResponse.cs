using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SQLite;
using System;

namespace GrowthTrigal.Common.Models
{
    public class FlowerResponse
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Type { get; set; }

        public string VarietyName { get; set; }

        public string BedName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeasurementResponse> Measurements { get; set; }

        [ForeignKey(typeof(HomeResponse))]
        public int Home_Id { get; set; }

        [ManyToOne]
        public HomeResponse Home { get; set; }

        public DateTime flowerDate { get; set; }

        //public override int GetHashCode()
        //{
        //    return Flower_Id;
        //}


    }
}
