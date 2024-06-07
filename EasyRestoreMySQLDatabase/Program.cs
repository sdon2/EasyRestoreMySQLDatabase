using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Reflection;
using CommandLine;
using System.Collections.Generic;

namespace EasyRestoreMySQLDatabase
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                CommandLine.Parser.Default.ParseArguments<Options>(args)
                    .WithParsed(RunOptions)
                    .WithNotParsed(HandleParseError);

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return 1;
            }
        }

        static void RunOptions(Options options)
        {
            using (MySqlConnection conn = new MySqlConnection(options.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(options.SQLFile);
                        conn.Close();
                    }
                }
            }

            Console.WriteLine("Database successfully restored.");
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {            
            //throw new Exception("Unable to proceed. There are errors in restoring SQL file.");
        }
    }
}
