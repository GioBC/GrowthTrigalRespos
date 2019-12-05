using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Controllers
{
    //[Authorize(Roles = "Manager")]//permisos de usuario
    public class FlowersController : Controller
    {
        private readonly DataContext _datacontext;
        

        public FlowersController(

            DataContext datacontext)
        {
            _datacontext = datacontext;
           
        }

        // GET: Flowers
        public IActionResult Index()
        {
            return View(_datacontext.Flowers.OrderBy(f =>f.BedName)
                .Include(f => f.Home)
                .Include(f => f.Measurements)
                .ThenInclude(m => m.Measurer));
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _datacontext.Flowers
                .Include(f => f.Measurements)
                .ThenInclude(m => m.Measurer)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // GET: Flowers/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flower flower)
        {
            if (ModelState.IsValid)
            {
                _datacontext.Add(flower);
                await _datacontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        // GET: Flowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _datacontext.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }
            return View(flower);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,VarietyName")] Flower flower)
        {
            if (id != flower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _datacontext.Update(flower);
                    await _datacontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowerExists(flower.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        // GET: Flowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _datacontext.Flowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flower = await _datacontext.Flowers.FindAsync(id);
            _datacontext.Flowers.Remove(flower);
            await _datacontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowerExists(int id)
        {
            return _datacontext.Flowers.Any(e => e.Id == id);
        }





    }
}
