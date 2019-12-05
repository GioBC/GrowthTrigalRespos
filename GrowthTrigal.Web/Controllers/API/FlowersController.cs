using GrowthTrigal.Common.Models;
using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
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
    public class FlowersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public FlowersController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: api/Flowers
        [HttpGet]
        public IEnumerable<Flower> GetFlowers()
        {
            return _dataContext.Flowers
                .OrderBy(f => f.BedName)
                .Include(f => f.Measurements);


        }

        [HttpPost]
        [Route("GetFlowerBy")]
        public async Task<IActionResult> GetFlowerAsync(FlowerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var flower = await _dataContext.Flowers
                .Include(f => f.Measurements)
                .FirstOrDefaultAsync(f => f.Id.Equals(request.Id));

            var response = new FlowerResponse
            {

                Id = flower.Id,
                Type = flower.Type,
                VarietyName = flower.VarietyName,
                BedName = flower.BedName,
                Measurements = flower.Measurements?.Select(mea => new MeasurementResponse
                {
                    Measure = mea.Measure,
                    MeasureDate = mea.MeasureDate,
                    Id = mea.Id
                }).ToList(),

            };

            return Ok(response);
        }


        // GET: api/Flowers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlower([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flower = await _dataContext.Flowers.FindAsync(id);

            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        // PUT: api/Flowers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlower([FromRoute] int id, [FromBody] Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flower.Id)
            {
                return BadRequest();
            }

            _dataContext.Entry(flower).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(id))
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

        // POST: api/Flowers
        [HttpPost]
        public async Task<IActionResult> PostFlower([FromBody] Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Flowers.Add(flower);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction("GetFlower", new { id = flower.Id }, flower);
        }

        // DELETE: api/Flowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flower = await _dataContext.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            _dataContext.Flowers.Remove(flower);
            await _dataContext.SaveChangesAsync();

            return Ok(flower);
        }

        private bool FlowerExists(int id)
        {
            return _dataContext.Flowers.Any(e => e.Id == id);
        }
    }
}