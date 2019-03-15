using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Util;
using XamarinApp.ViewModels;

namespace XamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarTrabajo : ContentPage
	{
        private bool statusConexion = new Metodos().CheckConexion();

        #region Costructor y bind cambio de red
        public AgregarTrabajo ()
		{

			InitializeComponent ();


        }

      
        #endregion

        #region Guardar evento
        private async void BtnGuardar_ClickedAsync(object sender, EventArgs e)
        {

            bool confirmar = await DisplayAlert("Crear","¿Está seguro de crear este registro?","Sí","No");

            if (confirmar)
            {
                bool inserto = false;

                Trabajo_dto trabajo = new Trabajo_dto
                {

                    IDTrabajo = txtIDTrabajo.Text,
                    CodTrabajo = txtCodTrabajo.Text,
                    Descripcion = txtDescTrabajo.Text,
                    FechaTrabajo = dateFechaTrabajo.Date.ToString()

                };

                if (App.EstadoConexion)
                {
                  
                    inserto = await trabajo.CrearTrabajoAsync();

                }
                else
                {

                    inserto = await trabajo.CrearTrabajoNoConexionAsync();

                }

                if (inserto)
                {
                    await DisplayAlert("Grabar", "Trabajo correctamente creado", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Grabar", "Error al crear trabajo", "Aceptar");
                }

            }

        }
        #endregion

        private void BtnLimpiar_Clicked(object sender, EventArgs e)
        {

            txtCodTrabajo.Text = string.Empty;
            txtIDTrabajo.Text = "0";
            txtDescTrabajo.Text = string.Empty;
            dateFechaTrabajo.Date = DateTime.Today;

        }

    }

}