using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPV_Portal.CORE
{
    /// <summary>
    /// Interfaz que ayudara a homogenizar los objetos tabla para sus operaciones en la BD.
    /// 
    /// Autor: CHANTAL ORIGEL
    /// Fecha: 
    /// </summary>
    public interface TablaDB
    {
        /// <summary>
        /// Nombre de la tabla en la BD.
        /// </summary>
        String NombreTabla { get; }
        /// <summary>
        /// Nombre del id de la tabla.
        /// </summary>
        String IDTabla { get; }
        /// <summary>
        /// Valor para ordenar los datos
        /// </summary>
        String Orderby { get; }
        /// <summary>
        /// Método para obtener los parámetros para insertar en la BD.
        /// </summary>
        /// <returns>Lista de parámetros para interactuar con la BD.</returns>
        List<ParametroBD> ObtenParametros();
    }    
}