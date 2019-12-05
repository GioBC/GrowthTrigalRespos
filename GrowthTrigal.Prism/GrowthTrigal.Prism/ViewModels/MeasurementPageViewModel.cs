using GrowthTrigal.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrowthTrigal.Prism.ViewModels
{
    public class MeasurementPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private MeasurementResponse _measurement;
        private DelegateCommand _editMeasureCommand;
        public MeasurementPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            Title = "Detalles de Medida";
            _navigationService = navigationService;
        }

        public MeasurementResponse Measurements
        {
            get => _measurement;
            set => SetProperty(ref _measurement, value);
        }
        

        public DelegateCommand EditMeasureCommand => _editMeasureCommand ?? (_editMeasureCommand = new DelegateCommand(SelectEditMeasure));

      

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Measure"))
            {
                Measurements= parameters.GetValue<MeasurementResponse>("Measure");
                
            }
        }

        private async void SelectEditMeasure()
        {
            var parameters = new NavigationParameters
            {
                { "Measuremt", Measurements }
            };

            await _navigationService.NavigateAsync("EditMeasurementPage", parameters);
        }
    }
}
