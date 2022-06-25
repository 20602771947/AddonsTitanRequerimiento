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
    public class DepartamentoDAO
    {

        public List<DepartamentoDTO> ObtenerDepartamentos()
        {
            List<DepartamentoDTO> lstDepartamentoDTO = new List<DepartamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDepartamentos", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DepartamentoDTO oDepartamentoDTO = new DepartamentoDTO();
                        oDepartamentoDTO.IdDepartamento = int.Parse(drd["Id"].ToString());
                        oDepartamentoDTO.Codigo = drd["Codigo"].ToString();
                        oDepartamentoDTO.Descripcion = drd["Descripcion"].ToString();
                        oDepartamentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDepartamentoDTO.Add(oDepartamentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDepartamentoDTO;
        }

        public int UpdateInsertDepartamento(DepartamentoDTO oDepartamentoDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertDepartamentos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oDepartamentoDTO.IdDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oDepartamentoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oDepartamentoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oDepartamentoDTO.Estado);
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

        public List<DepartamentoDTO> ObtenerDatosxID(int IdDepartamento)
        {
            List<DepartamentoDTO> lstDepartamentoDTO = new List<DepartamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDepartamentosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DepartamentoDTO oDepartamentoDTO = new DepartamentoDTO();
                        oDepartamentoDTO.IdDepartamento = int.Parse(drd["Id"].ToString());
                        oDepartamentoDTO.Codigo = drd["Codigo"].ToString();
                        oDepartamentoDTO.Descripcion = drd["Descripcion"].ToString();
                        oDepartamentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDepartamentoDTO.Add(oDepartamentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDepartamentoDTO;
        }


        public int Delete(int IdDepartamento)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDepartamento", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
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
