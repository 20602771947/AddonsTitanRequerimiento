using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
namespace DAO
{
    public class UnidadMedidaDAO
    {

        public List<UnidadMedidaDTO> ObtenerUnidadMedidas()
        {
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = new List<UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUnidadMedidas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UnidadMedidaDTO oUnidadMedidaDTO = new UnidadMedidaDTO();
                        oUnidadMedidaDTO.IdUnidadMedida = int.Parse(drd["Id"].ToString());
                        oUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oUnidadMedidaDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUnidadMedidaDTO.Add(oUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUnidadMedidaDTO;
        }

        public int UpdateInsertUnidadMedida(UnidadMedidaDTO oUnidadMedidaDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertUnidadMedidas", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oUnidadMedidaDTO.IdUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oUnidadMedidaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoSunat", oUnidadMedidaDTO.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oUnidadMedidaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oUnidadMedidaDTO.Estado);
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

        public List<UnidadMedidaDTO> ObtenerDatosxID(int IdUnidadMedida)
        {
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = new List<UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUnidadMedidasxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UnidadMedidaDTO oUnidadMedidaDTO = new UnidadMedidaDTO();
                        oUnidadMedidaDTO.IdUnidadMedida = int.Parse(drd["Id"].ToString());
                        oUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oUnidadMedidaDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUnidadMedidaDTO.Add(oUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUnidadMedidaDTO;
        }


        public int Delete(int IdUnidadMedida)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarUnidadMedida", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida);
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
