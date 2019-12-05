using System.Threading.Tasks;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Models;

namespace GrowthTrigal.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Flower> ToSeedAsync(HomeViewModel model, bool isNew);
        HomeViewModel ToFlowerViewModel(Flower flower);


        Task<Measurement> ToMeasureAsync(MeasurementsViewModel model, bool isNew);
        MeasurementsViewModel ToMeasurermentViewModel(Measurement measure);
        Task ToHomeAsync(AddHomeViewModel model, bool v);
    }
} 