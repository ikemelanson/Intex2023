using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Intex2023.Models
{
    public class Helpers
    {
        public static string GetRDSConnectionStringLogin()
        {
            var appConfig = System.Configuration.ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME_LOGIN"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

            return "host=" + hostname + ";port=" + port + ";database=" + dbname + ";user id=" + username + ";password=" + password + ";Connection Lifetime=0;";
        }

        public static string GetRDSConnectionStringBurial()
        {
            var appConfig = System.Configuration.ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME_BURIAL"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

            return "host=" + hostname + ";port=" + port + ";database=" + dbname + ";user id=" + username + ";password=" + password + ";Connection Lifetime=0;";
        }
    }
}
