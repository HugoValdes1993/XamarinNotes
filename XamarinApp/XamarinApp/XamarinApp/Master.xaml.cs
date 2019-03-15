using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.views;
using XamarinApp.Views;

namespace XamarinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{
		public Master ()
		{

			InitializeComponent ();

		}

        private void BtnAgregarPersona_Clicked(object sender, EventArgs e)
        {

            App.MasterDetail.IsPresented = false;
            App.MasterDetail.Detail.Navigation.PushAsync(new Persona());

        }

        private void BtnSacarFoto_Clicked(object sender, EventArgs e)
        {

            App.MasterDetail.IsPresented = false;
            App.MasterDetail.Detail.Navigation.PushAsync(new Fotos());

        }

        private void BtnTrabajos_Clicked(object sender, EventArgs e)
        {
            App.MasterDetail.IsPresented = false;
            App.MasterDetail.Detail.Navigation.PushAsync(new ListarTrabajos());

        }

        private void BtnLogout_Clicked(object sender, EventArgs e)
        {

            App.UsuarioLogeado = null;

            Login fpm = new Login();
            Application.Current.MainPage = fpm;

        }
    }
}