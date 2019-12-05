using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Data.Entities
{
    public class Flower
    {

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage="The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Flower Type")]
        public string Type { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Variety Name")]
        public string VarietyName { get; set; }


        [MaxLength(4, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Bed Name")]
        public string BedName { get; set; }

        public string FullVariety => $"{Type} - {VarietyName}";
        
        public ICollection<Measurement> Measurements { get; set; }

       
        public Home Home { get; set; }

        //public UP UP { get; set; }
        //public ICollection<Measurer> Measurers { get; set; }
        //public Measurer Measurer { get; set; }
    }

}
