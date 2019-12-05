using GrowthTrigal.Web.Data.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GrowthTrigal.Web.Models
{
    public class MeasurementsViewModel : Measurement
    {
        public int FlowerId { get; set; }

        public int HomeId{ get; set; }

        public int UpId { get; set; }

        //public int MeasurerId { get; set; }

        //public int UPId { get; set; }

        //public int MeasurerId { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Measurer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Measurer.")]
        public int MeasurerId { get; set; }

        public IEnumerable<SelectListItem> Measurers { get; set; }

    }
}
