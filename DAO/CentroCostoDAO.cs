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
    public class CentroCostoDAO
    {
        public List<CentroCostoDTO> ObtenerCentroCostos(string IdSociedad)
        {
            List<CentroCostoDTO> lstCentroCostoDTO = new List<CentroCostoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCentroCostos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CentroCostoDTO oCentroCostoDTO = new CentroCostoDTO();
                        oCentroCostoDTO.IdCentroCosto = int.Parse(drd["Id"].ToString());
                        oCentroCostoDTO.Codigo = drd["Codigo"].ToString();
                        oCentroCostoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCentroCostoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCentroCostoDTO.Add(oCentroCostoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstCentroCostoDTO;
        }

        public int UpdateInsertCentroCosto(CentroCostoDTO oCentroCostoDTO,string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCentroCostos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oCentroCostoDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCentroCostoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCentroCostoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCentroCostoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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

        public List<CentroCostoDTO> ObtenerDatosxID(int IdCentroCosto)
        {
            List<CentroCostoDTO> lstCentroCostoDTO = new List<CentroCostoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCentroCostosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CentroCostoDTO oCentroCostoDTO = new CentroCostoDTO();
                        oCentroCostoDTO.IdCentroCosto = int.Parse(drd["Id"].ToString());
                        oCentroCostoDTO.Codigo = drd["Codigo"].ToString();
                        oCentroCostoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCentroCostoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCentroCostoDTO.Add(oCentroCostoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstCentroCostoDTO;
        }


        public int Delete(int IdCentroCosto)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarCentroCosto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto);
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
