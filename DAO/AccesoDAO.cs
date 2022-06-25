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
    public class AccesoDAO
    {
        public List<AccesoDTO> ObtenerAccesos(string IdPerfil)
        {
            List<AccesoDTO> lstAccesoDTO = new List<AccesoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAccesosxPerfil", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", int.Parse(IdPerfil));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AccesoDTO oAccesoDTO = new AccesoDTO();
                        oAccesoDTO.IdAceeso = int.Parse(drd["Id"].ToString());
                        oAccesoDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oAccesoDTO.Perfil = drd["Perfil"].ToString();
                        oAccesoDTO.IdMenu = int.Parse(drd["IdMenu"].ToString());
                        oAccesoDTO.Menu = drd["Menu"].ToString();
                        oAccesoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAccesoDTO.Add(oAccesoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstAccesoDTO;
        }


        public int UpdateInsertAcceso(int IdPerfil,List<int> ArrayAccesos)
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
                        int rpta = 0;
                        for (int i = 0; i < ArrayAccesos.Count; i++)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertAccesos", cn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                            da.SelectCommand.Parameters.AddWithValue("@IdMenu", ArrayAccesos[i]);
                            da.SelectCommand.Parameters.AddWithValue("@Estado", 1);
                            rpta = da.SelectCommand.ExecuteNonQuery();
                        }

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


        public int Delete(int IdPerfil)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAccesoxPerfil", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
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
