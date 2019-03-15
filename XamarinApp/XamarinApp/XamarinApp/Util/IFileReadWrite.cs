using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinApp.Util
{
     public interface IFileReadWrite
     {

        Task<bool> WriteToFile(string text,string nombreArchivo);
        Task<string> ReadFromFile(string nombreArchivo);
        bool DeleteFile(string nombreArchivo);

     }
}
