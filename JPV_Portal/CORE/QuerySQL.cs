using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPV_Portal.CORE
{
    public class QuerySQL
    {
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: GeneraCMDExec
        ///DESCRIPCIÓN: Método que sirve para generar un comando SQL en base a la tabla que se indique.
        ///PARAMETROS:  objetoTabla:Implementacion de la interfaz de objeto tabla de la cual se desea generar el comando.
        ///             modoDeCaptura: Modo de captura en el cual se desea generar el comando.
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static String GeneraCMDExec(TablaDB Tabla, MODO_DE_CAPTURA Captura, params FiltroBD[] filtros)
        {
            String Query = String.Empty;
            String Campo_Añadir = String.Empty;
            String Campo_Valor = String.Empty;
            String Campo_Llave = String.Empty;
            //Obtenemos los parametros.
            List<ParametroBD> parametrosBD = Tabla.ObtenParametros();

            //variable para comparar con la cantidad de filtros que nos ayudara asignar el operador condicional            
            //Vaidamos el modo de captura.


            //QUERY SECUNDARIO
            if (filtros.Count() > 0)
            {
                foreach (FiltroBD filtro in filtros)
                {
                    foreach (TablaDB tabla in filtro.Tabla)
                    {
                        Campo_Añadir = String.Empty;
                        Campo_Valor = String.Empty;
                        Campo_Llave = String.Empty;
                        Query += "\n";
                        parametrosBD = tabla.ObtenParametros();
                        //variable para comparar con la cantidad de filtros que nos ayudara asignar el operador condicional            
                        //Vaidamos el modo de captura.
                        switch (filtro.Captura)
                        {
                            case MODO_DE_CAPTURA.CAPTURA_ALTA:
                                //Creamos la linea base.
                                Query += "INSERT INTO " + tabla.NombreTabla;
                                //Generamos el comando.
                                foreach (ParametroBD parametro in parametrosBD)
                                {
                                    Campo_Añadir += parametro.CampoBD + ",";
                                    Campo_Valor += TipoDeDatoBD(parametro.ValorBD) + ",";
                                }
                                // añadimos los valores a la consulta
                                Query += "(" + Campo_Añadir.Remove(Campo_Añadir.Length - 1) + ") VALUES (" + Campo_Valor.Remove(Campo_Valor.Length - 1) + ")";
                                break;

                            case MODO_DE_CAPTURA.CAPTURA_ACTUALIZA:
                                //Creamos la linea base.
                                Query += "UPDATE " + tabla.NombreTabla + " SET ";
                                //Generamos el comando.
                                foreach (ParametroBD parametro in parametrosBD)
                                {
                                    if (parametro.CampoBD != tabla.IDTabla)
                                    {
                                        Campo_Valor = TipoDeDatoBD(parametro.ValorBD).ToString();
                                        Campo_Añadir += parametro.CampoBD + " = " + Campo_Valor + ",";
                                    }
                                    else
                                        Campo_Llave = TipoDeDatoBD(parametro.ValorBD).ToString();
                                }
                                // añadimos los valores a la consulta
                                Query += Campo_Añadir.Remove(Campo_Añadir.Length - 1) + " WHERE " + tabla.IDTabla + " = " + Campo_Llave;
                                break;
                            case MODO_DE_CAPTURA.CAPTURA_ELIMINAR:
                                Query += "DELETE FROM " + tabla.NombreTabla + " WHERE " + tabla.IDTabla + " = " + TipoDeDatoBD(ObtenValorDeParametro(parametrosBD, tabla.IDTabla));
                                break;
                        }
                    }
                }
            }
            else
            {
                switch (Captura)
                {
                    case MODO_DE_CAPTURA.CAPTURA_ALTA:
                        //Creamos la linea base.
                        Query = "INSERT INTO " + Tabla.NombreTabla;
                        //Generamos el comando.
                        foreach (ParametroBD parametro in parametrosBD)
                        {
                            if (parametro.ValorBD != null)
                            {
                                Campo_Añadir += parametro.CampoBD + ",";
                                Campo_Valor += TipoDeDatoBD(parametro.ValorBD) + ",";
                            }
                        }
                        // añadimos los valores a la consulta
                        Query += "(" + Campo_Añadir.Remove(Campo_Añadir.Length - 1) + ") VALUES (" + Campo_Valor.Remove(Campo_Valor.Length - 1) + ")";
                        break;

                    case MODO_DE_CAPTURA.CAPTURA_ACTUALIZA:
                        //Creamos la linea base.
                        Query = "UPDATE " + Tabla.NombreTabla + " SET ";
                        //Generamos el comando.
                        foreach (ParametroBD parametro in parametrosBD)
                        {
                            if (parametro.CampoBD != Tabla.IDTabla)
                            {
                                if (parametro.ValorBD != null)
                                {
                                    Campo_Valor = TipoDeDatoBD(parametro.ValorBD).ToString();
                                    Campo_Añadir += parametro.CampoBD + " = " + Campo_Valor + ",";
                                }
                            }
                            else
                                Campo_Llave = TipoDeDatoBD(parametro.ValorBD).ToString();
                        }
                        // añadimos los valores a la consulta
                        Query += Campo_Añadir.Remove(Campo_Añadir.Length - 1) + " WHERE " + Tabla.IDTabla + " = " + Campo_Llave;
                        break;
                    case MODO_DE_CAPTURA.CAPTURA_ELIMINAR:
                        //Creamos la linea base. Delete from Cat_Clientes where Cliente_ID=1
                        Query = "DELETE from " + Tabla.NombreTabla;
                        //Generamos el comando.
                        foreach (ParametroBD parametro in parametrosBD)
                        {
                            if (parametro.CampoBD != Tabla.IDTabla)
                            {

                            }
                            else
                                Campo_Llave = TipoDeDatoBD(parametro.ValorBD).ToString();
                        }
                        // añadimos los valores a la consulta
                        Query += " WHERE " + Tabla.IDTabla + " = " + Campo_Llave;
                        break;
                }


            }
            //Retornamos el comando.
            return Query;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN: Método auxiliar para obtener el valor de un parámetro de una lista de parámetros.
        ///PARAMETROS:  parametros: Lista de parámetros en la cual se buscará el valor.
        ///             parametroABuscar: Nombre del parámetro del cual se quiere obtener el valor.
        ///CREO:        CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static Object ObtenValorDeParametro(IEnumerable<ParametroBD> parametros, String parametroABuscar)
        {
            //Declaramos el objeto a retornar.
            Object valor = "0";
            //Buscamos el parámetro.
            foreach (ParametroBD iParam in parametros)
            {
                if (iParam.CampoBD.Trim().ToLower().Equals(parametroABuscar.Trim().ToLower()))
                {
                    valor = iParam.ValorBD;
                    break;
                }
            }
            //Retornamos el objeto.
            return valor;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: TipoDeDatoBD
        ///DESCRIPCIÓN: Método Auxiliar para obtener el tipo de dato de BD en base al tipo de dato del objeto.
        ///PARAMETROS:  dato: Dato del cual se va a obtener el tipo de dato de BD.
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public static Object TipoDeDatoBD(Object dato)
        {
            //Enteros.
            if (dato is Int64)
                return dato;
            if (dato is Int32)
                return dato;
            if (dato is Int16)
                return dato;
            if (dato is Decimal)
                return dato;
            //Cadenas
            if (dato is String)
            {
                if (dato.ToString() == "GETDATE()")
                    return dato;
                else
                    return "'" + dato.ToString().Trim() + "'";
            }
            //Fechas
            if (dato is DateTime)
                return "'" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dato) + "'";
            //Valores moneda.
            if (dato is Double)
                return dato;
            //Por default, cadenas.
            return dato;
        }

    }
}