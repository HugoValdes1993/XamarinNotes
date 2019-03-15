using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApp.Util;

namespace XamarinApp.ViewModels
{
    public class Persona_dto 
    {
        public string NombrePersona { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumRut { get; set; }
        public string DigVer { get; set; }

        private static IFileReadWrite ServicioArchivos = DependencyService.Get<IFileReadWrite>();

        #region Listar
        public async Task<Persona_dto> ListarTodoAsync()
        {

            string Json = await ServicioArchivos.ReadFromFile("persona.json");

            Persona_dto persona = JsonConvert.DeserializeObject<Persona_dto>(Json);

            return persona;

        }
        #endregion

        #region Grabar en Json
        public async Task<bool> GrabarAsync(Persona_dto persona)
        {

            string json = JsonConvert.SerializeObject(persona, Formatting.Indented);
            bool Grabo = await ServicioArchivos.WriteToFile(json,"persona.json");

            return Grabo;
        }
        #endregion
    }
}
