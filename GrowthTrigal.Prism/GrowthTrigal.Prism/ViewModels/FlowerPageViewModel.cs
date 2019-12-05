using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GrowthTrigal.Prism.ViewModels
{
    public class FlowerPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private HomeResponse _home;
        private ObservableCollection<FlowerItemViewModel> _flowers;
       
        public FlowerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            var home = JsonConvert.DeserializeObject<HomeResponse>(Settings.Farm);
            Title = $"Bloque: {home.BlockNumber}";
            LoadFlowers();
        }
        

        public ObservableCollection<FlowerItemViewModel> Seed
        {
            get => _flowers;
            set => SetProperty(ref _flowers, value);

        }


        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters);

        //    if (parameters.ContainsKey("home"))
        //    {
        //        _home = parameters.GetValue<HomeResponse>("home");
        //        //Title = $"Bloque: {_home.BlockNumber}";
        //        //LoadFlowers();

        //    }
        //}

        //private void LoadFlowers()
        //{
        //    Seed = new ObservableCollection<FlowerItemViewModel>(_home.Flowers.Select(f => new FlowerItemViewModel(_navigationService)
        //    {
        //        Type = f.Type,
        //        VarietyName = f.VarietyName,
        //        BedName = f.BedName,
        //        Id = f.Id,
        //        Measurements = f.Measurements?.Select(mea => new MeasurementResponse
        //        {
        //            Measure = mea.Measure,
        //            MeasureDate = mea.MeasureDate,
        //            Id = mea.Id,
        //            Measurer=mea.Measurer
        //        }).ToList(),
        //    }).ToList());
        //}
        
        private void LoadFlowers()
        {
            _home = JsonConvert.DeserializeObject<HomeResponse>(Settings.Farm);

            Seed = new ObservableCollection<FlowerItemViewModel>(_home.Flowers.Select(f => new FlowerItemViewModel(_navigationService)
            {
                Type = f.Type,
                VarietyName = f.VarietyName,
                BedName = f.BedName,
                Id = f.Id,
                Measurements = f.Measurements?.Select(mea => new MeasurementResponse
                {
                    Measure = mea.Measure,
                    MeasureDate = mea.MeasureDate,
                    Id = mea.Id,
                    Measurer = mea.Measurer
                }).ToList(),
            }).ToList());
        }

    }
}
