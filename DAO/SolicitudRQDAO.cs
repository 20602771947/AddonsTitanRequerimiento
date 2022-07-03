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

        public List<SolicitudRQDTO> ObtenerSolicitudesRQ(string IdSociedad)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQ", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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



        public int UpdateInsertSolicitud(SolicitudRQDTO oSolicitudRQDTO, SolicitudRQDetalleDTO oSolicitudRQDetalleDTO, string IdSociedad)
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
                        int rpta = 0;
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
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        int IdInsert = 0;
                        if (oSolicitudRQDTO.IdSolicitudRQ > 0)
                        {
                            IdInsert = oSolicitudRQDTO.IdSolicitudRQ;
                        }
                        else
                        {
                            IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        }
                        
                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        int IdInsertDetalle = 0;
                        for (int i = 0; i < oSolicitudRQDetalleDTO.IdArticulo.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQDetalle", oSolicitudRQDetalleDTO.IdSolicitudRQDetalle[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudRQDetalleDTO.IdArticulo[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oSolicitudRQDetalleDTO.IdUnidadMedida[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@FechaNecesaria", oSolicitudRQDetalleDTO.FechaNecesaria[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@CantidadNecesaria", oSolicitudRQDetalleDTO.CantidadNecesaria[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@PrecioInfo", oSolicitudRQDetalleDTO.PrecioInfo[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oSolicitudRQDetalleDTO.IdIndicadorImpuesto[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@Total", oSolicitudRQDetalleDTO.ItemTotal[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oSolicitudRQDetalleDTO.IdAlmacen[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdProveedor", oSolicitudRQDetalleDTO.IdProveedor[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@NumeroFabricacion", oSolicitudRQDetalleDTO.NumeroFabricacion[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@NumeroSerie", oSolicitudRQDetalleDTO.NumeroSerie[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdLineaNegocio", oSolicitudRQDetalleDTO.IdLineaNegocio[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdCentroCostos", oSolicitudRQDetalleDTO.IdCentroCostos[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdProyecto", oSolicitudRQDetalleDTO.IdProyecto[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdMoneda", oSolicitudRQDetalleDTO.IdItemMoneda[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@TipoCambio", oSolicitudRQDetalleDTO.ItemTipoCambio[i]);
                            da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            //IdInsertDetalle = Convert.ToInt32(dad.SelectCommand.ExecuteScalar());
                            rpta = dad.SelectCommand.ExecuteNonQuery();
                            //int rptaDetalle = dad.SelectCommand.ExecuteNonQuery();
                        }

                        //if (IdInsert > 0 && IdInsertDetalle>0)
                        //{
                        //    rpta = 1;
                        //}
                       
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

            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
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
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }


            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalle++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oSolicitudRQDTO.Detalle = new SolicitudDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudDetalleDTO oSolicitudRQDetalleDTO = new SolicitudDetalleDTO();
                        oSolicitudRQDetalleDTO.IdSolicitudRQDetalle = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDetalleDTO.IdSolicitudCabecera = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQDetalleDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oSolicitudRQDetalleDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oSolicitudRQDetalleDTO.FechaNecesaria = Convert.ToDateTime(drd["FechaNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.CantidadNecesaria = decimal.Parse(drd["CantidadNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.PrecioInfo = decimal.Parse(drd["PrecioInfo"].ToString());
                        oSolicitudRQDetalleDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDetalleDTO.ItemTotal = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDetalleDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oSolicitudRQDetalleDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oSolicitudRQDetalleDTO.NumeroFabricacion = drd["NumeroFabricacion"].ToString();
                        oSolicitudRQDetalleDTO.NumeroSerie = drd["NumeroSerie"].ToString();
                        oSolicitudRQDetalleDTO.IdLineaNegocio = int.Parse(drd["IdLineaNegocio"].ToString());
                        oSolicitudRQDetalleDTO.IdCentroCostos = int.Parse(drd["IdCentroCostos"].ToString());
                        oSolicitudRQDetalleDTO.IdProyecto = int.Parse(drd["IdProyecto"].ToString());
                        oSolicitudRQDetalleDTO.IdItemMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oSolicitudRQDetalleDTO.ItemTipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO.Detalle.Add(oSolicitudRQDetalleDTO));
                        oSolicitudRQDTO.Detalle[posicion]= oSolicitudRQDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstSolicitudRQDTO.Add(oSolicitudRQDTO);

            }



            return lstSolicitudRQDTO;
        }




        public int Delete(int IdSolicitudRQDetalle)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDetalleSolicitud", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQDetalle", IdSolicitudRQDetalle);
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
