using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using MyLeasing.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GrowthTrigal.Prism.ViewModels
{
    public class EditMeasurementPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private MeasurementResponse _measurement;
        private string _clave; // atributo privado
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;
        private ObservableCollection<MeasurerResponse> _measurers;
        private MeasurerResponse _measurer;
        private DelegateCommand _saveCommand;



        public EditMeasurementPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            IsEnabled = true;
            _apiService = apiService;
            _navigationService = navigationService;
            Usuario = "orlando.munar@globostudio.net";
            Clave = "123456";

        }

        public string Usuario { get; set; } // propiedades

        public string Clave
        {
            get => _clave;
            set => SetProperty(ref _clave, value);
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();

            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            //var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var request = new TokenRequest
            {
                Password = Clave,
                Username = Usuario,

            };

            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
            var token = response.Result;
            var flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);



            var measurementRequest = new MeasurementRequest
            {
                Measure = Measurement.Measure,
                Id = Measurement.Id,
                MeasureDate = Measurement.MeasureDate,
                FlowerId = flower.Id,
                //HomeId = home.Id,
                //UpId = up.Id

            };


            
                Response<object> response2;
                if (IsEdit)
                {
                    response2 = await _apiService.PutAsync(url, "/api", "/Measurements", measurementRequest.Id, measurementRequest, "bearer", token.Token);
                }
                else
                {
                    response2 = await _apiService.PostAsync(url, "/api", "/Measurements", measurementRequest, "bearer", token.Token);

                }

                IsRunning = false;
                IsEnabled = true;

                if (!response2.IsSuccess)
                {

                    await App.Current.MainPage.DisplayAlert("Error", "Hay Problema", "Aceptar");
                    return;
                }

            
                await App.Current.MainPage.DisplayAlert(
                       "Listo", "Creado", "Aceptar");
            



            //await MeasurementsPageViewModel.GetInstance().Updateflower();

            //IsRunning = false;
            //IsEnabled = true;

            //Settings.Farm = JsonConvert.SerializeObject(this);

            //await _navigationService.NavigateAsync("MeasurementsPage");

            //await _navigationService.GoBackToRootAsync();

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Measurement.Measure))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No hay medida!", "Aceptar");
                return false;
            }




            if (Measurement.MeasureDate == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error no hay fecha", "Aceptar");
                return false;
            }

            return true;
        }




        public ObservableCollection<MeasurerResponse> Measurers
        {
            get => _measurers;
            set => SetProperty(ref _measurers, value);
        }

        public MeasurerResponse Measurer
        {
            get => _measurer;
            set => SetProperty(ref _measurer, value);
        }

        public MeasurementResponse Measurement
        {
            get => _measurement;
            set => SetProperty(ref _measurement, value);
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }



        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Measure"))
            {
                Measurement = parameters.GetValue<MeasurementResponse>("Measure");
                IsEdit = true;
                Title = "Editar Medida";

            }
            else
            {
                Measurement = new MeasurementResponse { MeasureDate = DateTime.Today };

                IsEdit = false;
                Title = "Nueva Medida";

            }

            //LoadMeasurerAsync();

        }



        //private async void LoadMeasurerAsync()
        //{
        //    var url = App.Current.Resources["UrlAPI"].ToString();
        //    var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

        //    var response = await _apiService.GetListAsync<MeasurerResponse>(
        //        url, "/api", "/Measurer/GetMeasurer", "bearer", token.Token);



        //    //if (!response.IsSuccess)
        //    //{
        //    //    await App.Current.MainPage.DisplayAlert("Error", "Problem with user data", "Aceptar");
        //    //    return;
        //    //}

        //    //var measurerslist = (List<MeasurerResponse>)response.Result;
        //    //Measurers = new ObservableCollection<MeasurerResponse>(measurerslist);

        //    //if (!string.IsNullOrEmpty(Measurement.Measurer))
        //    //{
        //    //    Measurer = Measurers.FirstOrDefault(pt => pt.Name == Property.PropertyType);
        //    //}
        //}
    }
}
