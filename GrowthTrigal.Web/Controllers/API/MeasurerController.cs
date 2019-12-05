using GrowthTrigal.Common.Models;
using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurerController: ControllerBase
    {
        private readonly DataContext _dataContext;

        public MeasurerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetMeasurer")]
        public  IEnumerable<Measurer> GetMeasurer()
        {

            return _dataContext.Measurers;
             
                
                //Users.OrderBy(pt => pt.Document);
        }
    }
}

