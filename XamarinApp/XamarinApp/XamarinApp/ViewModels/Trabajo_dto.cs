using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using XamarinApp.Util;

namespace XamarinApp.ViewModels
{
    public class Trabajo_dto
    {

        public bool Status { get; set; }
        public string IDTrabajo { get; set; }
        public string CodTrabajo { get; set; }
        public string Descripcion { get; set; }
        public string FechaTrabajo { get; set; }
        public string AccionAsync { get; set; }

        private static HttpClient cliente = new HttpClient();

        private static IFileReadWrite ServicioArchivos = DependencyService.Get<IFileReadWrite>();


        #region Listar
        public async System.Threading.Tasks.Task<List<Trabajo_dto>> GetTrabajosAsync()
        {

            cliente.BaseAddress = new Uri(Constantes.IPApiServer);

            string url = string.Format(Constantes.URICrudTrabajos);

            var response = await cliente.GetAsync(url);

            var result = response.Content.ReadAsStringAsync().Result;

            List<Trabajo_dto> _trabajos =  JsonConvert.DeserializeObject<List<Trabajo_dto>>(result);

            string json = JsonConvert.SerializeObject(_trabajos, Formatting.Indented);
            ServicioArchivos.DeleteFile("trabajostemp.json");
            await ServicioArchivos.WriteToFile(json,"trabajostemp.json");

            return _trabajos;

        }
        #endregion

        #region ListarNoInternet
        public async System.Threading.Tasks.Task<List<Trabajo_dto>> ListarTrabajosNoInternetAsync()
        {
            string jsonTrabajos = await ServicioArchivos.ReadFromFile("trabajostemp.json");

            List<Trabajo_dto> trabajosNoInternet = JsonConvert.DeserializeObject<List<Trabajo_dto>>(jsonTrabajos);

            return trabajosNoInternet;

        }
        #endregion

        #region Modificar
        public async System.Threading.Tasks.Task<bool> ModificarTrabajoAsync()
        {

            var parametros = new FormUrlEncodedContent(new[]
            {

                new KeyValuePair<string, string>("IDTrabajo", IDTrabajo),
                new KeyValuePair<string, string>("CodTrabajo", CodTrabajo),
                new KeyValuePair<string, string>("DescrTrabajo", Descripcion),
                new KeyValuePair<string, string>("FechaRealiza", FechaTrabajo)

            });

            cliente.BaseAddress = new Uri(Constantes.IPApiServer);

            string url = string.Format(Constantes.URICrudTrabajos);

            var response = await cliente.PutAsync(url, parametros);

            var result = response.Content.ReadAsStringAsync().Result;

            return (int.Parse(result) == 1);

        }
        #endregion

        #region Crear
        public async System.Threading.Tasks.Task<bool> CrearTrabajoAsync()
        {

            var parametros = new FormUrlEncodedContent(new[]
            {

                new KeyValuePair<string, string>("IDTrabajo", IDTrabajo),
                new KeyValuePair<string, string>("CodTrabajo", CodTrabajo),
                new KeyValuePair<string, string>("DescrTrabajo", Descripcion),
                new KeyValuePair<string, string>("FechaRealiza", FechaTrabajo)

            });

            cliente.BaseAddress = new Uri(Constantes.IPApiServer);

            string url = string.Format(Constantes.URICrudTrabajos);

            var response = await cliente.PostAsync(url, parametros);

            var result = response.Content.ReadAsStringAsync().Result;

            return (int.Parse(result) == 1);

        }
        #endregion

        #region Eliminar
        public async System.Threading.Tasks.Task<bool> EliminarTrabajo()
        {

            cliente.BaseAddress = new Uri(Constantes.IPApiServer);

            string url = string.Format(Constantes.URIEliminarTrabajo + IDTrabajo);

            var response = await cliente.DeleteAsync(url);

            var result = response.Content.ReadAsStringAsync().Result;

            return (int.Parse(result) == 1);

        }
        #endregion

        #region Crear sin conexion
        public async System.Threading.Tasks.Task<bool> CrearTrabajoNoConexionAsync()
        {

            try
            {

                List<Trabajo_dto> _trabajos = await ListarTrabajosNoInternetAsync();
                AccionAsync = Constantes.ConsTareaInsert;
                _trabajos.Add(this);

                ServicioArchivos.DeleteFile("trabajostemp.json");
                string json = JsonConvert.SerializeObject(_trabajos, Formatting.Indented);
                bool Grabo = await ServicioArchivos.WriteToFile(json, "trabajostemp.json");

                return Grabo;

            }
            catch (NullReferenceException)
            {

                return false;

            }
            
        }
        #endregion

        #region Procesar colas pendientes sin conexion
        public async System.Threading.Tasks.Task ProcesarTareasPendientesAsync()
        {

            List<Trabajo_dto> listadoTrabajos = await ListarTrabajosNoInternetAsync();

            foreach (var trabajo in listadoTrabajos)
            {

                switch (trabajo.AccionAsync)
                {

                    case Constantes.ConsTareaInsert:
                            
                            await trabajo.CrearTrabajoAsync();
                            trabajo.AccionAsync = null;
                        break;

                    case Constantes.ConsTareaUpdate:
                             
                            await trabajo.ModificarTrabajoAsync();
                            trabajo.AccionAsync = null;
                        break;

                      case Constantes.ConsTareaDelete:
                       
                            await trabajo.EliminarTrabajo();
                            trabajo.AccionAsync = null;
                        break;

                }

            }

            ServicioArchivos.DeleteFile("trabajostemp.json");

        }
        #endregion

    }

}
