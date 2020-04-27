using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace JPV_Portal.Modelo.Ayudante
{
    public class ConexionBD
    {
        public static string BD = ConfigurationManager.ConnectionStrings["JPVWEB"].ConnectionString;  
    }
}