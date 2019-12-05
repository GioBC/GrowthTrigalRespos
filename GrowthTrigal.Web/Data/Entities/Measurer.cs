using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowthTrigal.Web.Data.Entities
{
    public class Measurer
    {

        public int Id { get; set; }

        public User User { get; set; }
       

        public ICollection<Measurement> Measurements { get; set; }

        //public ICollection<Flower> Flowers { get; set; }
        
        //public ICollection<Home> Homes { get; set; }

    }
}
