using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Data.Entities
{
    public class Measurement
    {

        public int Id { get; set; }

        //[StringLength(4)]
        [MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Measure { get; set; }

        [Display(Name = "Measure Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Measure Date Local")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime MeasureDateLocal => MeasureDate.ToLocalTime();

        public Flower Flower { get; set; }

        public Measurer Measurer { get; set; }


       
    }
}
 