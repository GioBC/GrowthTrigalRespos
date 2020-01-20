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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Prism.ViewModels
{
    public class EditMeasurementPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private MeasurementResponse _measurementone;
        private MeasurementResponse _measurementtwo;
        private MeasurementResponse _measurementthee;
        private string _clave; // atributo privado
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;
        private ObservableCollection<MeasurerResponse> _measurers;
        private MeasurerResponse _measurer;
        private DelegateCommand _agregarCommand;



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
        public MeasurementResponse MeasurementOne
        {
            get => _measurementone;
            set => SetProperty(ref _measurementone, value);
        }
        public MeasurementResponse MeasurementTwo
        {
            get => _measurementtwo;
            set => SetProperty(ref _measurementtwo, value);
        }
        public MeasurementResponse MeasurementThree
        {
            get => _measurementthee;
            set => SetProperty(ref _measurementthee, value);
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
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            var flower = JsonConvert.DeserializeObject<FlowerResponse>(Settings.Farm);
            var resultvalidate = await ValidateDataAsync();

            if (resultvalidate == true)
            {
                if (!connection)
                {
                    IsEnabled = false;
                    IsRunning = true;

                    var measurementRequestone = new MeasurementRequest
                    {
                        Measure = MeasurementOne.Measure,
                        Id = MeasurementOne.Id,
                        MeasureDate = MeasurementOne.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    var datos = new DataService();
                    if (measurementRequestone.Measure != null)
                    {
                        await datos.Insert(measurementRequestone);
                    }
                    var measurementRequesttwo = new MeasurementRequest
                    {
                        Measure = MeasurementTwo.Measure,
                        Id = MeasurementTwo.Id,
                        MeasureDate = MeasurementTwo.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    if (measurementRequesttwo.Measure != null)
                    {
                        await datos.Insert(measurementRequesttwo);
                    }
                    var measurementRequestthree = new MeasurementRequest
                    {
                        Measure = MeasurementThree.Measure,
                        Id = MeasurementThree.Id,
                        MeasureDate = MeasurementThree.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    if (measurementRequestthree.Measure != null)
                    {
                        await datos.Insert(measurementRequestthree);
                    }
                    await App.Current.MainPage.DisplayAlert(
                       "Listo", "Medidas creadas", "Aceptar");
                    IsEnabled = true;
                    IsRunning = false;


                }
                else
                {
                    await SaveAsync();

                    IsEnabled = false;
                    IsRunning = true;

                    var request = new TokenRequest
                    {
                        Password = Clave,
                        Username = Usuario,

                    };

                    var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
                    var token = response.Result;

                    var measurementRequestone = new MeasurementRequest
                    {
                        Measure = MeasurementOne.Measure,
                        Id = MeasurementOne.Id,
                        MeasureDate = MeasurementOne.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    var datos = new DataService();
                    Response<object> response2;
                    if (measurementRequestone.Measure != null)
                    {
                        if (IsEdit)
                        {
                            response2 = await _apiService.PutAsync(url, "/api", "/Measurements", measurementRequestone.Id, measurementRequestone, "bearer", token.Token);
                        }
                        else
                        {
                            response2 = await _apiService.PostAsync(url, "/api", "/Measurements", measurementRequestone, "bearer", token.Token);

                        }
                        if (!response2.IsSuccess)
                        {

                            await App.Current.MainPage.DisplayAlert("Error", "Hay Problema", "Aceptar");
                            IsEnabled = true;
                            IsRunning = false;
                            return;

                        }
                    }
                    
                    var measurementRequesttwo = new MeasurementRequest
                    {
                        Measure = MeasurementTwo.Measure,
                        Id = MeasurementTwo.Id,
                        MeasureDate = MeasurementTwo.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    if (measurementRequesttwo.Measure != null)
                    {
                        if (IsEdit)
                        {
                            response2 = await _apiService.PutAsync(url, "/api", "/Measurements", measurementRequesttwo.Id, measurementRequesttwo, "bearer", token.Token);
                        }
                        else
                        {
                            response2 = await _apiService.PostAsync(url, "/api", "/Measurements", measurementRequesttwo, "bearer", token.Token);

                        }
                        if (!response2.IsSuccess)
                        {

                            await App.Current.MainPage.DisplayAlert("Error", "Hay Problema", "Aceptar");
                            IsEnabled = true;
                            IsRunning = false;
                            return;

                        }
                    }
                    var measurementRequestthree = new MeasurementRequest
                    {
                        Measure = MeasurementThree.Measure,
                        Id = MeasurementThree.Id,
                        MeasureDate = MeasurementThree.MeasureDate,
                        FlowerId = flower.Id,

                    };
                    if (measurementRequestthree.Measure != null)
                    {
                        if (IsEdit)
                        {
                            response2 = await _apiService.PutAsync(url, "/api", "/Measurements", measurementRequestthree.Id, measurementRequestthree, "bearer", token.Token);
                        }
                        else
                        {
                            response2 = await _apiService.PostAsync(url, "/api", "/Measurements", measurementRequestthree, "bearer", token.Token);

                        }
                        if (!response2.IsSuccess)
                        {

                            await App.Current.MainPage.DisplayAlert("Error", "Hay Problema", "Aceptar");
                            IsEnabled = true;
                            IsRunning = false;
                            return;

                        }
                    }
                    await App.Current.MainPage.DisplayAlert(
                       "Listo", "Medidas creadas", "Aceptar");
                    IsEnabled = true;
                    IsRunning = false;
                }
            }
        }
        private async Task SaveAsync()
        {
            IsEnabled = false;
            IsRunning = true;

            try
            {
                DataService dataService = new DataService();
                Measurelist = await dataService.GetMeasurers();
                if (Measurelist.Count == 0)
                {
                    // await App.Current.MainPage.DisplayAlert("Alert", "No hay medidas pendiendes para guardar", "Aceptar");

                    IsEnabled = true;
                    IsRunning = false;
                }
                else
                {
                    var url = App.Current.Resources["UrlAPI"].ToString();

                    var request = new TokenRequest
                    {
                        Password = Clave,
                        Username = Usuario,

                    };

                    var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
                    var token = response.Result;

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
                            IsEnabled = true;
                            IsRunning = false;
                            return;

                        }

                    }

                    await dataService.DeleteAllMeasurers();
                    // await App.Current.MainPage.DisplayAlert(
                    //             "Listo", "Medidas sincronizadas corretamente", "Aceptar");

                    IsEnabled = true;
                    IsRunning = false;
                }



            }
            catch (Exception ex)
            {
            }

        }
        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(MeasurementOne.Measure) && string.IsNullOrEmpty(MeasurementTwo.Measure) && string.IsNullOrEmpty(MeasurementThree.Measure))
            {
                await App.Current.MainPage.DisplayAlert("Faltan Medidas", "Debe agregar al menos una medida para guardar", "Aceptar");
                return false;
            }
            else if (string.IsNullOrEmpty(MeasurementOne.Measure) || string.IsNullOrEmpty(MeasurementTwo.Measure) || string.IsNullOrEmpty(MeasurementThree.Measure))
            {
                bool answer = await ValidateEmptyAsync();
                return answer;
            }

            if (MeasurementOne.MeasureDate == null || MeasurementTwo.MeasureDate == null || MeasurementThree.MeasureDate == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error no hay fecha", "Aceptar");
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateEmptyAsync()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Faltan Medidas", "¿ Esta seguro que desea guardar ?", "Aceptar", "Cancelar");
            Debug.WriteLine("Respuesta: " + answer);
            if (answer == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Measure"))
            {
                MeasurementOne = parameters.GetValue<MeasurementResponse>("Measure");
                MeasurementTwo = parameters.GetValue<MeasurementResponse>("Measure");
                MeasurementThree = parameters.GetValue<MeasurementResponse>("Measure");
                IsEdit = true;
                Title = "Editar Medida";

            }
            else
            {
                MeasurementOne = new MeasurementResponse { MeasureDate = DateTime.Today };
                MeasurementTwo = new MeasurementResponse { MeasureDate = DateTime.Today };
                MeasurementThree = new MeasurementResponse { MeasureDate = DateTime.Today };

                IsEdit = false;
                Title = "Nueva Medida";

            }
        }
    }
}
