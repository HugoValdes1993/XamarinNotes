using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace XamarinApp.Util
{
    public class Metodos
    {
        public bool HayConexion()
        {

            string huesped = "http://www.google.com";

            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(huesped))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool HayConexion2()
        {
            string huesped = "http://www.google.com";

            try
            {
                return new Ping().Send(huesped).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }

        }

        public bool CheckConexion()
        {
            bool StatusConexion = new Metodos().HayConexion();

            if (StatusConexion == false)
            {

                bool StatusConexion2 = new Metodos().HayConexion2();

                if (StatusConexion2 == false)
                {

                    return false;

                }

                return false;

            }

            return true;

        }

    }

}
