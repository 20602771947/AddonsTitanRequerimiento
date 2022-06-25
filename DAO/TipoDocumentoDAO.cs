﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class TipoDocumentoDAO
    {

        public List<TipoDocumentoDTO> ObtenerTipoDocumentos()
        {
            List<TipoDocumentoDTO> lstTipoDocumentoDTO = new List<TipoDocumentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoDocumentos", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoDocumentoDTO oTipoDocumentoDTO = new TipoDocumentoDTO();
                        oTipoDocumentoDTO.IdTipoDocumento = int.Parse(drd["Id"].ToString());
                        oTipoDocumentoDTO.Codigo = drd["Codigo"].ToString();
                        oTipoDocumentoDTO.TipoDocumento = drd["TipoDocumento"].ToString();
                        oTipoDocumentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoDocumentoDTO.Add(oTipoDocumentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstTipoDocumentoDTO;
        }

    }
}
