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
    public class IndicadorImpuestoDAO
    {

        public List<IndicadorImpuestoDTO> ObtenerIndicadorImpuestos(string IdSociedad)
        {
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = new List<IndicadorImpuestoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarIndicadorImpuestos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IndicadorImpuestoDTO oIndicadorImpuestoDTO = new IndicadorImpuestoDTO();
                        oIndicadorImpuestoDTO.IdIndicadorImpuesto = int.Parse(drd["Id"].ToString());
                        oIndicadorImpuestoDTO.Codigo = drd["Codigo"].ToString();
                        oIndicadorImpuestoDTO.Descripcion = drd["Descripcion"].ToString();
                        oIndicadorImpuestoDTO.Porcentaje = decimal.Parse(drd["Porcentaje"].ToString());
                        oIndicadorImpuestoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstIndicadorImpuestoDTO.Add(oIndicadorImpuestoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstIndicadorImpuestoDTO;
        }

        public int UpdateInsertIndicadorImpuesto(IndicadorImpuestoDTO oIndicadorImpuestoDTO,string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertIndicadorImpuestos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oIndicadorImpuestoDTO.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oIndicadorImpuestoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oIndicadorImpuestoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Porcentaje", oIndicadorImpuestoDTO.Porcentaje);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oIndicadorImpuestoDTO.Estado);
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

        public List<IndicadorImpuestoDTO> ObtenerDatosxID(int IdIndicadorImpuesto)
        {
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = new List<IndicadorImpuestoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarIndicadorImpuestosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", IdIndicadorImpuesto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IndicadorImpuestoDTO oIndicadorImpuestoDTO = new IndicadorImpuestoDTO();
                        oIndicadorImpuestoDTO.IdIndicadorImpuesto = int.Parse(drd["Id"].ToString());
                        oIndicadorImpuestoDTO.Codigo = drd["Codigo"].ToString();
                        oIndicadorImpuestoDTO.Descripcion = drd["Descripcion"].ToString();
                        oIndicadorImpuestoDTO.Porcentaje = decimal.Parse(drd["Porcentaje"].ToString());
                        oIndicadorImpuestoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstIndicadorImpuestoDTO.Add(oIndicadorImpuestoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstIndicadorImpuestoDTO;
        }


        public int Delete(int IdIndicadorImpuesto)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarIndicadorImpuesto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", IdIndicadorImpuesto);
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
