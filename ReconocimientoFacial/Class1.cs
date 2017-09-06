using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ConectarBD
    {
        //CONEXION A LA BASE DE DATOS
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                SqlConnection conectar = new SqlConnection("Data Source=.; Initial Catalog=reconocimiento; User Id=sa; Password=kratos12");
                conectar.Open();
                return conectar;
            }
            catch (Exception)
            {

                throw new Exception("Error en la conexion"); ;
            }
        }

        public static SqlConnection CerrarConexion()
        {
            SqlConnection conectar = new SqlConnection(ConfigurationManager.ConnectionStrings["cadenadeconexion"].ToString());
            conectar.Close();
            return conectar;
        }
    }
}
