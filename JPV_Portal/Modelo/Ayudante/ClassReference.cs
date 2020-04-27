using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace JPV_Portal.Modelo.Ayudante
{
    public class Sessiones
    {
        private static String P_Usuario_ID = "Usuario_ID";
        private static String P_Usuario = "Usuario";        
        private static String P_Email = "Email";        
        private static String P_Tipo_Usuario = "Rol";

        public static String Usuario_ID
        {
            get
            {
                if (HttpContext.Current.Session[Sessiones.P_Usuario_ID] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Sessiones.P_Usuario_ID].ToString();
            }
            set
            {
                HttpContext.Current.Session[Sessiones.P_Usuario_ID] = value;
            }
        }
        public static String Usuario
        {
            get
            {
                if (HttpContext.Current.Session[Sessiones.P_Usuario] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Sessiones.P_Usuario].ToString();
            }
            set
            {
                HttpContext.Current.Session[Sessiones.P_Usuario] = value;
            }
        }
       
        public static String Email
        {
            get
            {
                if (HttpContext.Current.Session[Sessiones.P_Email] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Sessiones.P_Email].ToString();
            }
            set
            {
                HttpContext.Current.Session[Sessiones.P_Email] = value;
            }
        }
        public static String Tipo_Usuario
        {
            get
            {
                if (HttpContext.Current.Session[Sessiones.P_Tipo_Usuario] == null)
                    return String.Empty;
                else
                    return HttpContext.Current.Session[Sessiones.P_Tipo_Usuario].ToString();
            }
            set
            {
                HttpContext.Current.Session[Sessiones.P_Tipo_Usuario] = value;
            }
        }
                            
    }

    public class Cls_Util
    {
        public static String Crear_Ruta(String Carpeta)
        {
            DirectoryInfo Ruta;
            Ruta = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Temporal/" + Carpeta + "/"));
            if (!Ruta.Exists)
                Ruta.Create();
            return Ruta.ToString();

        }
    }

    public class Menus
    {
        public int menu_id { set; get; }
        public int parent_id { set; get; }
        public int orden { set; get; }
        public String nombre { set; get; }
        public String enlace { set; get; }
        public String logo { set; get; }
        public String rol { set; get; }
    }

    public class Respuesta
    {
        public String Mensaje { get; set; }
        public String Registros { get; set; }
        public String Tabla_Registros { get; set; }
        public Boolean Estatus { get; set; }
        public String data { get; set; }
    }

    public class Select2
    {
        public int id { set; get; }
        public String nombre { set; get; }
    }
}