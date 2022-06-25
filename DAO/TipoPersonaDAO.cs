using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class TipoPersonaDAO
    {
        public List<TipoPersonaDTO> ObtenerTipoPersonas()
        {
            List<TipoPersonaDTO> lstTipoPersonaDTO = new List<TipoPersonaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoPersonas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoPersonaDTO oTipoPersonaDTO = new TipoPersonaDTO();
                        oTipoPersonaDTO.IdTipoPersona = int.Parse(drd["Id"].ToString());
                        oTipoPersonaDTO.TipoPersona = drd["TipoPersona"].ToString();
                        oTipoPersonaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoPersonaDTO.Add(oTipoPersonaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstTipoPersonaDTO;
        }
    }
}
