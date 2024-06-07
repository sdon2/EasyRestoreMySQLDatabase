using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Reflection;

namespace EasyRestoreMySQLDatabase
{
    class Program
    {
        static int Main(string[] args)
        {
            string sqlFile;
            string cs = "server=localhost;user=root;pwd=;";

            if (args.Length != 0)
            {
                sqlFile = args[0];
            } else
            {
                sqlFile = "backup.sql";
            }            

            try
            {
                RestoreDatabase(cs, sqlFile);
                Console.WriteLine("Database successfully restored.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return 1;
            }
        }

        static void RestoreDatabase(string constring, string file)
        {
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(Path.Combine(dir, file));
                        conn.Close();
                    }
                }
            }
        }
    }
}
