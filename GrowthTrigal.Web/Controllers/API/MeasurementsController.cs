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

    [Route("api/[Controller]")]
    [ApiController]
    public class MeasurementsController: ControllerBase
    {

      
        private readonly DataContext _dataContext;

        public MeasurementsController(DataContext dataContex)
        {
            
           _dataContext = dataContex;
        }

        

        [HttpPost]
        public async Task<IActionResult> PostMeasurement([FromBody] MeasurementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //var up = await _dataContext.UPs.FindAsync(request.UpId);
            //if (up == null)
            //{
            //    return BadRequest("Not valid up.");
            //}

            //var home = await _dataContext.Homes.FindAsync(request.HomeId);
            //if (home == null)
            //{
            //    return BadRequest("Not valid Home.");
            //}

            var flower = await _dataContext.Flowers.FindAsync(request.FlowerId);
            if (flower == null)
            {
                return BadRequest("Not valid flower.");
            }

         
            var measurement = new Measurement
            {
                Measure= request.Measure,
                MeasureDate=request.MeasureDate,
                Flower=flower
 
            };

            _dataContext.Measurements.Add(measurement);
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(
            [FromRoute] int id,
            [FromBody] MeasurementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldMeasurement = await _dataContext.Measurements.FindAsync(request.Id);
            if (oldMeasurement == null)
            {
                return BadRequest("Measurements doesn't exists.");
            }

            oldMeasurement.Measure = request.Measure;
            oldMeasurement.MeasureDate= request.MeasureDate;

            _dataContext.Measurements.Update(oldMeasurement);
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }





    }
}
