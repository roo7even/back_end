using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;


namespace back_end {
    class Database
    {
        public SQLiteConnection dbConnection;
        public string dbFileName = "aranet.db";

        public Database()
        {

            dbConnection = new SQLiteConnection("Data source=" + dbFileName);

            if (!File.Exists("./" + dbFileName))
            {
                Console.WriteLine("Can't find : " + dbFileName);
                String location = System.AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Should be placed in " + location);
                return;
            }

            Console.WriteLine("Datafile loaded: " + dbFileName);


        }
        public void OpenConnection()
        {
            if (dbConnection.State != System.Data.ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (dbConnection.State != System.Data.ConnectionState.Closed)
            {
                dbConnection.Close();
            }
        }
    }
}

