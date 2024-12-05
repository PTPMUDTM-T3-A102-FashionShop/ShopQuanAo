using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DB
{
    public class DBConnection
    {
        public SqlConnection conn;
        public SqlCommand cmd;

        public DBConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.DoAnKetMon_UDTMConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
