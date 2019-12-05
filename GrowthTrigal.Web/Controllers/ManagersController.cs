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
    public class ManagersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public ManagersController(DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        // GET: Managers
        public IActionResult Index()
        {
            return View(_dataContext.Managers
                .Include(m => m.User));
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
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

                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    var manager = new Manager
                    {
                        User = user
                    };

                    _dataContext.Managers.Add(manager);
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
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "Manager");
                return user;
            }

            return null;
        }

        //// GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(m=> m.User)
                .FirstOrDefaultAsync(m=> m.Id==id);
            if (manager == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Document = manager.User.Document,
                FirstName = manager.User.FirstName,
                Id = manager.Id,
                LastName = manager.User.LastName,
                PhoneNumber = manager.User.PhoneNumber,
            };
            return View (view);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel view)
        {
            

            if (ModelState.IsValid)
            {

                var manager = await _dataContext.Managers
                    .Include(m => m.User)
                    .FirstOrDefaultAsync(m => m.Id == view.Id);
                manager.User.Document = view.Document;
                manager.User.FirstName = view.FirstName;
                manager.User.LastName = view.LastName;
                manager.User.PhoneNumber = view.PhoneNumber;

                await _userHelper.UpdateUserAsync(manager.User);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> DeleteManagers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(me => me.User)
                
              
                .FirstOrDefaultAsync(me => me.Id == id.Value);
            if (manager == null)
            {
                return NotFound();
            }

            _dataContext.Managers.Remove(manager);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(manager.User.Email);
            return RedirectToAction(nameof(Index));//OJO CON ESTO  SI TE REGRESA DONDE ES!!
        }

        //// POST: Managers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var manager = await _datacontext.Managers.FindAsync(id);
        //    _datacontext.Managers.Remove(manager);
        //    await _datacontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ManagerExists(int id)
        //{
        //    return _datacontext.Managers.Any(e => e.Id == id);
        //}
    }
}
