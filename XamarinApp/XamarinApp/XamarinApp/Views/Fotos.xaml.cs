using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Fotos : ContentPage
	{
		public Fotos ()
		{
			InitializeComponent ();
		}


        #region Boton Tomar 
        private async void BtnTomarFoto_ClickedAsync(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                Directory = "Trabajo 1",
                Name = "test.jpg"
            });

        }
        #endregion

    }
}