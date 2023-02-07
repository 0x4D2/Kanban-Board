﻿
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
                if (m_Conn.State == null)
                {
                    m_Conn = GetMySqlConnection();
                }
                e
                return m_Conn;
            }
        }
        private static MySqlConnection GetMySqlConnection()
        {
            try
            {
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = "Server=localhost;Port=3306;Database=kanban;Uid=kanban_user_;Pwd=kanban_user_;Convert Zero Datetime=True";
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