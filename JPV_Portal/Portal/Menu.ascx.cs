using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using LitJson;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;

namespace JPV_Portal.Portal
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Header_Menu"] == null)
                    Crear_Menu_Sistema();
                else
                    Lbl_Menu.Text = Session["Header_Menu"].ToString();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error al cargar la configuración inicial de la página. Error: [" + Ex.Message + "]");
            }
        }
        
        public void Crear_Menu_Sistema()
        {
            StringBuilder MENU_SISTEMA = new StringBuilder();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            List<Menus> Listado = null;
            IEnumerable<Menus> Listado_Menus = null; 
            try
            {
                if (!String.IsNullOrEmpty(Sessiones.Tipo_Usuario))
                {

                    MENU_SISTEMA.Append("<nav id='Nav_Menu'>");
                    MENU_SISTEMA.Append("<div id='panel-menu'>");
                    MENU_SISTEMA.Append("<ul>");

                    if (Sessiones.Tipo_Usuario.Equals("Administrador"))
                    {
                        StreamReader r = new StreamReader(Server.MapPath("../Portal/Administrador.json"));
                        String json = r.ReadToEnd();
                        Listado = JsonConvert.DeserializeObject<List<Menus>>(json);
                    }
                    else
                    {
                        StreamReader r = new StreamReader(Server.MapPath("../Portal/Empleado.json"));
                        String json = r.ReadToEnd();
                        Listado = JsonConvert.DeserializeObject<List<Menus>>(json);
                    }

                    Listado_Menus = from menu in Listado
                                    where menu.parent_id == 0
                                    orderby menu.orden ascending
                                    select menu;

                    if (Listado_Menus != null)//aqui viene la lista del menu que tenga parent_id=0, esto quiere decir, los titulos de las categorias
                    {
                        foreach (var Menu_Padre in Listado_Menus) //analiza uno a uno las categorias
                        {
                            if (String.IsNullOrEmpty(Menu_Padre.enlace.ToString()))//solo permite los enlaces que esten vacios, por si hay algun colado
                            {
                                var Listado_Submenus = from menus in Listado //De la lista del menu original se trae una lista de submenus que tengan el parent_id
                                                       where menus.parent_id.ToString().Equals(Menu_Padre.menu_id.ToString())//igual al menu_id de la categoria
                                                       orderby menus.orden ascending// y los ordena del menor al mayor por su menu_id
                                                       select menus;

                                foreach (var Submenu in Listado_Submenus)
                                {
                                    MENU_SISTEMA.Append("<li>");
                                    MENU_SISTEMA.Append("<a href='" + Submenu.enlace + "'>" + Submenu.nombre + "</a>");
                                    MENU_SISTEMA.Append("</li>");
                                }
                            }

                        }

                        MENU_SISTEMA.Append("</ul>");
                        MENU_SISTEMA.Append("</div>");
                        //CUENTA
                        MENU_SISTEMA.Append("<div id='panel-account'>");
                        MENU_SISTEMA.Append("<ul>");
                        //MENU_SISTEMA.Append("<li><a href='#'>Perfil</a></li>");
                        MENU_SISTEMA.Append("<li><a  id='btn_cerrar' href='#'>Salir</a></li>");
                        MENU_SISTEMA.Append("</ul>");
                        MENU_SISTEMA.Append("</div>");
                        MENU_SISTEMA.Append("</nav>");
                        //Ligamos el menú construido con el ctrl que lo mostrara en pantalla al usuario.
                        Lbl_Menu.Text = MENU_SISTEMA.ToString();
                        Session["Header_Menu"] = MENU_SISTEMA.ToString();
                    }

                }
            
            }catch (Exception Ex)
            {
                throw new Exception("Error al ejecutar la construcción del menú del sistema. Error: [" + Ex.Message + "]");
            }
        }
    }
}