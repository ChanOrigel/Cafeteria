using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using LitJson;
using System.Web.Security;
using System.Web.UI.WebControls;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Ayudante;

namespace JPV_Portal.Portal
{
    public partial class WebSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String Accion = String.Empty;
            String Json_Cadena = String.Empty;
            Response.Clear();
            if (this.Request.QueryString["Accion"] != null)
            {
                if (!String.IsNullOrEmpty(this.Request.QueryString["Accion"].ToString().Trim()))
                {
                    Accion = this.Request.QueryString["Accion"].ToString().Trim();
                    switch (Accion)
                    {
                        case "Autentificar":
                            Json_Cadena = Autentificar();
                            Response.ContentType = "application/json";
                            break;                       
                        case "RecuperarUsuario":
                            Json_Cadena = RecuperarUsuario();
                            Response.ContentType = "application/json";
                            break;                       
                        case "Cerrar":
                            Json_Cadena = Cerrar();
                            Response.ContentType = "application/json";
                            break;
                    }
                }
            }

            Response.Write(Json_Cadena);
            Response.Flush();
            Response.End();
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: AutentificarUsuario
        ///DESCRIPCIÓN: VALIDA AUTENTIFICACION
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        private String Autentificar()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Usuarios Obj_Negocio = new Cls_Mdl_Usuarios();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            String Json_Object = String.Empty;
            DataTable Dt_Registros = new DataTable();
            String Str_Json = String.Empty;

            try
            {
                Json_Object = HttpUtility.HtmlDecode(HttpContext.Current.Request["Param"] == null ? String.Empty : HttpContext.Current.Request["Param"].ToString().Trim());
                Obj_Negocio = JsonMapper.ToObject<Cls_Mdl_Usuarios>(Json_Object);
                Dt_Registros = Controlador.Consulta_Usuario(Obj_Negocio);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                    {
                        if (Dt_Registros.Rows[0]["Estatus"].ToString().Trim().ToUpper() == "ACTIVO")
                        {
                            Obj_Respuesta.Estatus = true;
                            Sessiones.Usuario_ID = Dt_Registros.Rows[0]["Usuario_ID"].ToString().Trim();
                            Sessiones.Usuario = Dt_Registros.Rows[0]["Usuario"].ToString().Trim().ToUpper();                            
                            Sessiones.Email = Dt_Registros.Rows[0]["Email"].ToString().Trim();
                            Sessiones.Tipo_Usuario = Dt_Registros.Rows[0]["Rol"].ToString().Trim();
                            FormsAuthentication.Initialize();
                        }
                        else
                        {
                            Obj_Respuesta.Estatus = false;
                            Obj_Respuesta.Mensaje = "Favor de comunicarse con el área administrativa, para activar su Login";
                        }
                    }
                    else
                    {
                        Obj_Respuesta.Estatus = false;
                        Obj_Respuesta.Mensaje = "Favor de introducir sus datos correctos.";
                    }
                }
                else
                {
                    Obj_Respuesta.Estatus = false;
                    Obj_Respuesta.Mensaje = "Favor de introducir sus datos correctos.";
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Informe técnico - [No se encontró el servidor o éste no estaba accesible]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Recuperar_Usuario
        ///DESCRIPCIÓN: 
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        ///
        private String RecuperarUsuario()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Usuarios Obj_Negocio = new Cls_Mdl_Usuarios();
            ServicioCorreo Obj_Correo = new ServicioCorreo();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            String Json_Object = String.Empty;
            String Mensaje = String.Empty;
            DataTable Dt_Registros = new DataTable();
            DataTable Dt_Parametros = new DataTable();

            try
            {
                Json_Object = HttpUtility.HtmlDecode(HttpContext.Current.Request["Param"] == null ? String.Empty : HttpContext.Current.Request["Param"].ToString().Trim());
                Obj_Negocio = JsonMapper.ToObject<Cls_Mdl_Usuarios>(Json_Object);
                Dt_Registros = Controlador.Consulta_Usuario(Obj_Negocio);
                Dt_Parametros = Controlador.Consulta_Parametros();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                    {
                        if (Dt_Registros.Rows[0]["Estatus"].ToString().Trim().ToUpper() == "ACTIVO")
                        {
                            Mensaje = Obj_Correo.Texto=(Dt_Registros.Rows[0]["Password"].ToString().Trim());
                            if (Obj_Correo.RecuperarPassword(Dt_Registros.Rows[0]["Email"].ToString().Trim(), Mensaje, "Cafeteria - Recuperación de contraseña."))
                            {
                                Obj_Respuesta.Estatus = true;
                                Obj_Respuesta.Mensaje = "Su contraseña fue enviada a su correo electrónico, si no se encuentra en la bandeja de entrada, favor de buscarla en los correos spam.";
                            }
                            else
                            {
                                Obj_Respuesta.Estatus = false;
                                Obj_Respuesta.Mensaje = "No se pudo enviar la contraseña, Por favor intente de nuevo.";
                            }
                        }
                        else
                        {
                            Obj_Respuesta.Estatus = false;
                            Obj_Respuesta.Mensaje = "Favor de comunicarse con el área administrativa para activar su login.";
                        }
                    }
                    else
                    {
                        Obj_Respuesta.Estatus = false;
                        Obj_Respuesta.Mensaje = "Favor de introducir el correo de empleado correcto.";
                    }
                }
                else
                {
                    Obj_Respuesta.Estatus = false;
                    Obj_Respuesta.Mensaje = "El correo no existe en nuestros registros";
                }

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "El correo no existe en nuestros registros";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }

            return Json_Resultado;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Autenticar_Empleado
        ///DESCRIPCIÓN: VALIDA AUTENTIFICACION
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        protected string Cerrar()
        {
            Respuesta Mensaje = new Respuesta();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Mensaje.Estatus = true;
            Mensaje.Mensaje = "logout";
            return LitJson.JsonMapper.ToJson(Mensaje);
        }
    }
}