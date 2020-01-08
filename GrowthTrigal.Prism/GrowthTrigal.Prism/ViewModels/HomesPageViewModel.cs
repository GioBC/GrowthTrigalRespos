using GrowthTrigal.Common.Models;
using Prism.Navigation;
using System.Linq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowthTrigal.Prism.ViewModels
{
    public class HomesPageViewModel : ViewModelBase

    {
        private readonly INavigationService _navigationService;
        private UPResponse _Up;
        private ObservableCollection<HomeItemViewModel> _homes;
        private DataService _dataService;
        private List<UPResponse> UP;



        public HomesPageViewModel(
            INavigationService navigationService,
            DataService dataService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        public ObservableCollection<HomeItemViewModel> Blocks
        {
            get => _homes;
            set => SetProperty(ref _homes, value);
        }




        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("farm"))
            {

                _Up = parameters.GetValue<UPResponse>("farm");
                Title = $" { _Up.FarmName}";
                Blocks = new ObservableCollection<HomeItemViewModel>(_Up.Homes.Select(h => new HomeItemViewModel(_navigationService)
                {
                    BlockNumber = h.BlockNumber,
                    Id = h.Id,
                    Flowers = h.Flowers?.Select(f => new FlowerResponse
                    {
                        Id = f.Id,
                        Type = f.Type,
                        VarietyName = f.VarietyName,
                        BedName = f.BedName,
                        Measurements = f.Measurements?.Select(mea => new MeasurementResponse
                        {
                            Measure = mea.Measure,
                            MeasureDate = mea.MeasureDate,
                            Id = mea.Id,
                            Measurer = mea.Measurer,

                        }).ToList(),
                    }).ToList(),
                }).ToList());
            }


            if (parameters.ContainsKey("farm1"))
            {
                await LoadHomes();

                var farmName = UP.FirstOrDefault().FarmName;
                var Homes = UP.FirstOrDefault().Homes;
                Title = $" { farmName }";
                Blocks = new ObservableCollection<HomeItemViewModel>(Homes.Select(h => new HomeItemViewModel(_navigationService)
                {
                    BlockNumber = h.BlockNumber,
                    Id = h.Id,
                    Flowers = h.Flowers?.Select(f => new FlowerResponse
                    {
                        Id = f.Id,
                        Type = f.Type,
                        VarietyName = f.VarietyName,
                        BedName = f.BedName,
                        Measurements = f.Measurements?.Select(mea => new MeasurementResponse
                        {
                            Measure = mea.Measure,
                            MeasureDate = mea.MeasureDate,
                            Id = mea.Id,
                            Measurer = mea.Measurer,

                        }).ToList(),
                    }).ToList(),
                }).ToList());
            }
        }

        private async Task LoadHomes()
        {
            UP = await _dataService.GetAllHomes();
        }
    }
}
