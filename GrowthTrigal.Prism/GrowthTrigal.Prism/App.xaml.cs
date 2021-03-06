﻿using Prism;
using Prism.Ioc;
using GrowthTrigal.Prism.ViewModels;
using GrowthTrigal.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyLeasing.Common.Services;
using GrowthTrigal.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GrowthTrigal.Prism
{
    public partial class App
    {
        DataService dataService;
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            dataService = new DataService();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();        
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomesPage, HomesPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();

            containerRegistry.RegisterForNavigation<FlowerPage, FlowerPageViewModel>();
            containerRegistry.RegisterForNavigation<MeasurementsPage, MeasurementsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditMeasurementPage, EditMeasurementPageViewModel>();
            containerRegistry.RegisterForNavigation<MeasurementPage, MeasurementPageViewModel>();
        }
    }
}
