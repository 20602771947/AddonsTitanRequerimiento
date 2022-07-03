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
    public class ProyectoDAO
    {

        public List<ProyectoDTO> ObtenerProyectos(string IdSociedad)
        {
            List<ProyectoDTO> lstProyectoDTO = new List<ProyectoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProyectos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProyectoDTO oProyectoDTO = new ProyectoDTO();
                        oProyectoDTO.IdProyecto = int.Parse(drd["Id"].ToString());
                        oProyectoDTO.Codigo = drd["Codigo"].ToString();
                        oProyectoDTO.Descripcion = drd["Descripcion"].ToString();
                        oProyectoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProyectoDTO.Add(oProyectoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProyectoDTO;
        }

        public int UpdateInsertProyecto(ProyectoDTO oProyectoDTO,string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertProyectos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProyecto", oProyectoDTO.IdProyecto);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oProyectoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oProyectoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oProyectoDTO.Estado);
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

        public List<ProyectoDTO> ObtenerDatosxID(int IdProyecto)
        {
            List<ProyectoDTO> lstProyectoDTO = new List<ProyectoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProyectosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProyectoDTO oProyectoDTO = new ProyectoDTO();
                        oProyectoDTO.IdProyecto = int.Parse(drd["Id"].ToString());
                        oProyectoDTO.Codigo = drd["Codigo"].ToString();
                        oProyectoDTO.Descripcion = drd["Descripcion"].ToString();
                        oProyectoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProyectoDTO.Add(oProyectoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProyectoDTO;
        }


        public int Delete(int IdProyecto)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarProyecto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProyecto", IdProyecto);
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
