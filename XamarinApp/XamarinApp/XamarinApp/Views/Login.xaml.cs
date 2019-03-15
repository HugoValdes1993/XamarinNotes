using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels;
using XamarinApp.Util;
using Plugin.Connectivity;
using Newtonsoft.Json;

namespace XamarinApp.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{

			InitializeComponent();

		}

        private bool statusInternet = new Metodos().CheckConexion();

        #region Evento on Appearing
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () =>
            {

                await System.Threading.Tasks.Task.Delay(250);
                txtLogin.Focus();

                if (!statusInternet)
                {

                    lblSinConexion.Text = "Modo sin conexión";

                }

            });

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

        }
     
        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {

                lblSinConexion.Text = "";
                statusInternet = true;
            }
            else
            {

                lblSinConexion.Text = "Modo sin conexión";
                statusInternet = false;

            }

        }
        #endregion


        #region Evento Login
        private async void Button_Clicked(object sender, EventArgs e)
        {

            string User = txtLogin.Text;
            string Pass = txtPassword.Text;

            btnLogin.IsEnabled = false;

            Indicador.IsRunning = true;

            if (statusInternet)
            {

                HttpClient cliente = new HttpClient();

                cliente.BaseAddress = new Uri(Constantes.IPApiServer);

                string url = string.Format("api/usuario/login.php?username={0}&password={1}", User, Pass);

                var response = await cliente.GetAsync(url);

                var result = response.Content.ReadAsStringAsync().Result;

                Indicador.IsRunning = false;
                btnLogin.IsEnabled = true;

                Usuario UsuariLogin = JsonConvert.DeserializeObject<Usuario>(result);

                if (UsuariLogin.Status == false)
                {

                    await DisplayAlert("Login", "Usuario o contraseña incorrectos!", "Aceptar");

                }
                else
                {

                    await UsuariLogin.GuardarUsuarioTemp(UsuariLogin);

                    App.UsuarioLogeado = UsuariLogin;

                    MasterDetailPage fpm = new MainPage();
                    Application.Current.MainPage = fpm;

                }

            }
            else
            {
                Usuario usuario = new Usuario
                {

                    Password = Pass,
                    Username = User

                };

                usuario = await usuario.LoginSinConexionAsync();

                if (usuario == null)
                {

                    await DisplayAlert("Login", "Usuario o contraseña incorrectos!", "Aceptar");

                }
                else
                {

                    App.UsuarioLogeado = usuario;

                    MasterDetailPage fpm = new MainPage();
                    Application.Current.MainPage = fpm;

                }


            }
          
        }
        #endregion

    }

}