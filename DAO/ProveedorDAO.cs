﻿using System;
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
    public class ProveedorDAO
    {

        public List<ProveedorDTO> ObtenerProveedores()
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedores", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertProveedores", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", proveedorDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoCliente", proveedorDTO.CodigoCliente);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPersona", proveedorDTO.TipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@TipoDocumento", proveedorDTO.TipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", proveedorDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", proveedorDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoContribuyente", proveedorDTO.EstadoContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionContribuyente", proveedorDTO.CondicionContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@DireccionFiscal", proveedorDTO.DireccionFiscal);
                        da.SelectCommand.Parameters.AddWithValue("@Departamento", proveedorDTO.Departamento);
                        da.SelectCommand.Parameters.AddWithValue("@Provincia", proveedorDTO.Provincia);
                        da.SelectCommand.Parameters.AddWithValue("@Distrito", proveedorDTO.Distrito);
                        da.SelectCommand.Parameters.AddWithValue("@Pais", proveedorDTO.Pais);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", proveedorDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@ComprobantesElectronicos", proveedorDTO.ComprobantesElectronicos);
                        da.SelectCommand.Parameters.AddWithValue("@AfiliadoPLE", proveedorDTO.AfiliadoPLE);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionPago", proveedorDTO.CondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@LineaCredito", proveedorDTO.LineaCredito);
                        da.SelectCommand.Parameters.AddWithValue("@Email", proveedorDTO.Email);
                        da.SelectCommand.Parameters.AddWithValue("@Web", proveedorDTO.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Fax", proveedorDTO.Fax);
                        da.SelectCommand.Parameters.AddWithValue("@NombreContacto", proveedorDTO.NombreContacto);
                        da.SelectCommand.Parameters.AddWithValue("@TelefonoContacto", proveedorDTO.TelefonoContacto);
                        da.SelectCommand.Parameters.AddWithValue("@EmailContacto", proveedorDTO.EmailContacto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaIngreso", proveedorDTO.FechaIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", proveedorDTO.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", proveedorDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", proveedorDTO.Tipo);
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


        public List<ProveedorDTO> ObtenerDatosxID(int IdProveedor)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public int Delete(int IdProveedor)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
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
