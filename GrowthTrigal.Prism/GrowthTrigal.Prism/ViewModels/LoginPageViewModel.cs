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
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private string _clave;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _ingresarCommand;
        private DataService _dataService;
        private bool _isEdit;


        public LoginPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _dataService = new DataService();
            Title = "Flower Growth";
            IsEnabled = true;

            AliasFarm = "OL";

        }
        public DelegateCommand IngresarCommand => _ingresarCommand ?? (_ingresarCommand = new DelegateCommand(IngresarAsync));
        public bool IsRemember { get; set; }
        public string AliasFarm { get; set; }
        public string BlockNumber { get; set; }
        public string Usuario { get; set; }
        public string fecha { get; set; }// propiedades
        private List<UPResponse> farm { get; set; }
        private List<TokenRequest> Userlist { get; set; }
        private List<MeasurementRequest> Measurelist { get; set; }
        public string Clave
        {
            get => _clave;
            set => SetProperty(ref _clave, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value); // refresca la interfaz de usuario
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value); // refresca la interfaz de usuario
        }
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        private async void IngresarAsync()
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Clave))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes ingresar el usuario y contraseña", "Aceptar");
                return;
            }
            else
            {
                IsRunning = true;
                IsEnabled = false;

                await LoadDataFromDBAsync();

                try
                {
                    if (Userlist.Count == 0)
                    {

                        if (await CheckConnectionAsync() == true)
                        {
                            await SincronizarAsync();
                            await SaveAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Es tu primer ingreso a la app, Debes tener conexion a internet para cargar informacion", "Aceptar", "Cancelar");
                            IsRunning = false;
                            IsEnabled = true;
                        }
                    }
                    else
                    {
                        var dateDaystr = DateTime.Now.ToString("yyyy/MM/dd");
                        var dateDay = Convert.ToDateTime(dateDaystr);
                        var dateHour = DateTime.Now.Hour;
                        var dateystr = Userlist.LastOrDefault().fecha.ToString("yyyy/MM/dd");
                        var datey = Convert.ToDateTime(dateystr);

                        if (dateDay > datey && dateHour > 5)
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Tienes una actualizacion pendiente!, conecta tu dispositivo a internet e intenta ingresar para cargar nueva información,. Si ya tienes conexion espera la sincronizacion", "Aceptar");
                            var url = App.Current.Resources["UrlAPI"].ToString();
                            var connection = await _apiService.CheckConnectionAsync(url);


                            if (!connection)
                            {

                                bool answer = await App.Current.MainPage.DisplayAlert("Alert", "No tienes conexion, ¿Desea continuar sin realizar la sincronizacion", "Aceptar", "Cancelar");
                                Debug.WriteLine("Respuesta: " + answer);
                                if (answer == true)
                                {
                                    await LoadUserData();
                                }
                            }
                            else
                            {
                                await SincronizarAsync();
                                await SaveAsync();
                            }
                        }
                        else
                        {
                            await LoadUserData();
                        }
                    }

                }
                catch (Exception ex)
                {
                }


            }
        }
        private async Task LoadDataFromDBAsync()
        {

            Userlist = await _dataService.GetUser();
            farm = await _dataService.GetAllHomes();
            
        }
        public async Task SincronizarAsync()
        {
            try
            {

                IsRunning = true;
                IsEnabled = false;
                var url = App.Current.Resources["UrlAPI"].ToString();
                var connection = await _apiService.CheckConnectionAsync(url);

                if (!connection)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Verifique datos ingresados o valide su conexion a internet para realizar sincronizacion si estos son correctos", "Aceptar");
                }
                else
                {

                    var request = new TokenRequest
                    {
                        Password = Clave,
                        Username = Usuario,
                        BlockNumber = BlockNumber,
                        AliasFarm = AliasFarm,
                        fecha = DateTime.Now,

                    };

                    var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);


                    if (!response.IsSuccess)
                    {
                        IsRunning = false;
                        IsEnabled = true;
                        await App.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecto, o no existe en base de datos, validar info o contactar con administrador", "Aceptar");
                        Clave = string.Empty;
                    }
                    else
                    {


                        _dataService.Insert(request);


                        var token = response.Result;
                        var response3 = await _apiService.GetUPByEmailAsync(
                            url,
                            "api",
                            "/Homes/GetUPByEmail",
                            "bearer",
                            token.Token,
                            AliasFarm);


                        if (!response3.IsSuccess)
                        {
                            IsRunning = false;
                            IsEnabled = true;
                            await App.Current.MainPage.DisplayAlert("Error", "Problem with user data", "Aceptar");
                        }
                        else
                        {
                            IsRunning = false;
                            IsEnabled = true;

                            var farm = response3.Result;

                            await _dataService.DeleteAllUpsHomes();
                            await _dataService.Insert(farm);
                            await LoadDataFromDBAsync();

                            var parameters = new NavigationParameters
                            {
                                      {"farm", farm }
                            };

                            await App.Current.MainPage.DisplayAlert("Alert", "Datos sincronizados correctamente", "Aceptar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error inesperado, intente ingresar nuevamente", "Aceptar");
            }
        }
        public async Task LoadUserData()
        {
            IsEnabled = true;
            IsRunning = false;
            bool isSucces = false;

            foreach (TokenRequest item in Userlist)
            {
                var user = item.Username;
                var pwd = item.Password;

                if (item.Username == Usuario && item.Password == Clave)
                {
                    isSucces = true;
                    var farm2 = farm;


                    var parameters = new NavigationParameters
                                 {
                                    {"farm1", farm2 }
                                 };
                    await _navigationService.NavigateAsync("HomesPage", parameters);

                }
            }
            if (isSucces == false)
            {

                await App.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta, pruebe conectandose a la red o valide la información / Si esta conectado espere sincronizacion", "Aceptar");
                await SincronizarAsync();

                IsEnabled = true;
                IsRunning = false;
                return;

            }
        }
        public async Task<bool> CheckConnectionAsync()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);

            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No tienes conexion, Debes realizar una primera conexion para empezar a utilizar la aplicacion", "Aceptar");
                return false;
            }

            return true;
        }
        private async Task SaveAsync()
        {
            IsEnabled = false;
            IsRunning = true;

            var request = new TokenRequest
            {
                Password = Clave,
                Username = Usuario,

            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);
            var token = response.Result;

            try
            {
                DataService dataService = new DataService();
                Measurelist = await dataService.GetMeasurers();
                if (Measurelist.Count == 0)
                {
                    IsEnabled = true;
                    IsRunning = false;
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
                            IsEnabled = true;
                            IsRunning = false;
                            return;

                        }

                    }

                    await dataService.DeleteAllMeasurers();
                    await App.Current.MainPage.DisplayAlert(
                                 "Listo", "Medidas pendientes fueron sincronizadas corretamente", "Aceptar");

                    IsEnabled = true;
                    IsRunning = false;
                }



            }
            catch (Exception ex)
            {
            }

        }
    }

}



