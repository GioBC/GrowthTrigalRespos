using GrowthTrigal.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Services
{
    public class DataContextSQLite : IdentityDbContext<UserResponse>
    {

        //public DataContextSQLite(DbContextOptions<DataContextSQLite> options) : base(options) //Se crea la Cconexion BD
        //{
        //}
        public DbSet<TokenResponse> TokenResponses { get; set; } // La tabla se va a llamar Flowers, manejamos los datos como una propiedad
        public DbSet<MeasurerResponse> MeasurerResponses { get; set; } // La tabla se va a llamar Homes, manejamos los datos como una propiedad

        public DbSet<MeasurementResponse> MeasurementResponses { get; set; } // La tabla se va a llamar Managers, manejamos los datos como una propiedad
        public DbSet<FlowerResponse> FlowerResponses { get; set; } // La tabla se va a llamar Measurements, manejamos los datos como una propiedad
        public DbSet<HomeResponse> HomeResponses { get; set; } // La tabla se va a llamar Measurers, manejamos los datos como una propiedad
        public DbSet<UPResponse> UPResponses { get; set; } // La tabla se va a llamar UPs, manejamos los datos como una propiedad
        public DbSet<UpRequest> UpRequests { get; set; } // La tabla se va a llamar UPs, manejamos los datos como una propiedad

    }
}
