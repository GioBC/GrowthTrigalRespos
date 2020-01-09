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
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private string _clave;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _ingresarCommand;
        private DataService _dataService;


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
        public string Usuario { get; set; } // propiedades
        private List<UPResponse> farm { get; set; }
        private List<TokenRequest> Userlist { get; set; }
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

        private async void IngresarAsync()
        {
            //IsRunning = true;
            //IsEnabled = false;

            if (string.IsNullOrEmpty(Usuario))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes ingresar el usuario", "Aceptar");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(Clave))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Debes ingresar la clave", "Aceptar");
                    return;
                }
                else
                {
                    //var url = App.Current.Resources["UrlAPI"].ToString();
                    //var connection = await _apiService.CheckConnectionAsync(url);

                    //if (!connection)
                    //{
                    bool isSucces = false;

                    IsRunning = true;
                    IsEnabled = false;

                    await LoadDataFromDBAsync();
                    IsEnabled = true;
                    IsRunning = false;

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
                        
                        //await App.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta, pruebe conectandose a la red o valide la información", "Aceptar");
                        await SincronizarAsync();

                        IsEnabled = true;
                        IsRunning = false;
                        return;

                    }
                
                    //else
                    //{
                        
                    //    await LoadDataFromDBAsync();
                    //    await LoadUserData();
                    //}
                }
            }
        }
        private async Task LoadDataFromDBAsync()
        {
            IsEnabled = true;
            IsRunning = false;

            farm = await _dataService.GetAllHomes();
            Userlist = await _dataService.GetUser();


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
            foreach (TokenRequest item in Userlist)
            {
                var user = item.Username;
                var pwd = item.Password;

                if (item.Username == Usuario && item.Password == Clave)
                {

                    var farm2 = farm;


                    var parameters = new NavigationParameters
                                 {
                                    {"farm1", farm2 }
                                 };
                    await _navigationService.NavigateAsync("HomesPage", parameters);
                }


            }
        }
    }

}



