using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Helpers;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
      

        public SeedDb(DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
          
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // cuando pase por aqui automaticamente crea el DB así este borrada, crea la estructura de la DB
            await CheckRoles();
            var manager = await CheckUserAsync("1152442863", "Orlando", "Munar", "orlando.munar@globostudio.net", "319 727 8962","Manager");
            var measurer = await CheckUserAsync("1152442863", "Orlando", "Munar", "omunarb@unal.edu.co", "319 727 8962","Measurer");
            await CheckHomesAsync();
            await CheckManagerAsync(manager);
            await CheckMeasurersAsync(measurer);
            await CheckUPsAsync();
            await CheckFlowersAsync();
            await CheckMeasurementsAsync();
        }

        private async Task CheckFlowersAsync()
        {
            var home = _context.Homes.FirstOrDefault();
            
            if (!_context.Flowers.Any())
            {
                AddFlower("Spider", "Saffina", "001A", home);
                AddFlower("Cremon", "Zembla Lime","001B", home);
                AddFlower("Cremon", "Rossano", "003A", home);
                AddFlower("Spider", "Novastar", "004A", home);
                AddFlower("Spider", "Anastasia Lilac (Anamora)", "005B", home);
                AddFlower("Cremon", "Maisy", "006B", home);
                AddFlower("Cremon", "Arctic Queen", "009B", home);
                AddFlower("Spider", "Sol", "010A", home);
                AddFlower("Spider", "Sol", "010B", home);
                AddFlower("Cremon", "Solemio", "016B", home);
                AddFlower("Spider", "Anastasia Lilac (Anamora)", "020A", home);
                AddFlower("Spider", "Anastasia White", "024A", home);
                AddFlower("Cremon", "Maisy", "026A", home);
                AddFlower("Spider", "Anastasia Dark Green", "028A", home);
                AddFlower("Spider", "Anastasia Dark Green", "029A", home);
                AddFlower("Cremon", "Andrea", "040B", home);
                AddFlower("Cremon", "Andrea", "041A", home);
                AddFlower("Pompon", "Tedcha", "043B", home);
                AddFlower("Cremon", "Solemio Yellow", "044A", home);
                AddFlower("Pompon", "Orange Tedcha", "044B", home);


                await _context.SaveChangesAsync();
            }
        }

        private void AddFlower(string Type, string VarietyName, string BedName, Home home)
        {
            _context.Flowers.Add(new Flower
            {
                Type = Type,
                VarietyName = VarietyName,
                BedName= BedName,
                Home = home

            });

        }




        private async Task CheckMeasurementsAsync()
        {
            var measurer = _context.Measurers.FirstOrDefault();
            var flower = _context.Flowers.FirstOrDefault();
            //var up = _context.UPs.FirstOrDefault();
           
            if (!_context.Measurements.Any())
            {

                _context.Measurements.Add(new Measurement
                {
                    MeasureDate = DateTime.Today,
                    Measurer= measurer,
                    Flower= flower,
                    Measure= "70,5",
                    

                }) ;
            }

            await _context.SaveChangesAsync();

        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }

      

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

               // var token = await  _userHelper.GenerateEmailConfirmationTokenAsync(user);
               //await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }


        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Measurer");

        }

        private async Task CheckMeasurersAsync(User user)
        {
            if (!_context.Measurers.Any())
            {
                _context.Measurers.Add(new Measurer { User = user });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckHomesAsync()
        {
            
            if (!_context.Homes.Any())
            {

                _context.Homes.Add(new Entities.Home { BlockNumber = "01" });
                
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUPsAsync()
        {
            //var home = _context.Homes.FirstOrDefault();
            //var flower = _context.Flowers.FirstOrDefault();
            if (!_context.UPs.Any())
            {
                //AddUP("FLORES EL TRIGAL SAS", "OL", home, flower);
                //AddUP("FLORES EL TRIGAL SAS", "OL", home, flower);
                //AddUP("FLORES EL TRIGAL SAS", "OL", home, flower);
                AddUP("FLORES EL TRIGAL SAS", "OL");
               
                await _context.SaveChangesAsync();
            }
        }

        private void AddUP(string FarmName, string AliasFarm/*, Home home, Flower flower*/)
        {
            _context.UPs.Add(new UP
            {
                FarmName = FarmName,
                AliasFarm = AliasFarm
                //Home = home,
                //Flower = flower

            });

        }

       
    }















}

