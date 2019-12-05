using GrowthTrigal.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Prism.ViewModels
{
   public class MeasurementsItemViewModel: MeasurementResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMeasurEditComand;
        public MeasurementsItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMeasurEditComand => _selectMeasurEditComand ?? (_selectMeasurEditComand = new DelegateCommand(SelectMeasurEdit));



        private async void SelectMeasurEdit()
        {
            var parameters = new NavigationParameters
            {
                { "Measure", this }
            };

            await _navigationService.NavigateAsync("EditMeasurementPage", parameters);
        }
    }
}
