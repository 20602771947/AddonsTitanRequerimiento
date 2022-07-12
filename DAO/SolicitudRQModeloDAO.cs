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
    public class SolicitudRQModeloDAO
    {
        public int UpdateInsertSolicitudRQModelo(SolicitudRQModeloDTO oSolicitudRQModeloDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudRQModeloDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oSolicitudRQModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapa", oSolicitudRQModeloDTO.IdEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@Aprobaciones", oSolicitudRQModeloDTO.Aprobaciones);
                        da.SelectCommand.Parameters.AddWithValue("@Rechazos", oSolicitudRQModeloDTO.Rechazos);
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


    }
}
