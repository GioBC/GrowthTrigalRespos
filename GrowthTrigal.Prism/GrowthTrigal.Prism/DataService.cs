
namespace GrowthTrigal.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Interfaces;
    using SQLite;
    using SQLiteNetExtensionsAsync.Extensions;
    using Xamarin.Forms;

    using Microsoft.EntityFrameworkCore;

    public class DataService
    {

        private SQLiteAsyncConnection connection;
        private readonly DataService _dataService;


        public DataService()
        {
            this.OpenOrCreateDB();
        }
        private async Task OpenOrCreateDB()
        {

                var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
                this.connection = new SQLiteAsyncConnection(databasePath);
                await connection.CreateTableAsync<TokenRequest>().ConfigureAwait(false);
                await connection.CreateTableAsync<MeasurerResponse>().ConfigureAwait(false);
                await connection.CreateTableAsync<MeasurementResponse>().ConfigureAwait(false);
                await connection.CreateTableAsync<FlowerResponse>().ConfigureAwait(false);
                await connection.CreateTableAsync<HomeResponse>().ConfigureAwait(false);
                await connection.CreateTableAsync<UPResponse>().ConfigureAwait(false);
                await connection.CreateTableAsync<MeasurementRequest>().ConfigureAwait(false);
 

        }
        public async Task Insert<T>(List<T> models)
        {
            try
            {
                await connection.InsertWithChildrenAsync(models, recursive: true).ConfigureAwait(false);
            }
            catch (Exception ex) 
            {
                throw;
            }
              
        }
        public async Task Insert<T>(T model)
        {
            try
            {
                await connection.InsertWithChildrenAsync(model, recursive: true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }
        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }
        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }
        public async Task<List<UPResponse>> GetAllHomes()
        {
            try
            {

                var querty_UPs = await this.connection.QueryAsync<UPResponse>("SELECT * FROM [UPResponse]");
                var querty_Homes = await this.connection.QueryAsync<HomeResponse>("SELECT * FROM [HomeResponse]");
                var querty_Flowers = await this.connection.QueryAsync<FlowerResponse>("select * from  [FlowerResponse]");
                var querty_Measurement = await this.connection.QueryAsync<MeasurementResponse>("SELECT * FROM [MeasurementResponse]");
                var querty_Medidas = await this.connection.QueryAsync<MeasurementRequest>("select * from [MeasurementRequest]");

                var array = querty_UPs.ToList();
                var arrayHomes = querty_Homes.ToList();
                var arrayFlowers = querty_Flowers.ToList();
                var arrayMeasurements = querty_Measurement.ToList();

                var list = array.Select(u => new UPResponse
                {
                    Id = u.Id,
                    AliasFarm = u.AliasFarm,
                    FarmName = u.FarmName,
                    Homes = arrayHomes.Select(hm => new HomeResponse
                    {
                        Id = hm.Id,
                        BlockNumber = hm.BlockNumber,
                        UP_Id = hm.UP_Id,
                        UP = hm.UP,
                        Flowers = arrayFlowers.Select(f => new FlowerResponse
                        {
                            Id = f.Id,
                            Type = f.Type,
                            VarietyName = f.VarietyName,
                            BedName = f.BedName,
                            Measurements = arrayMeasurements.Select(mea => new MeasurementResponse
                            {
                                Measure = mea.Measure,
                                MeasureDate = mea.MeasureDate,
                                Id = mea.Id
                            }).ToList(),
                        }).ToList(),
                    }).ToList(),
                }).ToList();
 
                return list;

            }

            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<TokenRequest>> GetUser()
        {
            var querty_Users = await this.connection.QueryAsync<TokenRequest>("select Username from [TokenRequest]");
            var array = querty_Users.ToArray();
            var list = array.Select(tk => new TokenRequest
            {
                Username = tk.Username,
                Password = tk.Password,
                BlockNumber = tk.BlockNumber,
                AliasFarm = tk.AliasFarm

            }).ToList();
            return list;
        }
        public async Task<List<TokenRequest>> GetPwd()
        {
            var querty_Users = await this.connection.QueryAsync<TokenRequest>("select Password from [TokenRequest]");
            var array = querty_Users.ToArray();
            var list = array.Select(tk => new TokenRequest
            {
                Password = tk.Password

            }).ToList();
            return list;
        }
        public async Task<List<MeasurementRequest>> GetMeasurers()
        {
            try
            {
                var querty_Medidas = await this.connection.QueryAsync<MeasurementRequest>("select * from [MeasurementRequest]");
                var array = querty_Medidas.ToArray();
                var list = array.Select(tk => new MeasurementRequest
                {
                    Id = tk.Id,
                    Measure = tk.Measure,
                    MeasureDate = tk.MeasureDate,
                    FlowerId = tk.FlowerId

                }).ToList();
                return list;

            }
            catch (Exception ex) 
            {
                return null;
            }
        }
        public async Task DeleteAllUpsHomes()
        {
            var querty_Ups = await this.connection.QueryAsync<UPResponse>("delete from [UPResponse]");
            var querty_Homes = await this.connection.QueryAsync<HomeResponse>("delete  from [HomeResponse]");
            var querty_Flowers = await this.connection.QueryAsync<FlowerResponse>("delete from [FlowerResponse]");
            var querty_Measurement = await this.connection.QueryAsync<MeasurementResponse>("delete from [MeasurementResponse]");
            var querty_Measurer = await this.connection.QueryAsync<MeasurerResponse>("delete from [MeasurerResponse]");
        }
        public async Task DeleteAllUsers()
        {
            var querty_Users = await this.connection.QueryAsync<TokenRequest>("delete from [TokenRequest]");
        }
        public async Task DeleteAllMeasurers()
        {
            try
            {
                var querty_Measurers = await this.connection.QueryAsync<MeasurementRequest>("delete from [MeasurementRequest]");
            }
            catch (Exception ex)
            { 

            }
        }
    }
}
