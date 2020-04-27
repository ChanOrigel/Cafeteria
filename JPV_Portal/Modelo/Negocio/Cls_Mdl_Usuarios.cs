using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Usuarios : TablaDB
    {
        public String Usuario_ID     { get; set; }
        public String Usuario         { get; set; }
        public String Email          { get; set; }
        public String Password       { get; set; }
        public String Rol           { get; set; }
        public String Estatus        { get; set; }
        public String Usuario_Creo   { get; set; }
        public String Usuario_Modifico   { get; set; }
        public String Fecha_Creo     { get; set; }
        public String Fecha_Modifico     { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Usuarios() { }

        public Cls_Mdl_Usuarios(string Usuario_ID, String Usuario = "", String Email = "", String Password = "", String Estatus = "",
            String Rol = "", String Usuario_Creo = "", String Fecha_Creo="", String Usuario_Modifico = "", String Fecha_Modifico="")
        {
            this.Usuario_ID = Usuario_ID;
            this.Usuario = Usuario;
            this.Email = Email;
            this.Password = Password;
            this.Estatus = Estatus;
            this.Rol = Rol;
            this.Usuario_Creo = Usuario_Creo;
            this.Fecha_Creo = Fecha_Creo;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;

        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Usuarios"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Usuario_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Usuario_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Usuario_ID))
                parametrosBD.Add(new ParametroBD("Usuario_ID", Usuario_ID));
            parametrosBD.Add(new ParametroBD("Usuario", Usuario));
            parametrosBD.Add(new ParametroBD("Email", Email));
            parametrosBD.Add(new ParametroBD("Password", Password));
            parametrosBD.Add(new ParametroBD("Rol", Rol));
            parametrosBD.Add(new ParametroBD("Estatus", Estatus));
            if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA)) { 
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Creo));
                parametrosBD.Add(new ParametroBD("Fecha_Creo", Fecha_Creo));
            }
            else
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}