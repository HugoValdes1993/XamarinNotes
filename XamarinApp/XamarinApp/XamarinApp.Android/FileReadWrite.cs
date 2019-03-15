using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;

using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using XamarinApp.Droid;
using XamarinApp.Util;

[assembly:Xamarin.Forms.Dependency(typeof(FileReadWrite))]

namespace XamarinApp.Droid
{
    public class FileReadWrite : IFileReadWrite
    {

        #region Leer desde archivo
        public async Task<string> ReadFromFile(string nombreArchivo)
        {
            string Resultado = string.Empty;
            TextReader Lector = null;

            try
            {
                var PathDocumento = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                var PathFile = Path.Combine(PathDocumento, nombreArchivo);

                Lector = new StreamReader(PathFile);

                if (File.Exists(PathFile))
                {

                    Resultado = await Lector.ReadToEndAsync();

                }

            }
            catch(System.IO.FileNotFoundException ex)
            {

                throw new FileNotFoundException("Archivo no encontrado", ex.InnerException);

            }
            finally
            {

                if (Lector != null)
                {

                    Lector.Close();

                }

            }

            return Resultado;
        }
        #endregion

        #region Escribir archivo json
        public async Task<bool> WriteToFile(string Texto, string nombreArchivo)
        {

            bool Resultado = true;
            TextWriter Escritor = null;

            try
            {
                string NombreArchivo = nombreArchivo;
                string PathDocumento = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                var PathFile = Path.Combine(PathDocumento, NombreArchivo);
              
                var file = File.Open(PathFile, FileMode.Create, FileAccess.Write);
                Escritor = new StreamWriter(file);
                await Escritor.WriteAsync(Texto);

            }
            catch (Exception ex)
            {

                throw new Exception("Un error ha ocurrido al crear el archivo", ex.InnerException);
                 
            }
            finally
            {

                if (Escritor != null)
                {
                    Escritor.Close();
                }

            }

            return Resultado;

        }
        #endregion

        #region Eliminar archivo
        public bool DeleteFile(string nombreArchivo)
        {

            bool elimino = true;

            try
            {
                var PathDocumento = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                var PathFile = Path.Combine(PathDocumento, nombreArchivo);


                if (File.Exists(PathFile))
                {

                    File.Delete(PathFile);

                }

            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar leer archivo", ex.InnerException);

            }


            return elimino;

        }
        #endregion

    }

}