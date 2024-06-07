using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRestoreMySQLDatabase
{
    internal class Options
    {
        [Option('f', "file", Required = false, Default = "backup.sql", HelpText = "SQL file backup to restore database from")]
        public string SQLFile { get; set; }

        [Option('c', "connection-string", Required = false, Default = "server=localhost;user=root;pwd=;", HelpText = "Connection string to connect to MySQL host")]
        public string ConnectionString { get; set; }
    }
}
