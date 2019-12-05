using GrowthTrigal.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Prism.ViewModels
{
    public class MeasurementViewModel: MeasurementResponse 
    {
        private readonly INavigationService _navigationService;
        
        
        public MeasurementViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


       



    }
}
