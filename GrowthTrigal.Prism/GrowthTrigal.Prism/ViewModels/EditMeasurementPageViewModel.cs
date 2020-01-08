using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using GrowthTrigal.Common.Services;
using MyLeasing.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private DelegateCommand _agregarCommand;
        private DataService _dataService;
        private List<UPResponse> UP;



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

        public DelegateCommand AgregarCommand => _agregarCommand ?? (_agregarCommand = new DelegateCommand(addMeasureAsync));

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

        private List<MeasurementRequest> Measurelist { get; set; }

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

        private async void addMeasureAsync()
        {

            var resultvalidate = await ValidateDataAsync();

            if (resultvalidate == true)
            {

                var flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);

                var measurementRequest = new MeasurementRequest
                {
                    Measure = Measurement.Measure,
                    Id = Measurement.Id,
                    MeasureDate = Measurement.MeasureDate,
                    FlowerId = flower.Id,

                };
                var datos = new DataService();
                await datos.Insert(measurementRequest);

                await App.Current.MainPage.DisplayAlert(
                   "Listo", "Creado", "Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Validar ingreso de datos", "Aceptar");
            }


        }

        private async void SaveAsync()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                //IsRunning = true;
                //IsEnabled = false;
                await App.Current.MainPage.DisplayAlert("Error", "Valide su conexion a internet para realizar sincronizacion", "Aceptar");


            }
            else
            {      
                //var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                var request = new TokenRequest
                {
                    Password = Clave,
                    Username = Usuario,

                };

                var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
                var token = response.Result;
                // var flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);
                try
                {
                    DataService dataService = new DataService();
                    Measurelist = await dataService.GetMeasurers();
                    if (Measurelist.Count == 0)
                    {
                        //IsRunning = true;
                        //IsEnabled = false;

                        await App.Current.MainPage.DisplayAlert("Alert", "No hay medidas pendiendes para guardar", "Aceptar");
                    }
                    else
                    {
                        foreach (MeasurementRequest item in Measurelist) 
                        {
                            var Measure = item.Measure;
                            var IdMeasure = item.Id;
                            var MeasureDate = item.MeasureDate;
                            var FlowerId = item.FlowerId;

                            var measurementRequest = new MeasurementRequest
                            {
                                Measure = Measure,
                                Id = IdMeasure,
                                MeasureDate = MeasureDate,
                                FlowerId = FlowerId

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

                            if (!response2.IsSuccess)
                            {

                                await App.Current.MainPage.DisplayAlert("Error", "Hay Problema", "Aceptar");
                                return;
                            }
                           
                        }
                        //IsRunning = true;
                        //IsEnabled = false;
                        await dataService.DeleteAllMeasurers();
                        await App.Current.MainPage.DisplayAlert(
                                     "Listo", "Medidas sincronizadas corretamente", "Aceptar");
                    }

                           

                }
                catch (Exception ex)
                {
                }
            }
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
        }

    }
}
