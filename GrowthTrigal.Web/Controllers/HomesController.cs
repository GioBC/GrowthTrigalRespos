using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Helpers;
using GrowthTrigal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Controllers
{
    public class HomesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public HomesController(

            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)

        {
            _dataContext = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: Homes
        public IActionResult Index()
        {
            var home = _dataContext.Homes
                .OrderBy(h => h.BlockNumber)
                .Include(h => h.Flowers);

            return View(home);



        }


        // GET: Homes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _dataContext.Homes
                .Include(h => h.Flowers)
                


                .FirstOrDefaultAsync(h => h.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()/////////////////////////////////////////////////////////////////////////////////
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Home home)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(home);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _dataContext.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlockNumber,BedName")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(home);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Id))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _dataContext.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var home = await _dataContext.Homes.FindAsync(id);
            _dataContext.Homes.Remove(home);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(int id)
        {
            return _dataContext.Homes.Any(e => e.Id == id);

        }

        public async Task<IActionResult> AddSeed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var home = await _dataContext.Homes

                .FindAsync(id); //pegale el async firs of default
            if (home == null)
            {
                return NotFound();
            }

            var model = new HomeViewModel
            {
                HomeId = home.Id,


            };

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> AddSeed(HomeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var seed = await _converterHelper.ToSeedAsync(model, true);
                _dataContext.Flowers.Add(seed);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"Details/{model.HomeId}");
            }

            //model.Types = _combosHelper.GetComboTypes();
            //model.VarietyNames = _combosHelper.GetComboVarietyNames();
            //model.BedNames = _combosHelper.GetComboBedNames();
            return View(model);
        }



        public async Task<IActionResult> EditSeed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var flower = await _dataContext.Flowers
                .Include(f => f.Home)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToFlowerViewModel(flower);

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> EditSeed(HomeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var flower = await _converterHelper.ToSeedAsync(model, false);
                _dataContext.Flowers.Update(flower);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"Details/{model.HomeId}");
            }


            return View(model);
        }




        public async Task<IActionResult> DetailsMeasure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _dataContext.Flowers // NO MOVER
                .Include(h => h.Measurements)




                .FirstOrDefaultAsync(h => h.Id == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }



        public async Task<IActionResult> AddMeasure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var measure = await _dataContext.Flowers ///cambie de flowers
                .Include(f=> f.Home)

                .FirstOrDefaultAsync(f => f.Id == id.Value); //pegale el async firs of default
            if (measure == null)
            {
                return NotFound();
            }

            var model = new MeasurementsViewModel
            {
                
                FlowerId = measure.Id,
                MeasureDate = DateTime.Today,
                Measurers = _combosHelper.GetComboMeasurers(),
   
                
            };

            

            return View(model);
            
        }



        [HttpPost]
        public async Task<IActionResult> AddMeasure(MeasurementsViewModel model)
        {

            if (ModelState.IsValid)
            {
                var measure = await _converterHelper.ToMeasureAsync(model, true);
                _dataContext.Measurements.Add(measure);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"DetailsMeasure/{model.FlowerId}");
            }
            model.Measurers = _combosHelper.GetComboMeasurers();

            return View(model);
        }




        public async Task<IActionResult> EditMeasure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var measure = await _dataContext.Measurements
                .Include(f => f.Flower)
                .Include(f => f.Measurer)
                
                .FirstOrDefaultAsync(f => f.Id == id.Value);
            if (measure == null)
            {
                return NotFound();
            }

            

            return View(_converterHelper.ToMeasurermentViewModel(measure));

        }

        [HttpPost]
        public async Task<IActionResult> EditMeasure(MeasurementsViewModel model)
        {

            if (ModelState.IsValid)
            {
                var measure = await _converterHelper.ToMeasureAsync(model, false);
                _dataContext.Measurements.Update(measure);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"DetailsMeasure/{model.FlowerId}");
            }

            model.Measurers = _combosHelper.GetComboMeasurers();
            return View(model);
        }



        public async Task<IActionResult> DeleteMeasure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _dataContext.Measurements
                .Include(me => me.Flower)

                .FirstOrDefaultAsync(me => me.Id == id.Value);
            if (measurement == null)
            {
                return NotFound();
            }

            _dataContext.Measurements.Remove(measurement);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsMeasure)}/{measurement.Flower.Id}");//OJO CON ESTO  SI TE REGRESA DONDE ES!!
            //return View(measurement);
        }

       


        public async Task<IActionResult> DeleteSeed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _dataContext.Flowers

                .FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("DeleteSeed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSeed(int id)
        {
            var flower = await _dataContext.Flowers.FindAsync(id);
            _dataContext.Flowers.Remove(flower);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }



}



