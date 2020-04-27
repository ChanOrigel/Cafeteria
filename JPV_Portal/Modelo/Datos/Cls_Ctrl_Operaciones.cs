using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using JPV_Portal.CORE;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.Modelo.Negocio;
using SharpContent.ApplicationBlocks.Data;
using System.Globalization;

namespace JPV_Portal.Modelo.Datos
{
    public class Cls_Ctrl_Operaciones
    {
        /// <summary>
        /// Auxiliar en la operacion de usuarios con la BD de SQLServer.
        /// </summary>
        private OPSQL Obj_OPSQL;
        /// <summary>
        /// Inicializa el controlador para capturar y registrar en la BD.
        /// </summary>
        public Cls_Ctrl_Operaciones()
        {
            //Inicializamos
            Obj_OPSQL = new OPSQL();
        }
        ///*******************************************************************************
        /// NOMBRE DE LA CLASE: InsertaActualiza
        /// DESCRIPCIÓN: METODO QUE INSERTA/ACTUALIZA/ELIMINA
        /// PARÁMETROS :     
        /// CREO       :  CHANTAL ORIGEL
        /// FECHA_CREO : 
        /// MODIFICO          :
        /// FECHA_MODIFICO    :
        /// CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean MasterRegistro(TablaDB Elemento, MODO_DE_CAPTURA Captura, params FiltroBD[] filtros)
        {
            try
            {
                //Variable de resultado.
                Boolean resultado = false;
                //Retornamos el resultado.
                resultado = Obj_OPSQL.MasterObject(Elemento, Captura, filtros);
                //Retornamos el resultado.
                return resultado;
            }
            catch (Exception exDB)
            {
                //Cachamos la excepcion de tipo ejecucion de comandos y vemos que acciones tomar
                throw new Exception(exDB.Message);
            }
        }
        ///*******************************************************************************
        /// NOMBRE DE LA CLASE: InsertaActualiza
        /// DESCRIPCIÓN: METODO QUE INSERTA/ACTUALIZA 
        /// PARÁMETROS :     
        /// CREO       :  CHANTAL ORIGEL
        /// FECHA_CREO : 
        /// MODIFICO          :
        /// FECHA_MODIFICO    :
        /// CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public int MasterCount(TablaDB Elemento, String Identificador)
        {
            String Sql = String.Empty;
            Object ID = 0;
            int Contador = 0;
            Sql = "SELECT COUNT(Identificador) FROM " + Elemento.NombreTabla + " WHERE Identificador ='" + Identificador + "'";
            try
            {
                ID = SqlHelper.ExecuteScalar(ConexionBD.BD, CommandType.Text, Sql);
                Contador = (int.Parse(ID.ToString()) + 1);
            }
            catch (Exception exDB)
            {
                //Cachamos la excepcion de tipo ejecucion de comandos y vemos que acciones tomar
                throw new Exception(exDB.Message);
            }

            return Contador;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS USUARIOS REGISTRADOR
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Usuario(Cls_Mdl_Usuarios Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Usuarios";

                if (!String.IsNullOrEmpty(Dato.Usuario))
                    Sql += " WHERE Usuario = '" + Dato.Usuario + "'";

                if (!String.IsNullOrEmpty(Dato.Password))
                {
                    if (Sql.Contains("WHERE"))
                        Sql += " AND Password = '" + Dato.Password + "'";
                    else
                        Sql += " WHERE Password = '" + Dato.Password + "'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Cliente()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Clientes";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Cliente_Salida()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Clientes where Bodega='S'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Cargar_Cajas()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Cajas";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }

        ///////////////////////////////////////////////////////////////////////CAFETERIA/////////////////////////////////////////////////////////////////////////////
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Consulta_Producto
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Productos()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Cat_Productos inner join Cat_Categorias on Cat_Productos.Categoria_ID=Cat_Categorias.Categoria_ID";
                //Sql += " where Producto like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Consulta_Producto
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Producto(string Dato, String Cat_ID = "")
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Cat_Productos inner join Cat_Categorias on Cat_Productos.Categoria_ID=Cat_Categorias.Categoria_ID";
                Sql += " where  Cat_Productos.Categoria_ID like '%" + Cat_ID + "%' and Producto like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Consulta_Producto
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Categorias(Cls_Mdl_Productos Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Cat_Categorias";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Consulta_Categorias
        ///DESCRIPCIÓN: CONSULTAR LAS CATEGORIAS PARA EL COMBO
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Categorias(String Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Categorias";
                Sql += " where Categoria like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Guardar_Productos
        ///DESCRIPCIÓN: GUARDAR LOS PRODUCTOS O CATEGORIAS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Guardar_Productos(Cls_Mdl_Productos Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(Datos.Producto)) //PRODUCTOS
                {
                    Sql = "INSERT INTO  Cat_Productos (";
                    Sql += " Producto, ";
                    Sql += " Categoria_ID, ";
                    Sql += " Precio, ";
                    //Sql += " Fecha_Creo, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Datos.Producto.ToString()) ? "null, " : "'" + Datos.Producto.ToString() + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Categoria_ID.ToString()) ? "null, " : "'" + Datos.Categoria_ID + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Precio.ToString()) ? "null, " : "'" + Datos.Precio + "', ");
                    //Sql += (String.IsNullOrEmpty(Datos.Fecha_Creo) ? "null, " : "'" + Datos.Fecha_Creo + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Creo) ? "null " : "'" + Datos.Usuario_Creo + "' ");
                    Sql += ")";
                }
                else //CATEGORIAS
                {
                    Sql = "INSERT INTO  Cat_Categorias (";
                    Sql += " Categoria, ";
                    //Sql += " Fecha_Creo, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Datos.Categoria.ToString()) ? "null, " : "'" + Datos.Categoria.ToString() + "', ");
                    //Sql += (String.IsNullOrEmpty(Datos.Fecha_Creo) ? "null, " : "'" + Datos.Fecha_Creo + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Creo) ? "null " : "'" + Datos.Usuario_Creo + "' ");
                    Sql += ")";
                }


                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                    Obj_Conexion.Close();
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Actualizar_Productos
        ///DESCRIPCIÓN: ACTUALIZA LOS PRODUCTOS O CATEGORIAS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Actualizar_Productos(Cls_Mdl_Productos Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(Datos.Producto)) //PRODUCTOS
                {
                    Sql = " UPDATE  Cat_Productos SET ";
                    Sql += " Producto = " + (String.IsNullOrEmpty(Datos.Producto) ? "null, " : "'" + Datos.Producto + "', ");
                    Sql += " Categoria_ID = " + (String.IsNullOrEmpty(Datos.Categoria_ID.ToString()) ? "null, " : "'" + Datos.Categoria_ID + "', ");
                    Sql += " Precio = " + (String.IsNullOrEmpty(Datos.Precio.ToString()) ? "null, " : "'" + Datos.Precio + "', ");
                    Sql += " Usuario_Modifico = " + (String.IsNullOrEmpty(Datos.Usuario_Modifico) ? "null, " : "'" + Datos.Usuario_Modifico + "' ");
                    Sql += " WHERE  Cat_Productos.Producto_ID = '" + Datos.Producto_ID + "'";

                }
                else //CATEGORIAS
                {
                    Sql = " UPDATE  Cat_Categorias SET ";
                    Sql += " Categoria = " + (String.IsNullOrEmpty(Datos.Categoria) ? "null, " : "'" + Datos.Categoria + "', ");
                    Sql += " Usuario_Modifico = " + (String.IsNullOrEmpty(Datos.Usuario_Modifico) ? "null, " : "'" + Datos.Usuario_Modifico + "' ");
                    Sql += " WHERE  Cat_Categorias.Categoria_ID = '" + Datos.Categoria_ID + "'";
                }


                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                    Obj_Conexion.Close();
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Eliminar_Productos
        ///DESCRIPCIÓN: ACTUALIZA LOS PRODUCTOS O CATEGORIAS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Eliminar_Productos(Cls_Mdl_Productos Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;
            String SqlCons = String.Empty;
            DataTable Dt_Registro = new DataTable();


            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            try
            {
                if (!String.IsNullOrEmpty(Datos.Producto_ID)) //PRODUCTOS
                {
                    Sql = " DELETE FROM Cat_Productos";
                    Sql += " WHERE  Cat_Productos.Producto_ID = '" + Datos.Producto_ID + "'";
                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                    Transaccion = true;

                }
                else //CATEGORIAS
                {
                    SqlCons = " Select * FROM Cat_Productos where Categoria_ID ='" + Datos.Categoria_ID + "'";
                    Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, SqlCons).Tables[0];

                    if (Dt_Registro.Rows.Count > 0)
                    {
                        Transaccion = false;
                    }
                    else
                    {
                        Sql = " DELETE FROM Cat_Categorias";
                        Sql += " WHERE  Cat_Categorias.Categoria_ID = '" + Datos.Categoria_ID + "'";
                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();
                        Transaccion = true;
                    }
                }
                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                    Obj_Conexion.Close();
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Parametros()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Parametros";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Ventas()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            var date = DateTime.Now.ToString("yyyy/MM/dd");
            try
            {
                Sql = "Select ";
                Sql += "convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " from Ope_Ventas";

                if (!String.IsNullOrEmpty(date))
                {
                    Sql += " where Ope_Ventas.Fecha_Creo >= '" + date + " 00:00:00'";
                    Sql += " and Ope_Ventas.Fecha_Creo <= '" + date + " 23:59:59'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Ventas_Detalles()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            var date = DateTime.Now.ToString("yyyy/MM/dd");
            try
            {
                Sql = "Select ";
                Sql += "convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " from Ope_Ventas inner join Ope_Ventas_Detalles on Ope_Ventas.Venta_ID=Ope_Ventas_Detalles.Venta_ID ";
                Sql += " inner join Cat_Productos on Ope_Ventas_Detalles.Producto_ID=Cat_Productos.Producto_ID ";
                Sql += " inner join Cat_Categorias on Cat_Productos.Categoria_ID=Cat_Categorias.Categoria_ID ";

                if (!String.IsNullOrEmpty(date))
                {
                    Sql += " where Ope_Ventas.Fecha_Creo >= '" + date + " 00:00:00'";
                    Sql += " and Ope_Ventas.Fecha_Creo <= '" + date + " 23:59:59'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Parametros()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Parametros";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Iniciar_Formulario()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT top 1* FROM Ope_Balance";
                Sql += " order by Balance_ID desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Actualizar(Cls_Mdl_Balance Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = " Update Ope_Balance SET ";
                Sql += " Inicio_Actualizado = " + (String.IsNullOrEmpty(Datos.Inicio_Actualizado) ? "null " : "'" + Datos.Inicio_Actualizado + "' ");
                Sql += " WHERE Balance_ID = " + Datos.Balance_ID;

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Enviar_Corte(Cls_Mdl_Balance Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "INSERT INTO  Ope_Balance (";
                Sql += " Inicio_Caja, ";
                Sql += " Fin_Caja, ";
                Sql += " Ventas, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                Sql += (String.IsNullOrEmpty(Datos.Inicio_Caja) ? "null, " : "'" + Datos.Inicio_Caja + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Fin_Caja) ? "null, " : "'" + Datos.Fin_Caja + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Ventas) ? "null, " : "'" + Datos.Ventas + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Creo) ? "null " : "'" + Datos.Usuario_Creo + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                    Obj_Conexion.Close();
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Ventas_Detalles_Fechas(Cls_Mdl_Balance Datos)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            var date = DateTime.Now.ToString("yyyy/MM/dd");

            try
            {
                Sql = "Select ";
                Sql += "convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " from Ope_Ventas inner join Ope_Ventas_Detalles on Ope_Ventas.Venta_ID=Ope_Ventas_Detalles.Venta_ID ";
                Sql += " inner join Cat_Productos on Ope_Ventas_Detalles.Producto_ID=Cat_Productos.Producto_ID ";
                Sql += " inner join Cat_Categorias on Cat_Productos.Categoria_ID=Cat_Categorias.Categoria_ID ";

                if (!String.IsNullOrEmpty(Datos.Fecha_Inicio))
                {
                    var fecha = Datos.Fecha_Inicio.Split('/');
                    var result = fecha[2]+"/"+fecha[0]+"/"+fecha[1];
                    //var result = DateTime.ParseExact(Datos.Fecha_Inicio, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    Sql += " where Ope_Ventas.Fecha_Creo >= '" + result.ToString() + " 00:00:00'";
                }
                else
                    Sql += " where Ope_Ventas.Fecha_Creo >= '" + date + " 00:00:00'";

                if (!String.IsNullOrEmpty(Datos.Fecha_Fin))
                {
                    var fecha = Datos.Fecha_Fin.Split('/');
                    var result = fecha[2] + "/" + fecha[0] + "/" + fecha[1];
                    //var result = DateTime.ParseExact(Datos.Fecha_Fin, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    Sql += " and Ope_Ventas.Fecha_Creo <= '" + result + " 23:59:59'";
                }
                else
                    Sql += " and Ope_Ventas.Fecha_Creo <= '" + date + " 23:59:59'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }







        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        //public DataTable Consulta_Producto_Entrada(String Dato)
        //{
        //    String Sql = String.Empty;
        //    DataTable Dt_Registro = new DataTable();
        //    try
        //    {
        //        Sql = "SELECT ";
        //        Sql += "convert(varchar, Ope_Entrada_Mercancia.Fecha_Creo, 103) as Fecha_Creo,  * ";
        //        Sql += " FROM Ope_Entrada_Mercancia where Estatus='Almacen'";
        //        Sql += " and Proveedor_Producto like '%" + Dato + "%'";
        //        Sql += " order by Ope_Entrada_Mercancia.Entrada_ID desc";

        //        Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw new Exception(Ex.Message);
        //    }
        //    return Dt_Registro;
        //}
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: GENERA UN FOLIO
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Nuevo_Folio()
        {
            String Sql = String.Empty;
            DataTable Dt_Registros = new DataTable();

            try
            {
                Sql = "SELECT TOP 1 * From Ope_Ventas";
                Sql += " Order by Venta_ID desc";

                Dt_Registros = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registros;
        }
        /////******************************************************************************* 
        /////NOMBRE DE LA FUNCIÓN:      Salvar_Lista
        /////DESCRIPCIÓN:       Guardar o actualizar los items de la tabla
        /////PARAMETROS:  
        /////CREO:      MARIA CHANTAL ORIGEL SEGURA
        /////FECHA_CREO:  
        /////MODIFICO: 
        /////FECHA_MODIFICO:
        /////CAUSA_MODIFICACIÓN:
        /////*******************************************************************************
        internal Boolean Guardar_Venta(Cls_Mdl_Ventas Datos, List<Cls_Mdl_Ventas> Lista)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            try
            {
                Sql = "INSERT INTO Ope_Ventas (";
                Sql += " Folio, ";
                Sql += " Subtotal, ";
                Sql += " IVA, ";
                Sql += " Total, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                Sql += (String.IsNullOrEmpty(Datos.Folio) ? "null, " : "'" + Datos.Folio + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Subtotal) ? "null, " : "'" + Datos.Subtotal + "', ");
                Sql += (String.IsNullOrEmpty(Datos.IVA) ? "null, " : "'" + Datos.IVA + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Total) ? "null, " : "'" + Datos.Total + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Creo) ? "null " : "'" + Datos.Usuario_Creo + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                DataTable Ds_Datos = new DataTable();


                Sql = "SELECT Top 1 * FROM Ope_Ventas ";
                Sql += " order by Venta_ID desc";

                Obj_Comando.CommandText = Sql;
                Da_Datos = new SqlDataAdapter(Obj_Comando);
                Da_Datos.Fill(Ds_Datos);

                var Venta_ID = System.Convert.ToInt16(Ds_Datos.Rows[0]["Venta_ID"].ToString());

                for (int i = 0; i < Lista.Count; i++)
                {
                    Sql = "INSERT INTO  Ope_Ventas_Detalles (";
                    Sql += " Venta_ID, ";
                    Sql += " Producto_ID, ";
                    Sql += " Cantidad, ";
                    Sql += " Precio, ";
                    Sql += " Importe ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Venta_ID.ToString()) ? "null, " : "'" + Venta_ID.ToString() + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Producto_ID) ? "null, " : "'" + Lista[i].Producto_ID + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Cantidad) ? "null, " : "'" + Lista[i].Cantidad + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Precio) ? "null, " : "'" + Lista[i].Precio + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Importe) ? "null, " : "'" + Lista[i].Importe + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                }

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Ventas_Fechas(Cls_Mdl_Balance Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select Ope_Ventas.Venta_ID, Descripcion, Cantidad, Importe, Cliente, Estatus, Folio, Factura, Ope_Ventas.Fecha_Creo";
                //Sql += " convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo ";
                Sql += " from Ope_Ventas_Detalles inner join Ope_Ventas on Ope_Ventas_Detalles.Venta_ID=Ope_Ventas.Venta_ID ";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }

                Sql += " order by Ope_Ventas.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        
    }
}