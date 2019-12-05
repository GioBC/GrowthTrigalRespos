using GrowthTrigal.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GrowthTrigal.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) //Se crea la Cconexion BD
        {
        }

        public DbSet<Flower> Flowers { get; set; } // La tabla se va a llamar Flowers, manejamos los datos como una propiedad
        public DbSet<Home> Homes { get; set; } // La tabla se va a llamar Homes, manejamos los datos como una propiedad

        public DbSet<Manager> Managers { get; set; } // La tabla se va a llamar Managers, manejamos los datos como una propiedad
        public DbSet<Measurement> Measurements { get; set; } // La tabla se va a llamar Measurements, manejamos los datos como una propiedad
        public DbSet<Measurer> Measurers { get; set; } // La tabla se va a llamar Measurers, manejamos los datos como una propiedad
        public DbSet<UP> UPs { get; set; } // La tabla se va a llamar UPs, manejamos los datos como una propiedad
   

    }

}

