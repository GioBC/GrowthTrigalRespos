using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using MyLeasing.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Prism.ViewModels
{
    public class MeasurementsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private FlowerResponse _flower;
        private UPResponse _up;
        private HomeResponse _home;
        private ObservableCollection<HomeItemViewModel> _homes;  
        private ObservableCollection<MeasurementsItemViewModel> _measurements;
        private ObservableCollection<FlowerItemViewModel> _flowers;
        private DelegateCommand _addMeasurementCommand;
        private static MeasurementsPageViewModel _instance; /// apuntador a la misma clase
        private string _clave; // atributo privado

        public MeasurementsPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Medidas";
            AliasFarm = "OL";
            Usuario = "orlando.munar@globostudio.net";
            Clave = "123456";
           
            LoadMeasurements();
        }


        public string AliasFarm { get; set; }

       

        public string Usuario { get; set; } // propiedades
        
        public string Clave
        {
            get => _clave;
            set => SetProperty(ref _clave, value);
        }

        public DelegateCommand AddMeasurementCommand => _addMeasurementCommand ?? (_addMeasurementCommand = new DelegateCommand(AddMeasurement));

      

        public ObservableCollection<MeasurementsItemViewModel> Measurement
        {
            get => _measurements;
            set => SetProperty(ref _measurements, value);

        }

        public ObservableCollection<FlowerItemViewModel> Seed
        {
            get => _flowers;
            set => SetProperty(ref _flowers, value);

        }

        public ObservableCollection<HomeItemViewModel> Blocks
        {
            get => _homes;
            set => SetProperty(ref _homes, value);
        }


        public static MeasurementsPageViewModel GetInstance()
        {
            return _instance;
        }


        public async Task Updateflower()
        {
           
            var url = App.Current.Resources["UrlAPI"].ToString();
            var request = new TokenRequest
            {
                Password = Clave,
                Username = Usuario,
                AliasFarm = AliasFarm,

            };

            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
            var token = response.Result;


            var response2 = await _apiService.GetUPByEmailAsync(
                url,
                "api",
                "/Homes/GetUPByEmail",
                "bearer",
                token.Token,
               AliasFarm);


            if (response2.IsSuccess)
            {
                Settings.Farm = JsonConvert.SerializeObject(response2);
                _flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);
                Measurement = new ObservableCollection<MeasurementsItemViewModel>(_flower.Measurements.Select(mea => new MeasurementsItemViewModel(_navigationService)
                {
                    Measure = mea.Measure,
                    MeasureDate = mea.MeasureDate,
                    Id = mea.Id,
                    Measurer = mea.Measurer,
                }).ToList());
            }
        }
        

        /// esto no lo voy a borrar para ver más adelante su uso

        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters);

        //    if (parameters.ContainsKey("Seed"))
        //    {
        //        _flower = parameters.GetValue<FlowerResponse>("Seed");

        //        //LoadMeasurements();

        //    }
        //}

        //private void LoadMeasurements()
        //{
        //    Measurement = new ObservableCollection<MeasurementsItemViewModel>(_flower.Measurements.Select(mea => new MeasurementsItemViewModel(_navigationService)
        //    {
        //        Measure = mea.Measure,
        //        MeasureDate = mea.MeasureDate,
        //        Id = mea.Id,
        //        Measurer = mea.Measurer,
        //    }).ToList());
        //}


        // de aqui en adelante esto es lo que funciona con las instancias

        private void LoadMeasurements()
        {

            _flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);
            Measurement = new ObservableCollection<MeasurementsItemViewModel>(_flower.Measurements.Select(mea => new MeasurementsItemViewModel(_navigationService)
            {
                Measure = mea.Measure,
                MeasureDate = mea.MeasureDate,
                Id = mea.Id,
                Measurer = mea.Measurer,
            }).ToList());
        }


        private async void AddMeasurement()
        {

            await _navigationService.NavigateAsync("EditMeasurementPage");
        }
    }
}
