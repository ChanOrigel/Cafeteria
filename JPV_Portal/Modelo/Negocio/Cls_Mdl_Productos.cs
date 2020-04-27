using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Productos : TablaDB
    {
        public String Producto_ID { get; set; }
        public String Producto { get; set; }
        public String Categoria_ID { get; set; }
        public String Categoria { get; set; }
        public String Precio { get; set; }
        public String Usuario_Creo { get; set; }
        public String Fecha_Creo { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } 

        public Cls_Mdl_Productos() { } 

        public Cls_Mdl_Productos(string Producto_ID, String Producto = "", String Categoria_ID ="", String Categoria = "", String Precio = "", String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Creo = "", String Fecha_Creo = "")
        {
            this.Producto_ID = Producto_ID;
            this.Producto = Producto;
            this.Categoria_ID = Categoria_ID;
            this.Categoria = Categoria;
            this.Precio = Precio;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Creo = Usuario_Creo;
            this.Fecha_Creo = Fecha_Creo;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Productos"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Producto_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Producto_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Producto_ID.ToString()))
                parametrosBD.Add(new ParametroBD("Producto_ID", Producto_ID));
            parametrosBD.Add(new ParametroBD("Producto", Producto));
            parametrosBD.Add(new ParametroBD("Precio", Precio));
            if (!String.IsNullOrEmpty(Categoria_ID.ToString()))
                parametrosBD.Add(new ParametroBD("Categoria_ID", Categoria_ID));

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Creo)) {
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Creo));
                parametrosBD.Add(new ParametroBD("Fecha_Creo", "Getdate()"));
            }
            else if (!String.IsNullOrEmpty(Usuario_Modifico))
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));

            //Retornamos la lista.
            return parametrosBD;
        }
    }
}