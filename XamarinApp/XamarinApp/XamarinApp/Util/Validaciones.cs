using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Util
{
   public class Validaciones
    {
        #region Validacion Rut
        public bool esRutValido(string RUT, string DV)
        {

            var rut = RUT;
            var longitud = rut.Length;
            var factor = 2;
            var sumaProducto = 0;
            var con = 0;
            var caracter = 0;

            for (con = longitud - 1; con >= 0; con--)
            {
                caracter = Int32.Parse(rut.Substring(con, 1));
                sumaProducto += (factor * caracter);
                factor++; if (factor > 7) factor = 2;

            }

            var digitoAuxiliar = 11 - (sumaProducto % 11);
            var caracteres = "-123456789K0";
            var digitoCaracter = caracteres.Substring(digitoAuxiliar, 1);

            return DV.ToUpper().Equals(digitoCaracter);

        }
        #endregion

    }
}
