using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPV_Portal.CORE
{
    /// <summary>
    /// Estructura que representa un filtro para una consulta a la BD.
    /// 
    /// Autor: CHANTAL ORIGEL
    /// Fecha: 03/08/2016
    /// </summary>
    public struct FiltroBD
    {
        /// <summary>
        /// Nombre del campo en la BD.
        /// </summary>
        public List<TablaDB> Tabla;
        /// <summary>
        /// Valor que tendra el campo.
        /// </summary>
        public MODO_DE_CAPTURA Captura;
        /// <summary>
        /// Inicializa una nueva instancia del parámetro con los valores proporcionados.
        /// </summary>       
        public FiltroBD(List<TablaDB> Tabla = null, MODO_DE_CAPTURA Captura = MODO_DE_CAPTURA.CAPTURA_ALTA)
        {
            this.Tabla = Tabla;
            this.Captura = Captura;
        }
    }
    /// <summary>
    /// Estructura auxiliar para manejar campos de clases en BD.
    /// </summary>
    public struct ParametroBD
    {
        //Variables privadas que contendran los valores de las propiedades
        private String campoBD;
        private Object valorBD;
        /// <summary>
        /// Nombre del campo en la BD.
        /// </summary>
        public String CampoBD
        {
            get { return campoBD; }
            set { campoBD = value; }
        }
        /// <summary>
        /// Valor que tendra el campo.
        /// </summary>
        public Object ValorBD
        {
            get { return valorBD; }
            set { valorBD = value; }
        }
        /// <summary>
        /// Inicializa una nueva instancia del parámetro con los valores proporcionados.
        /// </summary>
        /// <param name="campoBD">Nombre del campo en la BD.</param>
        /// <param name="aliasCampoBD">Alias del campo.</param>
        /// <param name="valorBD">Valor del campo en la BD.</param>
        public ParametroBD(String campoBD = "", Object valorBD = null)
        {
            this.campoBD = campoBD;
            this.valorBD = valorBD;
        }
    }
    /// <summary>
    /// Enumeración que indicara el modo de captura en los formularios, 
    /// CAPTURA_NUEVO Indicara una nueva insercción en la BD
    /// CAPTURA_ACTUALIZACION Indicara una actualización a algún registro.
    /// CAPTURA_ELIMA Indicara Eliminar a algún registro.
    /// 
    /// Autor: Francisco Javier Becerra Toledp
    /// Fecha: 01/08/2016
    /// </summary>
    public enum MODO_DE_CAPTURA
    {
        CAPTURA_ALTA,
        CAPTURA_ACTUALIZA,
        CAPTURA_ELIMINAR
    }
}