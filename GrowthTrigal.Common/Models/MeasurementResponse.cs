using SQLiteNetExtensions.Attributes;
using System;
using SQLite;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowthTrigal.Common.Models
{
    public class MeasurementResponse
    {

        [PrimaryKey]
        public int Id { get; set; }

       public string Measure { get; set; }

       public DateTime MeasureDate { get; set; }

        public DateTime MeasureDateLocal => MeasureDate.ToLocalTime();

        [ManyToOne]
        public MeasurerResponse Measurer { get; set; }

        [ManyToOne]
        public FlowerResponse Flower { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

    }
}
