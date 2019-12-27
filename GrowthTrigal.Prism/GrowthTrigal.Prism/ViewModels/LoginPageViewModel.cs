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
        private string _clave; // atributo privado
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _ingresarCommand;
       // private DelegateCommand _SincronizarCommand;
        private DataService _dataService;


        public LoginPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService) //constructor 
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _dataService = new DataService();
            Title = "Flower Growth";
            IsEnabled = true;

            Usuario = "orlando.munar@globostudio.net";
            Clave = "123456";

            AliasFarm = "OL";

        }

        public DelegateCommand IngresarCommand => _ingresarCommand ?? (_ingresarCommand = new DelegateCommand(Ingresar));

        //public DelegateCommand SincronizarCommand => _SincronizarCommand ?? (_SincronizarCommand = new DelegateCommand(Ingresar));


        public bool IsRemember { get; set; }

        public string AliasFarm { get; set; }


        public string BlockNumber { get; set; }

        public string Usuario { get; set; } // propiedades

        //Test
        private List<UPResponse> farm { get; set; }
        private List<TokenRequest> Userlist { get; set; }
        private List<TokenRequest> Pwdlist { get; set; }

        private List<TokenResponse> Token { get; set; }





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



        private async void Ingresar()
        {
            if (string.IsNullOrEmpty(Usuario))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes ingresar el usuario", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Clave))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes ingresar la clave", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);

            if (!connection)
            {
                await LoadUsersFromDB();
                IsEnabled = true;
                IsRunning = false;
                //// App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
          
                try
                {
                    if (Userlist.FirstOrDefault().Username.ToString() == Usuario && Pwdlist.FirstOrDefault().Password.ToString() == Clave)
                    {
                        var farm2 = farm;
                        var parameters = new NavigationParameters
                    {
                         {"farm1", farm2 }
                    };
                        await _navigationService.NavigateAsync("HomesPage", parameters);

                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta, pruebe sincronizando conectandote a la red o valide la información", "Aceptar");
                    return;
                }

            }
            else
            {

                var answer = await this.LoadUsersFromAPI();
                //    if (answer)
                //    {
                //        this.SaveUsersToDB();
                //        IsEnabled = true;
                //        IsRunning = false;
                //    }
            }




        }
        private async Task LoadUsersFromDB()
        {
            farm = await _dataService.GetAllHomes();
            Userlist = await _dataService.GetUser();
            Pwdlist = await _dataService.GetPwd(); 
        }

        //private async Task SaveUsersToDB()
        //{
        //    await _dataService.DeleteAllUsers();
        //    _dataService.Insert(this.Userlist);

        //}

        private async Task<bool> LoadUsersFromAPI()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
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
                await App.Current.MainPage.DisplayAlert("Error", "User or password incorrect", "Aceptar");
                Clave = string.Empty;
                return false;
            }
            
            await _dataService.DeleteAllUsers();
            _dataService.Insert(request);


            var token = response.Result;
            var response3 = await _apiService.GetUPByEmailAsync(
                url,
                "api",
                "/Homes/GetUPByEmail",
                "bearer",
                token.Token,
                AliasFarm);

            _dataService.Insert(token);

            if (!response3.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problem with user data", "Aceptar");
                return false;
            }

            var farm = response3.Result;
            await _dataService.DeleteAllUpsHomes();
            _dataService.Insert(farm);

            //Settings.Farm = JsonConvert.SerializeObject(this);
            //Settings.Token = JsonConvert.SerializeObject(token);


            var parameters = new NavigationParameters
            {
                {"farm", farm }
            };


            await _navigationService.NavigateAsync("HomesPage", parameters);
            IsRunning = false;
            IsEnabled = true;
            return true;
        }
    }
}


