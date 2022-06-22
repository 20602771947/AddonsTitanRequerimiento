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
    public class ProveedorDAO
    {

        public List<ProveedorDTO> ObtenerProveedores()
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedores", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.Documento = drd["Documento"].ToString();
                        oProveedorDTO.Nombres = drd["Nombres"].ToString();
                        oProveedorDTO.Apellidos = drd["Apellidos"].ToString();
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.Direccion = drd["Direccion"].ToString();
                        oProveedorDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertProveedores", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", proveedorDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", proveedorDTO.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres", proveedorDTO.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@Apellidos", proveedorDTO.Apellidos);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", proveedorDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", proveedorDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", proveedorDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", proveedorDTO.Estado);
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


        public List<ProveedorDTO> ObtenerDatosxID(int IdProveedor)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.Documento = drd["Documento"].ToString();
                        oProveedorDTO.Nombres = drd["Nombres"].ToString();
                        oProveedorDTO.Apellidos = drd["Apellidos"].ToString();
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.Direccion = drd["Direccion"].ToString();
                        oProveedorDTO.Tipo = int.Parse(drd["Tipo"].ToString());
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public int Delete(int IdProveedor)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
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
