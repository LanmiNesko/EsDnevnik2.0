using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace EsDnevnik2._0
{
    class Konekcija
    {
        static public SqlConnection Connect()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["home"].ConnectionString;
            SqlConnection veza = new SqlConnection(connection_string);
            return veza;
        }
    }
}
