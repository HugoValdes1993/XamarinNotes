using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinApp.Util;

namespace XamarinApp.ViewModels
{
   public class Usuario 
   {
        public bool Status { get; set; }
        public string Nombre { get; set; }
        public int IDUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }

        private static IFileReadWrite ServicioArchivos = DependencyService.Get<IFileReadWrite>();

        public async System.Threading.Tasks.Task<bool> GuardarUsuarioTemp(Usuario usuario)
        {

            string json = JsonConvert.SerializeObject(usuario, Formatting.Indented);
            bool Grabo = await ServicioArchivos.WriteToFile(json, "usuario.json");

            return Grabo;

        }

        public async System.Threading.Tasks.Task<Usuario> LoginSinConexionAsync()
        {

            string jsonUsuario = await ServicioArchivos.ReadFromFile("usuario.json");

            Usuario usuarioTemp = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            if (usuarioTemp.Username == Username && usuarioTemp.Password == Password)
            {

                return usuarioTemp;

            }

            return null;

        }

    }

}
