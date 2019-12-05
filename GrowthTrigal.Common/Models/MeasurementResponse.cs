using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace GrowthTrigal.Common.Models
{
    public class MeasurementResponse
    {

        //[PrimaryKey]
        public int Id { get; set; }

       public string Measure { get; set; }

       public DateTime MeasureDate { get; set; }

        public DateTime MeasureDateLocal => MeasureDate.ToLocalTime();

        //[ManyToOne]
        public MeasurerResponse Measurer { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

    }
}
