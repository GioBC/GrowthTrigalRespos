using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class MeasurementRequest
    {

        public int Id { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public DateTime MeasureDate { get; set; }

        [Required]
        public int FlowerId { get; set; }

        //[Required]
        //public int HomeId { get; set; }

        //[Required]
        //public int UpId { get; set; }
    }
}
