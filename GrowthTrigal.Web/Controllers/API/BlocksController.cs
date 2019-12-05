using GrowthTrigal.Common.Models;
using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlocksController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BlocksController(DataContext context)
        {
            _dataContext = context;
        }


        // GET: api/Blocks
        [HttpGet]
        public IEnumerable<Home> GetHomes()
        {
            return _dataContext.Homes;
        }

       

        // GET: api/Blocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHome([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var home = await _dataContext.Homes.FindAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return Ok(home);
        }

        // PUT: api/Blocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome([FromRoute] int id, [FromBody] Home home)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != home.Id)
            {
                return BadRequest();
            }

            _dataContext.Entry(home).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blocks
        [HttpPost]
        public async Task<IActionResult> PostHome([FromBody] Home home)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Homes.Add(home);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction("GetHome", new { id = home.Id }, home);
        }

        // DELETE: api/Blocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var home = await _dataContext.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            _dataContext.Homes.Remove(home);
            await _dataContext.SaveChangesAsync();

            return Ok(home);
        }

        private bool HomeExists(int id)
        {
            return _dataContext.Homes.Any(e => e.Id == id);
        }
    }
}




//[HttpPost]
//[Route("GetHomeByEmail")]
//public async Task<IActionResult> GetHomeByEmailAsync(EmailRequest request)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest();
//    }


//    var home = await _dataContext.Homes
//        .Include(me => me.Flowers)
//        .Include(me => me.Measurements)
//        .ThenInclude(mea => mea.Measurer)
//        .ThenInclude(measu => measu.User)
//        .FirstOrDefaultAsync(measu => measu.UserName.ToLower().Equals(request.Email.ToLower()));

//    //var home = await _dataContext.Homes
//    //    .Include(h => h.Flowers)
//    //    .ToListAsync();


//    //var flower = await _dataContext.Flowers
//    //    .Include(f => f.Home)
//    //    .Include(f => f.Measurements)
//    //    .ToListAsync();



//    var response = new MeasurerResponse
//    {

//        Id = measurer.Id,
//        FirstName = measurer.User.FirstName,
//        LastName = measurer.User.LastName,
//        Document = measurer.User.Document,
//        Email = measurer.User.Email,
//        PhoneNumber = measurer.User.PhoneNumber,

//        Measurements = measurer.Measurements?.Select(mea => new MeasurementResponse
//        {
//            Measure = mea.Measure,
//            MeasureDate = mea.MeasureDate,
//            Id = mea.Id,
//            //Measurer = ToMeasurerResponse(mea.Measurer)



//        }).ToList(),

//        Homes = measurer.Homes?.Select(h => new HomeResponse
//        {
//            BlockNumber = h.BlockNumber,
//            Id = h.Id


//        }).ToList(),

//        Flowers = measurer.Flowers?.Select(f => new FlowerResponse
//        {
//            Id = f.Id,
//            Type = f.Type,
//            VarietyName = f.VarietyName,
//            BedName = f.BedName
//        }).ToList()



//    };

//    return Ok(response);
//}



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