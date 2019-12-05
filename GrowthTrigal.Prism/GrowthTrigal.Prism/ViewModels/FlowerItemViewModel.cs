using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Prism.ViewModels
{
   public  class FlowerItemViewModel: FlowerResponse
    {

        private readonly INavigationService _navigationService;
        private DelegateCommand _selectBlockSeedComand;
        public FlowerItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectBlockSeedComand => _selectBlockSeedComand ?? (_selectBlockSeedComand = new DelegateCommand(SelectSeed));

        private async void SelectSeed()
        {

            Settings.Farm = JsonConvert.SerializeObject(this);

            //await _navigationService.NavigateAsync("MeasurementsPage");
            await _navigationService.NavigateAsync("EditMeasurementPage");
            //var parameters = new NavigationParameters
            //{
            //    { "Seed", this }
            //};

            //await _navigationService.NavigateAsync("MeasurementsPage", parameters);
        }
    }
}
