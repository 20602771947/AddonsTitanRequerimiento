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
    public class ArticuloDAO
    {

        public List<ArticuloDTO> ObtenerArticulos(string IdSociedad)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["Id"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {

                }
            }
            return lstArticuloDTO;
        }



        public int UpdateInsertArticulo(ArticuloDTO oArticuloDTO,string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertArticulos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oArticuloDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oArticuloDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion1", oArticuloDTO.Descripcion1);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion2", oArticuloDTO.Descripcion2);
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oArticuloDTO.IdUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@ActivoFijo", oArticuloDTO.ActivoFijo);
                        da.SelectCommand.Parameters.AddWithValue("@ActivoCatalogo", oArticuloDTO.ActivoCatalogo);
                        da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", oArticuloDTO.IdCodigoUbso);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oArticuloDTO.Estado);
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



        public List<ArticuloDTO> ObtenerDatosxID(int IdArticulo)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["Id"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = (drd["Descripcion1"].ToString());
                        oArticuloDTO.Descripcion2 = (drd["Descripcion2"].ToString());
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        //oArticuloDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    
                }
            }
            return lstArticuloDTO;
        }


        public int Delete(int IdArticulo)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarArticulo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
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
