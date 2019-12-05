using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Models;
using GrowthTrigal.Web.Helpers;

namespace GrowthTrigal.Web.Controllers
{
    public class MeasurersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public MeasurersController(DataContext datacontext,
            IUserHelper  userHelper)
        {
            _dataContext = datacontext;
            _userHelper = userHelper;
        }

        // GET: Measurers
        public IActionResult Index()
        {
            return View(_dataContext.Measurers
                .Include(m=>m.User));
        }

        // GET: Measurers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurer = await _dataContext.Measurers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measurer == null)
            {
                return NotFound();
            }

            return View(measurer);
        }

        // GET: Measurers/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user =  await CreateUserAsync (model);
                if (user!=null)
                {
                    var measurer = new Measurer
                    {
                        User= user
                    };

                    _dataContext.Measurers.Add(measurer);
                    await _dataContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "User with this email already exists. ");
            }
            return View(model);
        }

        private async Task<User> CreateUserAsync(AddUserViewModel model)
        {
            var user = new User
            {
                Document=model.Document,
                Email= model.Username,
                FirstName=model.FirstName,
                LastName=model.LastName,
                PhoneNumber= model.PhoneNumber,
                UserName= model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if(result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "Measurer");
                return user;
            }

            return null;
        }




        //// GET: Measurers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurer = await _dataContext.Measurers
                .Include(m=> m.User)
                .FirstOrDefaultAsync(m=> m.Id==id.Value);

            if (measurer == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Document = measurer.User.Document,
                FirstName = measurer.User.FirstName,
                Id = measurer.Id,
                LastName = measurer.User.LastName,
                PhoneNumber = measurer.User.PhoneNumber,
            };
            return View(view);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel view)
        {

            if (ModelState.IsValid)
            {


                var  measurer = await _dataContext.Measurers
                    .Include(me=> me.User)
                      .FirstOrDefaultAsync(me=> me.Id == view.Id);

                measurer.User.Document = view.Document;
                measurer.User.FirstName = view.FirstName;
                measurer.User.LastName = view.LastName;
                measurer.User.PhoneNumber = view.PhoneNumber;

                await _userHelper.UpdateUserAsync(measurer.User);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }


        public async Task<IActionResult> DeleteMeasurer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurer = await _dataContext.Measurers
                .Include(me=> me.User)
                .Include(me=> me.Measurements)
                .FirstOrDefaultAsync(m => m.Id == id.Value);
            if (measurer == null)
            {
                return NotFound();
            }


            if (measurer.Measurements.Count!=0)
            {
                ModelState.AddModelError(string.Empty, "Measurer can't be delete because it has measurements.");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Measurers.Remove(measurer);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(measurer.User.Email);
            return RedirectToAction(nameof(Index));//OJO CON ESTO  SI TE REGRESA DONDE ES!!
        }

       
    }
}
