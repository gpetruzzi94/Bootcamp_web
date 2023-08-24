using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CodigoComun.Datos
{
    public class AccesoDatos
    {
        private SqlConnection ConnectionBD = new SqlConnection();
        public string strcondatos;
        public string strcondatosSucursal;

        //datos de conexion
        private string serverBD;
        private string usuarioBD;
        private string PasswordBD;
        private string basedatos;



        public AccesoDatos()
        {
            /*
            //Para SQL en la nube
            serverBD = "SQL5110.site4now.net";
            usuarioBD = "db_a96920_alumnos_admin";
            PasswordBD = "Bootcamp2023";
            basedatos = "db_a96920_alumnos";

            // armo conexion a la BD
            strcondatos = @"Data Source=" + serverBD + ";Initial Catalog=" + basedatos + ";User ID=" + usuarioBD + ";Password=" + PasswordBD;
            */

            
            //Para SQL en mi computadora
            serverBD = "DESKTOP-4UGI92H\\SQLEXPRESS";
            basedatos = "StocksApp";
            strcondatos = @"Data Source=" + this.serverBD + ";Initial Catalog=" + this.basedatos + ";Integrated Security=true";
            

        }

        public void Conectar()
        {
            try
            {
                if (ConnectionBD.State == 0)
                {
                    ConnectionBD.ConnectionString = strcondatos;

                    ConnectionBD.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ComprobarConexion()
        {
            try
            {
                if (!ComprobarConexionADO())
                    return false;
                else
                    return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ComprobarConexionADO()
        {
            try
            {
                if (ConnectionBD.State == 0)
                {
                    ConnectionBD.ConnectionString = strcondatos;
                    ConnectionBD.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Desconecta la conexión
        /// </summary>
        public void DesConectar()
        {
            try
            {
                ConnectionBD.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Ejecuta un string Command de selección y devuelve un OdbcDataReader.
        /// Aclaración: Luego de recorrer el DataReader, recordar cerrar la conexión (AccesoDatosOdbc.DesConectar())
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(SqlCommand Command)
        {
            try
            {
                Command.Connection = ConnectionBD;
                //Da.CommandTimeout = 0; //Para que no se vaya por timeout la conexión
                Conectar();
                SqlDataReader Dr = Command.ExecuteReader();
                return Dr;
            }
            catch (Exception e)
            {
                DesConectar();
                throw e;
            }
            finally
            {

            }
        }

        //PAra update o insert
        public SqlCommand ejecQuery(SqlCommand cmd)
        {
            int resp = 0;
            try
            {

                cmd.Connection = ConnectionBD;
                cmd.CommandTimeout = 60;
                Conectar();
                resp = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesConectar();
            }
            return cmd;
        }

        public int ejecQueryCommit(SqlCommand cmd, CommittableTransaction TRANS, int index = 0)
        {
            int resp = 0;
            try
            {
                cmd.Dispose();
                cmd.Connection = ConnectionBD;
                cmd.CommandTimeout = 180;
                //Conectar();
                cmd.Connection.EnlistTransaction(TRANS);
                resp = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 1205)
                {
                    if (index == 3)
                    {
                        //Log.Escribir(1, "SQL-COMMIT", $"Reintente 3 veces y sigo con error de 1205");
                        TRANS.Rollback();
                        throw;
                    }
                    //  Log.Escribir(1, "SQL-COMMIT", $"Obtuve error 1205, index {index}. Reintento ");
                    index++;
                    return ejecQueryEscalarCommit(cmd, TRANS, index++);
                }
                //   Log.Escribir(1, "SQL-COMMIT", $"SQL Exception - Error al obtener informacion de ubicacion del articulo " +
                //  $" desde order tango repository.{sqlEx.Message} ");
                TRANS.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                TRANS.Rollback();
                throw;
            }
            finally
            {
                //DesConectar();
            }
            return resp;
        }

        public int ejecQueryEscalarCommit(SqlCommand cmd, CommittableTransaction TRANS, int index = 0)
        {
            int resp = 0;
            try
            {
                cmd.Dispose();
                cmd.Connection = ConnectionBD;
                cmd.CommandTimeout = 180;
                //Conectar();
                cmd.Connection.EnlistTransaction(TRANS);
                resp = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 1205)
                {
                    if (index == 3)
                    {
                        //Log.Escribir(1, "SQL-COMMIT", $"Reintente 3 veces y sigo con error de 1205");
                        TRANS.Rollback();
                        throw;
                    }
                    //Log.Escribir(1, "SQL-COMMIT", $"Obtuve error 1205, index {index}. Reintento ");
                    index++;
                    return ejecQueryEscalarCommit(cmd, TRANS, index++);
                }
                //Log.Escribir(1, "SQL-COMMIT", $"SQL Exception - Error al obtener informacion de ubicacion del articulo " +
                //   $" desde order tango repository.{sqlEx.Message} ");
                TRANS.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                TRANS.Rollback();
                throw;
            }
            finally
            {
                //DesConectar();
            }
            return resp;
        }

        public int ejecQueryEscalar(SqlCommand cmd)
        {
            int resp = 0;
            try
            {
                cmd.Connection = ConnectionBD;
                cmd.CommandTimeout = 180;
                Conectar();

                resp = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesConectar();
            }
            return resp;
        }


        public int ejecQueryDevuelveInt(SqlCommand cmd)
        {
            int resp = 0;
            try
            {
                cmd.Connection = ConnectionBD;
                cmd.CommandTimeout = 60;
                Conectar();
                resp = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesConectar();
            }
            return resp;
        }



        /// <summary>
        /// Ejecuta un sqlCommand de store y lo devuelve
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SqlCommand ejecCommand(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = ConnectionBD;

                Conectar();
                int resp = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesConectar();
            }
            return cmd;
        }
        public DataTable execDTCommit(SqlCommand Command, CommittableTransaction TRANS)
        {
            try
            {
                Command.Connection = ConnectionBD;
                Command.CommandTimeout = 120;
                Command.Connection.EnlistTransaction(TRANS);
                //Conectar();
                SqlDataAdapter da = new SqlDataAdapter(Command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                // DesConectar();
                throw e;
            }
            finally
            {
                //todo Rodrigo
                // DesConectar();
            }
        }
        public DataTable execDT(SqlCommand Command)
        {
            try
            {
                Command.Connection = ConnectionBD;
                Command.CommandTimeout = 120;
                Conectar();
                SqlDataAdapter da = new SqlDataAdapter(Command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                DesConectar();
                throw e;
            }
            finally
            {
                //todo Rodrigo
                DesConectar();
            }
        }

        public DataTable execDT2(SqlCommand Command)
        {
            try
            {
                Command.Connection = ConnectionBD;

                Conectar();
                SqlDataAdapter da = new SqlDataAdapter(Command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                //DesConectar();
                throw e;
            }
            finally
            {
                DesConectar();
            }
        }
    }
}
