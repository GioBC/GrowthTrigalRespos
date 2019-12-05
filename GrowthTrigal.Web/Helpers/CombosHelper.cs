using GrowthTrigal.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;


namespace GrowthTrigal.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public IEnumerable<SelectListItem> GetComboTypes()
        {
            var list = _dataContext.Flowers.Select(fl => new SelectListItem
            {
                Text = fl.Type,
                Value = $"{fl.Id}"

            })
                .OrderBy(fl => fl.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a type...",
                Value = "0"
            });
            return list;


        }


        public IEnumerable<SelectListItem> GetComboVarietyNames()
        {
            var list = _dataContext.Flowers.Select(fl => new SelectListItem
            {
                Text = fl.VarietyName,
                Value = $"{fl.Id}"

            })
                .OrderBy(fl => fl.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a Variety...",
                Value = "0"
            });
            return list;

        }

        public IEnumerable<SelectListItem> GetComboBedNames()
        {
            var list = _dataContext.Flowers.Select(fl => new SelectListItem
            {
                Text = fl.BedName,
                Value = $"{fl.Id}"

            })
                .OrderBy(fl => fl.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a BedName...",
                Value = "0"
            });
            return list;


        }


        public IEnumerable<SelectListItem> GetComboHomes()
        {
            var list = _dataContext.Homes.Select(ho => new SelectListItem
            {
                Text = $"{ ho.BlockNumber}",
                Value = $"{ho.BlockNumber}"

            })
              .OrderBy(ho => ho.Text)
              .ToList();
         
            list.Insert(0, new SelectListItem
            {
                Text = "Select a Block...",
                Value = "0"
            });
            return list;


        }



        public IEnumerable<SelectListItem> GetComboMeasurements()
        {
            var list = _dataContext.Measurements.Select(me => new SelectListItem
            {
                Text = $"{ me.Measure}",
                Value = $"{me.Id}"

            })
              .OrderBy(me => me.Text)
              .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a Measure...",
                Value = "0"
            });
            return list;


        }

        public IEnumerable<SelectListItem> GetComboMeasurers()
        {
            var list = _dataContext.Measurers.Select(me => new SelectListItem
            {
                Text =  me.User.Document,
                Value = $"{me.Id}"

            })
              .OrderBy(me => me.Text)
              .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a Measurer...",
                Value = "0"
            });
            return list;
        }
    }

}
