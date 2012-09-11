using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using MySql.Data.MySqlClient;

namespace XMLDataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseController dc = new DatabaseController();
            
            if(!dc.TryConnectToDatabase()) 
                return;

            if (args.Length > 0)
            {
                dc.SetTable(args[0]);
            }
            else
            {
                dc.SetTable("ITEM");
            }
            DumpTableToXML(dc, "output");

            if (!dc.TryCloseConnection())
                return;
        }

        /// <summary>
        /// Using the provided database information, this method dumps the current table to an xml file.
        /// </summary>
        /// <param name="dc">the database we use</param>
        /// <param name="filename">the name of the file with no extension</param>
        private static void DumpTableToXML(DatabaseController dc, string filename)
        {
            DataTable dataTable;
            
            using (MySqlDataReader reader = dc.GetReader())
            {
                dataTable = reader.GetSchemaTable();
                dataTable.Load(reader);
            }
            XmlTextWriter xmlTWriter = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\" + filename + ".xml", Encoding.Default);
            xmlTWriter.Formatting = Formatting.Indented;
            try
            {
                dataTable.WriteXml(xmlTWriter, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
