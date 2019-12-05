using System.Collections.Generic;
using GrowthTrigal.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GrowthTrigal.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTypes();

        IEnumerable<SelectListItem> GetComboVarietyNames();

        IEnumerable<SelectListItem> GetComboBedNames();
        IEnumerable<SelectListItem> GetComboMeasurers();
    }

   
}