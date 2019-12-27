

using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowthTrigal.Common.Models
{
    public class MeasurerResponse
    {
        [PrimaryKey]
        public int Id { get; set; }

        [ManyToOne]
        public UserResponse User { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeasurementResponse> Measurements { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

    }
}
