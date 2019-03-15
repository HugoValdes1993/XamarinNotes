using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Util;
using XamarinApp.ViewModels;
using Plugin.Connectivity;
using System.Windows.Input;

namespace XamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarTrabajos : ContentPage
    {

        private bool statusInternet = new Metodos().CheckConexion();

        public ListarTrabajos ()
		{

			InitializeComponent ();

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            ListadoTrabajos();
         

            listTrabajos.RefreshCommand = new Command(() => {
                listTrabajos.IsRefreshing = true;
                ListadoTrabajos();
                listTrabajos.IsRefreshing = false;
            });

        }     

        #region Metodos
        public async void ListadoTrabajos()
        {
            if (App.EstadoConexion)
            {
                List<Trabajo_dto> _trabajos = await new Trabajo_dto().GetTrabajosAsync();
                listTrabajos.ItemsSource = null;
                listTrabajos.ItemsSource = _trabajos;

            }
            else
            {

                List<Trabajo_dto> _trabajos = await new Trabajo_dto().ListarTrabajosNoInternetAsync();
                listTrabajos.ItemsSource = null;
                listTrabajos.ItemsSource = _trabajos;

            }

           
        }

        private void BtnAgregarTrabajo_Clicked(object sender, EventArgs e)
        {

            App.MasterDetail.IsPresented = false;
            App.MasterDetail.Detail.Navigation.PushAsync(new AgregarTrabajo());

        }


        #endregion

        private void ListTrabajos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {

                Trabajo_dto TrabajoSelec = e.SelectedItem as Trabajo_dto;

                App.MasterDetail.IsPresented = false;
                App.MasterDetail.Detail.Navigation.PushAsync(new EditarTrabajo(TrabajoSelec));

            }

        }
    }

}