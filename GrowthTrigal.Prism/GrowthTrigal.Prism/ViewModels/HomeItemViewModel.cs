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
    public class HomeItemViewModel: HomeResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectBlockCommand;

        public HomeItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectBlockCommand => _selectBlockCommand ?? (_selectBlockCommand = new DelegateCommand(SelectBlock));

        private async void SelectBlock()
        {
            Settings.Farm = JsonConvert.SerializeObject(this);

            await _navigationService.NavigateAsync("FlowerPage");
        }
    }
}
 