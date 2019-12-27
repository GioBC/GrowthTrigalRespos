using GrowthTrigal.Common.Models;
using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HomesController : ControllerBase
    {
        private readonly DataContext _dataContext;
      

        public HomesController(DataContext dataContext)
        {
            _dataContext = dataContext;
           
        }



        [HttpPost]
        [Route("GetUPByEmail")]
        public async Task<IActionResult> GetUPByEmailAsync(UpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var up = await _dataContext.UPs
                .Include(u => u.Homes)
                .FirstOrDefaultAsync(u => u.AliasFarm.Equals(request.AliasFarm));


            var home = await _dataContext.Homes
                 .OrderBy(h => h.BlockNumber)
                .Include(h => h.Flowers)
                .ToListAsync();


            var flower = await _dataContext.Flowers
                .Include(f => f.Home)
                .Include(f => f.Measurements)
                .ThenInclude(mea=> mea.Measurer)
                .ThenInclude(me=>me.User)
                .ToListAsync();


            var response = new UPResponse
            {

                Id = up.Id,
                AliasFarm = up.AliasFarm,
                FarmName=up.FarmName,
                

                Homes = up.Homes?.Select(h => new HomeResponse
                {
                    Id = h.Id,
                    BlockNumber = h.BlockNumber,
                    Flowers=h.Flowers?.Select(f=> new FlowerResponse
                    {
                        Id = f.Id,
                        Type = f.Type,
                        VarietyName = f.VarietyName,
                        BedName = f.BedName,
                        Measurements= f.Measurements?.Select(mea => new MeasurementResponse
                        {
                            Measure = mea.Measure,
                            MeasureDate = mea.MeasureDate,
                            Id = mea.Id
                            //Measurer= ToMeasurerResponse(mea.Measurer)
 

                        }).ToList(),
                    }).ToList(),
                }).ToList(),

               

            };

            return Ok(response);
        }


        

        //private MeasurerResponse ToMeasurerResponse(Measurer measurer)
        //{
        //    return new MeasurerResponse
        //    {
        //        Document = measurer.User.Document,
        //        Email = measurer.User.Email,
        //        FirstName = measurer.User.FirstName,
        //        LastName = measurer.User.LastName,
        //        PhoneNumber = measurer.User.PhoneNumber

        //    };
        //}

        [HttpGet("GetLastMeasuremetByFlowerId/{id}")]
        public async Task<IActionResult> GetLastMeasuremetByFlowerId([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flower = await _dataContext.Flowers
                .Include(f => f.Measurements)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flower==null)
            {
                return NotFound();
            }

            var measurement = flower.Measurements.LastOrDefault();
            var response = new MeasurementResponse
            {
                Measure = measurement.Measure,
                MeasureDate = measurement.MeasureDate,
                Id = measurement.Id

            };
            return Ok(response);
        }



    }
}




