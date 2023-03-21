
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban_Board
{
    public class DbConnections
    {
        private static MySqlConnection m_Conn;
        public static  MySqlConnection Con
        {
            get
            {
                if (m_Conn == null)
                {
                    m_Conn = GetMySqlConnection();
                }
                
                return m_Conn;
            }
        }
        private static MySqlConnection GetMySqlConnection()
        {
            try
            {
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = "Server=localhost;Port=3306;Database=kanban;Uid=MyUser;Pwd=Password;Convert Zero Datetime=True";
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                Con.Close();
                string a = ex.Message;
                return null;
            }
        }
    }
}
