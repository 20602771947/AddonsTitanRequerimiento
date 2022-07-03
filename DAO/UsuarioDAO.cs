using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
using System.Transactions;


namespace DAO
{
    public class UsuarioDAO
    {
        public List<UsuarioDTO> ValidarUsuario(string Usuario, string Password,int IdSociedad)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidaUsuario", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Usuarios", Usuario);
                    da.SelectCommand.Parameters.AddWithValue("@Password", Password);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["Id"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.NombreUsuario = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUsuarioDTO;
        }


        public List<UsuarioDTO> ObtenerUsuarios(string IdSociedad)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUsuarios", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["Id"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.NombreUsuario = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.NombrePerfil = drd["NombrePerfil"].ToString();
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUsuarioDTO;
        }


        public int UpdateInsertUsuario(UsuarioDTO oUsuarioDTO,string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertUsuarios", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idUsuario", oUsuarioDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", oUsuarioDTO.NombreUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario", oUsuarioDTO.Usuario);
                        da.SelectCommand.Parameters.AddWithValue("@Contraseña", oUsuarioDTO.Password);
                        da.SelectCommand.Parameters.AddWithValue("@IdPerfil", oUsuarioDTO.IdPerfil);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oUsuarioDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@SapUsuario", oUsuarioDTO.SapUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@SapContraseña", oUsuarioDTO.SapPassword);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oUsuarioDTO.Estado);
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


        public List<UsuarioDTO> ObtenerDatosxID(int IdUsuario)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUsuariosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["Id"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.Password = drd["Contraseña"].ToString();
                        oUsuarioDTO.NombreUsuario = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.NombrePerfil = drd["NombrePerfil"].ToString();
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUsuarioDTO;
        }

        public int Delete(int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarUsuarios", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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
