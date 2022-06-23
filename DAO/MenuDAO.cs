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
    public class MenuDAO
    {

        public List<MenuDTO> ObtenerMenus()
        {
            List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMenus", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MenuDTO oMenuDTO = new MenuDTO();
                        oMenuDTO.IdMenu = int.Parse(drd["Id"].ToString());
                        oMenuDTO.Menu = drd["Menu"].ToString();
                        oMenuDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oMenuDTO.Orden = int.Parse(drd["Orden"].ToString());
                        oMenuDTO.Posicion = int.Parse(drd["Posicion"].ToString());
                        lstMenuDTO.Add(oMenuDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstMenuDTO;
        }


    }
}
