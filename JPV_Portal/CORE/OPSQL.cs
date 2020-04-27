using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using JPV_Portal.Modelo.Ayudante;

namespace JPV_Portal.CORE
{
    public class OPSQL
    {
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: MasterObject
        ///DESCRIPCIÓN: 
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean MasterObject(TablaDB Objeto, MODO_DE_CAPTURA Obj_Captura, params FiltroBD[] filtros)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();

            try
            {
                String Mi_SQL = QuerySQL.GeneraCMDExec(Objeto, Obj_Captura, filtros);

                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();
                Obj_Transaccion.Commit();
                Transaccion = true;
            }
            catch (Exception ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }
            return Transaccion;
        }
    }
}