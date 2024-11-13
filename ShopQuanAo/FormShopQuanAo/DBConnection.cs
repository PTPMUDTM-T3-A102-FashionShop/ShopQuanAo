using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FormShopQuanAo
{
    public class DBConnection
    {
        public SqlConnection conn;
        public SqlCommand cmd;

        public DBConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.DB_DoAnKetMon_UDTMConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }
    }
}
