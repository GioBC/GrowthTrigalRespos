using GrowthTrigal.Web.Data;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public HomeViewModel ToFlowerViewModel(Flower flower)
        {
            return new HomeViewModel
            {
                Type = $"{flower.Type}",
                VarietyName = $"{flower.VarietyName}",
                BedName = $"{flower.BedName}",
                Id =  flower.Id,
                Home =flower.Home,
                HomeId= flower.Home.Id
              

            };
        }

        public async Task<Flower> ToSeedAsync(HomeViewModel model, bool isNew)
        {
            return new Flower
            {
 
                Type = $"{model.Type}",
                VarietyName = $"{model.VarietyName}",
                BedName = $"{model.BedName}",
                Id = isNew ? 0 : model.Id,
                Home = await _dataContext.Homes.FindAsync(model.HomeId),

            };
        }



        

        public async Task<Measurement> ToMeasureAsync(MeasurementsViewModel model, bool isNew)
        {
            return new Measurement
            {

                Measure = $"{model.Measure}",
                MeasureDate= model.MeasureDate.ToUniversalTime(),
                Id = isNew ? 0 : model.Id,
                Flower = await _dataContext.Flowers.FindAsync(model.FlowerId),
                Measurer = await _dataContext.Measurers.FindAsync(model.MeasurerId),
               


                


            };
        }

        public MeasurementsViewModel ToMeasurermentViewModel(Measurement measure)
        {
            return new MeasurementsViewModel
            {
                Measure = measure.Measure,
                MeasureDate = measure.MeasureDateLocal,
                Id =  measure.Id,
                Flower = measure.Flower,
                FlowerId = measure.Flower.Id,
                MeasurerId = measure.Measurer.Id,
                Measurers = _combosHelper.GetComboMeasurers(),
              


            };
        }

        public Task ToHomeAsync(AddHomeViewModel model, bool v)
        {
            throw new NotImplementedException();
        }

        //public async Task<Home> ToHomeAsync(AddHomeViewModel model, bool isNew)
        //{
        //    return new Home
        //    {


        //        BlockNumber=model.BlockNumber,
        //        Id = isNew ? 0 : model.Id,






        //    };
        //}


    }
}
