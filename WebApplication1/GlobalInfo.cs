using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSA_Clementine1.Util
{
    public class GlobalInfo
    {

        //public static string VERSION = "0.10";
        //public static string DATEVERSION = "20210701";

        //public static bool ISDEBUG = true;
        //public static string ENVIRONEMENT = "DEV";
        //public static string CNX_STRING_SQL = @"Server=.;Database=bdd_gestcomm;User Id=sa;Password=azerty;";
        //public static string TABLE_USER = "utilisateur";

        public static bool ISDEBUG = true;
        public static string ENVIRONEMENT = "DEV2";

        public static string CNX_STRING_SQL =
        "data source=.;initial catalog=Test;integratedSecurity=true; MultipleActiveResultSets=true";
        public static string TABLE_USER = "users";

        

    }
}