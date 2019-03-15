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

namespace XamarinApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarTrabajo : ContentPage
	{
		public EditarTrabajo (Trabajo_dto trabajo)
		{
            InitializeComponent();

            txtIDTrabajo.Text = trabajo.IDTrabajo;
            txtCodTrabajo.Text = trabajo.CodTrabajo;
            txtDescTrabajo.Text = trabajo.Descripcion;
            dateFechaTrabajo.Date = DateTime.Parse(trabajo.FechaTrabajo);

        }

        #region Añadir foto
        private async void BtnAñadirFoto_ClickedAsync(object sender, EventArgs e)
        {
            Carga.IsRunning = true;

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {

                Directory = txtCodTrabajo.Text,
                Name = "test.jpg"

            });

            if (photo != null)
            {

                await DisplayAlert("Tomar foto", "Foto correctamente guardada" ,"Aceptar");

            }
            else
            {

                await DisplayAlert("Tomar foto", "Error al guardar foto", "Aceptar");

            }

            Carga.IsRunning = false;

        }
        #endregion

        #region Modificar trabajo
        private async void BtnModificar_ClickedAsync(object sender, EventArgs e)
        {

            bool confirmar = await DisplayAlert("Modificar","¿Está seguro de modificar el registro?","Sí","No");

            if (confirmar)
            {

                Trabajo_dto trabajo = new Trabajo_dto()
                {

                    IDTrabajo = txtIDTrabajo.Text,
                    CodTrabajo = txtCodTrabajo.Text,
                    Descripcion = txtDescTrabajo.Text,
                    FechaTrabajo = dateFechaTrabajo.Date.ToString()

                };



                bool modifico = await trabajo.ModificarTrabajoAsync();

                if (modifico)
                {

                    await DisplayAlert("Modificación", "Trabajo correctamente modificado", "Aceptar");

                }
                else
                {

                    await DisplayAlert("Modificación", "Error al modificar trabajo", "Aceptar");

                }

            }

        }
        #endregion

        #region Eliminar trabajo
        private async void BtnEliminar_ClickedAsync(object sender, EventArgs e)
        {
            bool confirmacion = await DisplayAlert("Eliminar", "¿Está seguro de eliminar el registro?", "Si", "No");

            if (confirmacion)
            {

                Trabajo_dto trabajo = new Trabajo_dto()
                {
                    IDTrabajo = txtIDTrabajo.Text
                };

                bool elimino = await trabajo.EliminarTrabajo();

                if (elimino)
                {

                    await DisplayAlert("Eliminación", "Registro eliminado correctamente", "Aceptar");

                    App.MasterDetail.IsPresented = false;
                    await App.MasterDetail.Detail.Navigation.PushAsync(new ListarTrabajos());

                }
                else
                {

                    await DisplayAlert("Eliminación","Error al eliminar registro","Aceptar");

                }

            }

        }
        #endregion

    }

}