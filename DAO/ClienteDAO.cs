using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAO;
using DTO;

namespace DAO
{
    public class ClienteDAO
    {

        public List<ClienteDTO> ObtenerClientes()
        {
            List<ClienteDTO> lstClienteDTO = new List<ClienteDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarClientes", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ClienteDTO oClienteDTO = new ClienteDTO();
                        oClienteDTO.IdCliente = int.Parse(drd["Id"].ToString());
                        oClienteDTO.Documento = drd["Documento"].ToString();
                        oClienteDTO.Nombres = drd["Nombres"].ToString();
                        oClienteDTO.Apellidos = drd["Apellidos"].ToString();
                        oClienteDTO.Telefono = drd["Telefono"].ToString();
                        oClienteDTO.Direccion = drd["Direccion"].ToString();
                        oClienteDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oClienteDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstClienteDTO.Add(oClienteDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstClienteDTO;
        }

        public int UpdateInsertCliente(ClienteDTO clienteDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertClientes", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", clienteDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", clienteDTO.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres", clienteDTO.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@Apellidos", clienteDTO.Apellidos);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", clienteDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", clienteDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", clienteDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", clienteDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }


        public List<ClienteDTO> ObtenerDatosxID(int IdCliente)
        {
            List<ClienteDTO> lstClienteDTO = new List<ClienteDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarClientesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ClienteDTO oClienteDTO = new ClienteDTO();
                        oClienteDTO.IdCliente = int.Parse(drd["Id"].ToString());
                        oClienteDTO.Documento = drd["Documento"].ToString();
                        oClienteDTO.Nombres = drd["Nombres"].ToString();
                        oClienteDTO.Apellidos = drd["Apellidos"].ToString();
                        oClienteDTO.Telefono = drd["Telefono"].ToString();
                        oClienteDTO.Direccion = drd["Direccion"].ToString();
                        oClienteDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oClienteDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstClienteDTO.Add(oClienteDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstClienteDTO;
        }


        public int Delete(int IdCliente)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarCliente", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
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
