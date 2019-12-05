using GrowthTrigal.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GrowthTrigal.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private HomeResponse _home;
      

        public HomePageViewModel(INavigationService navigationService): base (navigationService)
        {
            //Title = "Siembras Activas";

        }

       public  HomeResponse Home
        {
            get => _home;
            set => SetProperty(ref _home, value);
        }

       
     


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if( parameters.ContainsKey("home"))
            {
                Home = parameters.GetValue<HomeResponse>("home");
                Title = $"Bloque: {Home.BlockNumber}";
                
            }
        }
    }
}
