using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GrowthTrigal.Common.Models
{
    public class MeasurerResponse
    {
        //[PrimaryKey]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

    }
}
