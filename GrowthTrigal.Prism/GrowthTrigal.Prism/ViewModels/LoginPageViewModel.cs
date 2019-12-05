using GrowthTrigal.Common.Helpers;
using GrowthTrigal.Common.Models;
using MyLeasing.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

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

        public LoginPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService) //constructor 
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Flower Growth";
            IsEnabled = true;

            //Usuario = "orlando.munar@globostudio.net";
            //Clave = "123456";

            AliasFarm = "OL";
    
        }

        public DelegateCommand IngresarCommand => _ingresarCommand ?? (_ingresarCommand = new DelegateCommand(Ingresar));


        public bool IsRemember { get; set; }

        public string AliasFarm { get; set; }

       

        public string BlockNumber { get; set; }

        public string Usuario { get; set; } // propiedades

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
                //Settings.Farm = JsonConvert.SerializeObject(this);
                //Settings.Token = JsonConvert.SerializeObject(token);
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

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
                return;
            }

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
                return;
            }

            var farm = response3.Result;

            //Settings.Farm = JsonConvert.SerializeObject(this);
            //Settings.Token = JsonConvert.SerializeObject(token);


            var parameters = new NavigationParameters
            {
                {"farm", farm }
            };


            await _navigationService.NavigateAsync("HomesPage", parameters);
            IsRunning = false;
            IsEnabled = true;


            //await _navigationService.NavigateAsync("HomesPage");
            //IsRunning = false;
            //IsEnabled = true;


        }
    }

}
