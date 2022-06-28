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
    public class SolicitudRQDAO
    {

        public List<SolicitudRQDTO> ObtenerSolicitudesRQ()
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQ", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        lstSolicitudRQDTO.Add(oSolicitudRQDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQDTO;
        }



        public int UpdateInsertSolicitud(SolicitudRQDTO oSolicitudRQDTO, SolicitudRQDetalleDTO oSolicitudRQDetalleDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQ", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", oSolicitudRQDTO.IdSolicitudRQ);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oSolicitudRQDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oSolicitudRQDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@Numero", oSolicitudRQDTO.Numero);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitante", oSolicitudRQDTO.IdSolicitante);
                        da.SelectCommand.Parameters.AddWithValue("@IdSucursal", oSolicitudRQDTO.IdSucursal);
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oSolicitudRQDTO.IdDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@IdClaseArticulo", oSolicitudRQDTO.IdClaseArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdTitular", oSolicitudRQDTO.IdTitular);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oSolicitudRQDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oSolicitudRQDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@TotalAntesDescuento", oSolicitudRQDTO.TotalAntesDescuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oSolicitudRQDTO.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oSolicitudRQDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oSolicitudRQDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oSolicitudRQDTO.FechaContabilizacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaValidoHasta", oSolicitudRQDTO.FechaValidoHasta);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oSolicitudRQDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Comentarios", oSolicitudRQDTO.Comentarios);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSolicitudRQDTO.Estado);
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


        public List<SolicitudRQDTO> ObtenerDatosxID(int IdSolicitudRQ)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        lstSolicitudRQDTO.Add(oSolicitudRQDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQDTO;
        }



    }
}
