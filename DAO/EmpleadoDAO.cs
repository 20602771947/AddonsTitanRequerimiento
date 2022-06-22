using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace DAO
{
    public class EmpleadoDAO
    {

        public List<EmpleadoDTO> ObtenerEmpleados()
        {
            List<EmpleadoDTO> lstEmpleadoDTO = new List<EmpleadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEmpleados", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EmpleadoDTO oEmpleadoDTO = new EmpleadoDTO();
                        oEmpleadoDTO.IdEmpleado = int.Parse(drd["Id"].ToString());
                        oEmpleadoDTO.Documento = drd["Documento"].ToString();
                        oEmpleadoDTO.Nombres = drd["Nombres"].ToString();
                        oEmpleadoDTO.Apellidos = drd["Apellidos"].ToString();
                        oEmpleadoDTO.Telefono = drd["Telefono"].ToString();
                        oEmpleadoDTO.Direccion = drd["Direccion"].ToString();
                        oEmpleadoDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oEmpleadoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstEmpleadoDTO.Add(oEmpleadoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstEmpleadoDTO;
        }

        public int UpdateInsertEmpleado(EmpleadoDTO EmpleadoDTO)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertEmpleados", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", EmpleadoDTO.IdEmpleado);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", EmpleadoDTO.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres", EmpleadoDTO.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@Apellidos", EmpleadoDTO.Apellidos);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", EmpleadoDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", EmpleadoDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", EmpleadoDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", EmpleadoDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }
        }


        public List<EmpleadoDTO> ObtenerDatosxID(int IdEmpleado)
        {
            List<EmpleadoDTO> lstEmpleadoDTO = new List<EmpleadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEmpleadosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EmpleadoDTO oEmpleadoDTO = new EmpleadoDTO();
                        oEmpleadoDTO.IdEmpleado = int.Parse(drd["Id"].ToString());
                        oEmpleadoDTO.Documento = drd["Documento"].ToString();
                        oEmpleadoDTO.Nombres = drd["Nombres"].ToString();
                        oEmpleadoDTO.Apellidos = drd["Apellidos"].ToString();
                        oEmpleadoDTO.Telefono = drd["Telefono"].ToString();
                        oEmpleadoDTO.Direccion = drd["Direccion"].ToString();
                        oEmpleadoDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oEmpleadoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstEmpleadoDTO.Add(oEmpleadoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstEmpleadoDTO;
        }


        public int Delete(int IdEmpleado)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarEmpleado", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }


    }
}
