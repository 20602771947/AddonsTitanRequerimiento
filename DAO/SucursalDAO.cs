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
    public class SucursalDAO
    {
        public List<SucursalDTO> ObtenerSucursales()
        {
            List<SucursalDTO> lstSucursalDTO = new List<SucursalDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSucursales", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SucursalDTO oSucursalDTO = new SucursalDTO();
                        oSucursalDTO.IdSucursal = int.Parse(drd["Id"].ToString());
                        oSucursalDTO.Codigo = drd["Codigo"].ToString();
                        oSucursalDTO.Descripcion = drd["Descripcion"].ToString();
                        oSucursalDTO.Direccion = drd["Direccion"].ToString();
                        oSucursalDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSucursalDTO.Add(oSucursalDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSucursalDTO;
        }


        public int UpdateInsertSucursal(SucursalDTO oSucursalDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSucursales", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSucursal", oSucursalDTO.IdSucursal);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oSucursalDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSucursalDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oSucursalDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", 0);
                        da.SelectCommand.Parameters.AddWithValue("@IdProvincia", 0);
                        da.SelectCommand.Parameters.AddWithValue("@IdDistrito", 0);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSucursalDTO.Estado);
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


        public List<SucursalDTO> ObtenerDatosxID(int IdSucursal)
        {
            List<SucursalDTO> lstSucursalDTO = new List<SucursalDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSucursalesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSucursal", IdSucursal);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SucursalDTO oSucursalDTO = new SucursalDTO();
                        oSucursalDTO.IdSucursal = int.Parse(drd["Id"].ToString());
                        oSucursalDTO.Codigo = drd["Codigo"].ToString();
                        oSucursalDTO.Descripcion = drd["Descripcion"].ToString();
                        oSucursalDTO.Direccion = drd["Direccion"].ToString();
                        oSucursalDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSucursalDTO.Add(oSucursalDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSucursalDTO;
        }



        public int Delete(int IdSucursal)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarSucursal", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSucursal", IdSucursal);
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
