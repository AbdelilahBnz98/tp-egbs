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

        //public static bool ISDEBUG = true;
        //public static string ENVIRONEMENT = "DEV2";

        //public static string CNX_STRING_SQL =
        //"data source=188.121.44.214,1433;initial catalog=royagri_adp;user id=royagri_adp; password=?3g73bpT; MultipleActiveResultSets=true";
        //public static string TABLE_USER = "gest_utilisateur";

        public static bool ISDEBUG = false;
        public static string ENVIRONEMENT = "TEST";

        public static string CNX_STRING_SQL = @"Server=ABDO\SQLEXPRESS;Database=Test;integrated security=true;";
        public static string TABLE_USER = "users";

        //public static bool ISDEBUG = false;
        //public static string ENVIRONEMENT = "PROD";
        //public static string CNX_STRING_SQL = @"Server=MARWA-S215\SQLSERVER2012;Database=bdd_achat_prod;User Id=sdi_achat;Password=achat@2021;";

    }
}