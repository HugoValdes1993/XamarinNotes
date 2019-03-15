using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using XamarinApp.Util;

namespace XamarinApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Persona : ContentPage
	{
		public Persona ()
		{
			InitializeComponent ();
		}

        #region Agregar Persona
        private async void BtnAgregarPersona_Clicked(object sender, EventArgs e)
        {

            string Nombre = txtNombre.Text;
            string NumRut = txtNumRut.Text;
            DateTime FechaNacimiento = dateFechaNac.Date;
            string DigVer = cboDigVer.SelectedItem.ToString();

            bool esRutValido = new Validaciones().esRutValido(NumRut,DigVer);

            if (!esRutValido)
            {
                await DisplayAlert("Grabación", "Rut no valido", "Aceptar");
                return;
            }

            Persona_dto persona = new Persona_dto
            {
                NombrePersona = Nombre,
                NumRut = NumRut,
                FechaNacimiento = FechaNacimiento,
                DigVer = DigVer
            };

           bool Grabo = await persona.GrabarAsync(persona);

            if (Grabo)
            {
                await DisplayAlert("Grabación", "Registro realizado correctamente" , "Aceptar");

                LimpiarForm();

            }
            else
            {
                await DisplayAlert("Grabación" , "Error al grabar registro" , "Aceptar");
            }

        }
        #endregion

        public void LimpiarForm()
        {

            txtNombre.Text = string.Empty;
            txtNumRut.Text = string.Empty;
            cboDigVer.SelectedIndex = 0;
            dateFechaNac.Date = DateTime.Today;

        }


    }
}