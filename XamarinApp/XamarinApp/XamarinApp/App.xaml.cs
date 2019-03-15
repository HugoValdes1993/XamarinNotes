using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels;
using XamarinApp.views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinApp
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }

        public static Usuario UsuarioLogeado { get; set; }

        public static bool EstadoConexion { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new Login();

            EstadoConexion = CrossConnectivity.Current.IsConnected;

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChangedAsync;
        }

        private async void Current_ConnectivityChangedAsync(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {

                EstadoConexion = true;
                await new Trabajo_dto().ProcesarTareasPendientesAsync();
            }
            else
            {

                EstadoConexion = false;

            }

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            if (MasterDetail == null)
            {

                MainPage = new Login();

            }
            else
            {

                MainPage = MasterDetail;

            }
            
        }
    }
}
