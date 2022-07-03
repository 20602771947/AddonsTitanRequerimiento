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
    public class LineaNegocioDAO
    {

        
            public List<LineaNegocioDTO> ObtenerLineaNegocios(string IdSociedad)
            {
                List<LineaNegocioDTO> lstLineaNegocioDTO = new List<LineaNegocioDTO>();
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ListarLineaNegocios", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = da.SelectCommand.ExecuteReader();
                        while (drd.Read())
                        {
                            LineaNegocioDTO oLineaNegocioDTO = new LineaNegocioDTO();
                            oLineaNegocioDTO.IdLineaNegocio = int.Parse(drd["Id"].ToString());
                            oLineaNegocioDTO.Codigo = drd["Codigo"].ToString();
                            oLineaNegocioDTO.Descripcion = drd["Descripcion"].ToString();
                            oLineaNegocioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                            lstLineaNegocioDTO.Add(oLineaNegocioDTO);
                        }
                        drd.Close();


                    }
                    catch (Exception ex)
                    {
                    }
                }
                return lstLineaNegocioDTO;
            }

            public int UpdateInsertLineaNegocio(LineaNegocioDTO oLineaNegocioDTO,string IdSociedad)
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
                            SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertLineaNegocios", cn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@IdLineaNegocio", oLineaNegocioDTO.IdLineaNegocio);
                            da.SelectCommand.Parameters.AddWithValue("@Codigo", oLineaNegocioDTO.Codigo);
                            da.SelectCommand.Parameters.AddWithValue("@Descripcion", oLineaNegocioDTO.Descripcion);
                            da.SelectCommand.Parameters.AddWithValue("@Estado", oLineaNegocioDTO.Estado);
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

            public List<LineaNegocioDTO> ObtenerDatosxID(int IdLineaNegocio)
            {
                List<LineaNegocioDTO> lstLineaNegocioDTO = new List<LineaNegocioDTO>();
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ListarLineaNegociosxID", cn);
                        da.SelectCommand.Parameters.AddWithValue("@IdLineaNegocio", IdLineaNegocio);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = da.SelectCommand.ExecuteReader();
                        while (drd.Read())
                        {
                            LineaNegocioDTO oLineaNegocioDTO = new LineaNegocioDTO();
                            oLineaNegocioDTO.IdLineaNegocio = int.Parse(drd["Id"].ToString());
                            oLineaNegocioDTO.Codigo = drd["Codigo"].ToString();
                            oLineaNegocioDTO.Descripcion = drd["Descripcion"].ToString();
                            oLineaNegocioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                            lstLineaNegocioDTO.Add(oLineaNegocioDTO);
                        }
                        drd.Close();


                    }
                    catch (Exception ex)
                    {
                    }
                }
                return lstLineaNegocioDTO;
            }


            public int Delete(int IdLineaNegocio)
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
                            SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarLineaNegocio", cn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@IdLineaNegocio", IdLineaNegocio);
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