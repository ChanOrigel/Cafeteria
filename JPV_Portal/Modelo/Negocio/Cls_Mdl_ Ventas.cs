using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Ventas : TablaDB
    {
        public String Venta_ID { get; set; }
        public String Venta_Detalle_ID { get; set; }
        public String Folio { get; set; }
        public String Subtotal { get; set; }
        public String IVA { get; set; }
        public String Total { get; set; }
        public String Producto_ID { get; set; }
        public String Cantidad { get; set; }
        public String Precio { get; set; }
        public String Importe { get; set; }

        public String Dia { get; set; }
        public String Fecha { get; set; }
        public String Descripcion { get; set; }

        public String Usuario_Creo { get; set; }
        public String Fecha_Creo { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Ventas() { }

        public Cls_Mdl_Ventas(String Venta_ID = "", String Venta_Detalle_ID = "", String Folio = "",
            String Subtotal = "", String IVA = "", String Total = "", String Producto_ID = "", String Cantidad = "",
            String Precio = "", String Importe = "", String Usuario_Creo = "",
            String Fecha_Creo = "")
        {
            this.Venta_ID = Venta_ID;
            this.Venta_Detalle_ID = Venta_Detalle_ID;
            this.Folio = Folio;
            this.Subtotal = Subtotal;
            this.IVA = IVA;
            this.Total = Total;
            this.Producto_ID = Producto_ID;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Importe = Importe;

            this.Usuario_Creo = Usuario_Creo;
            this.Fecha_Creo = Fecha_Creo;

        }
        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Ope_Ventas"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Venta_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Venta_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            //if (!String.IsNullOrEmpty(Venta_ID))
            //    parametrosBD.Add(new ParametroBD("Venta_ID", Venta_ID));
            //if (!String.IsNullOrEmpty(Cliente))
            //    parametrosBD.Add(new ParametroBD("Cliente", Cliente));
            //parametrosBD.Add(new ParametroBD("Total_Vendido", Total_Vendido));
            //parametrosBD.Add(new ParametroBD("Estatus", Estatus));
            //parametrosBD.Add(new ParametroBD("Folio", Folio));
            //if (!String.IsNullOrEmpty(Factura))
            //    parametrosBD.Add(new ParametroBD("Factura", Factura));

            ////if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            //if (!String.IsNullOrEmpty(Usuario_Registro))
            //    parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            //else
            //{
            //    parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
            //    //parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Modifico));
            //}
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}