using GrowthTrigal.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Models
{
    public class HomeViewModel: Flower
    {

        public int HomeId { get; set; }

        public int MeasurerId { get; set; }

        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[Display(Name = "Flower Type")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a type.")]
        //public int TypeId { get; set; }

        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[Display(Name = "Variety Name")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a variety.")]
        //public int VarietyNameId { get; set; }

        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[Display(Name = "Bed Name")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a variety.")]
        //public string BedNameId { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> VarietyNames { get; set; }

        public IEnumerable<SelectListItem> BedNames { get; set; }


    }
}
