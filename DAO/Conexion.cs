using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DAO
{
    public class Conexion
    {
        public SqlConnection conectar()
        {
            return cn();
        }
        public SqlConnection cn()
        {
            string nombreBaseDatos = ConfigurationManager.AppSettings["BDAddonRQ"];
           
            SqlConnection cn = new SqlConnection("Server=192.168.0.61;Database=" + nombreBaseDatos + ";User ID=sa;Password=123;Trusted_Connection=False");
          
            return cn;
        }
    }
}
