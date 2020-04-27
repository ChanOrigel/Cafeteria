using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Balance : TablaDB
    {
        public String Balance_ID { get; set; }
        public String Inicio_Caja { get; set; }
        public String Fin_Caja { get; set; }
        public String Ventas { get; set; }
        public String GastoExtra { get; set; }
        public String Descripcion { get; set; }
        public String Inicio_Actualizado { get; set; }
        public String Fecha_Inicio { get; set; }
        public String Fecha_Fin { get; set; }
        public String Usuario_Creo { get; set; }
        public String Fecha_Creo { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_Balance() { } //PARA QUE SIRVE ESTO

        public Cls_Mdl_Balance(String Balance_ID = "", String Inicio_Caja = "", String Fin_Caja = "", String Ventas = "", String Inicio_Actualizado = "",
            String Usuario_Creo = "", String Fecha_Creo = "", String Usuario_Modifico = "", String Fecha_Modifico = "")
        {
            this.Balance_ID = Balance_ID;
            this.Inicio_Caja = Inicio_Caja;
            this.Fin_Caja = Fin_Caja;
            this.Ventas = Ventas;
            this.Inicio_Actualizado = Inicio_Actualizado;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Creo = Usuario_Creo;
            this.Fecha_Creo = Fecha_Creo;
        }
        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Ope_Historial_Abonos"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Abono_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Abono_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            //if (!String.IsNullOrEmpty(Abono_ID))
            //    parametrosBD.Add(new ParametroBD("Abono_ID", Abono_ID));
            //parametrosBD.Add(new ParametroBD("Folio", Folio));
            //parametrosBD.Add(new ParametroBD("Cliente", Cliente));
            //parametrosBD.Add(new ParametroBD("Cantidad", Cantidad));

            ////if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            //if (!String.IsNullOrEmpty(Usuario_Registro))
            //    parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            //else
            //{
            //    parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
            //    //parametrosBD.Add(new ParametroBD("Fecha_Modifico", "Getdate()"));
            //}
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}