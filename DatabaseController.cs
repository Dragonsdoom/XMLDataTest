using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace XMLDataTest
{
    public class DatabaseController
    {
        string connect = "Server = localhost; Database = cs153db; Uid = root; Pwd = \"\";";
        MySqlConnection con;
        MySqlCommand command;

        public DatabaseController()
        {
            con = new MySqlConnection(connect);
            command = new MySqlCommand();
            command.Connection = con;
            command.CommandText = "SELECT * FROM ORDERS";
        }

        public void SetTable(string tableName)
        {
            command.CommandText = "SELECT * FROM " + tableName;
        }

        public MySqlDataReader GetReader()
        {
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            return reader;
        }

        public bool TryConnectToDatabase()
        {
            //Try open connection
            try
            {
                con.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
#if DEBUG
            Console.WriteLine("Made the connection to the MySQL database.");
#endif
            return true;
        }

        public bool TryCloseConnection()
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }

            return true;
        }
    }
}
