using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class SociedadDAO
    {
        public List<SociedadDTO> ObtenerSociedades()
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedades", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["Id"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.NombreBd = drd["NombreBd"].ToString();
                        oSociedadDTO.CadenaConexion = drd["CadenaConexion"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSociedadDTO;
        }

        public int UpdateInsertSociedad(SociedadDTO oSociedadDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSociedades", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oSociedadDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@NombreSociedad", oSociedadDTO.NombreSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@NombreBd", oSociedadDTO.NombreBd);
                        da.SelectCommand.Parameters.AddWithValue("@CadenaConexion", oSociedadDTO.CadenaConexion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSociedadDTO.Estado);
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

        public List<SociedadDTO> ObtenerDatosxID(int IdSociedad)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedadesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["Id"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.NombreBd = drd["NombreBd"].ToString();
                        oSociedadDTO.CadenaConexion = drd["CadenaConexion"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSociedadDTO;
        }


        public int Delete(int IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarSociedad", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
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
