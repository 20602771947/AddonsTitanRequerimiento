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
    public class AlmacenDAO
    {

        public List<AlmacenDTO> ObtenerAlmacenes()
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacenes", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["Id"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstAlmacenDTO;
        }

        public int UpdateInsertAlmacen(AlmacenDTO oAlmacenDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertAlmacenes", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idAlmacen", oAlmacenDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oAlmacenDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oAlmacenDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAlmacenDTO.Estado);
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

        public List<AlmacenDTO> ObtenerDatosxID(int IdAlmacen)
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacenesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["Id"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstAlmacenDTO;
        }


        public int Delete(int IdAlmacen)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
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
