using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CSA_Clementine1.Util
{
    public class GlobalFunction
    {
       

        internal static void AfficherDialog(Label lblMsg, Literal srcpt1, string msg)
        {
            if (msg == "Le thread a été abandonné.") return;
            lblMsg.Text = msg;

            srcpt1.Text = @"<script>$(document).ready(function(){
                    setTimeout(function(){
                        $('#btnError').click();
                    },1);
                    }) </script>";
        }
        internal static void AfficherAlertDialog(Label lblMsg, Literal srcpt1, string msg)
        {
            if (msg == "Le thread a été abandonné.") return;
            lblMsg.Text = msg;

            srcpt1.Text = @"<script>$(document).ready(function(){
                    setTimeout(function(){
                        $('#btnAlert').click();
                    },1);
                    }) </script>";
        }
        internal static void AfficherDialog(Label lblMsg, Literal srcpt1, string msg, string btnTrigger)
        {
            if (msg == "Le thread a été abandonné.") return;
            lblMsg.Text = msg;

            srcpt1.Text = @"<script>$(document).ready(function(){
                    setTimeout(function(){
                        $('#" + btnTrigger + @"').click();
                    },1);
                    }) </script>";
        }
        internal static void AfficherDialog(Literal srcpt1, string btnTrigger)
        {
            srcpt1.Text = @"<script>$(document).ready(function(){
                    setTimeout(function(){
                        $('#" + btnTrigger + @"').click();
                    },1);
                    }) </script>";
        }

        internal static bool HasDroit(string type, string name, string mode, int codeFonction)
        {
            //string req = @"SELECT top 1 droit
            //FROM menu
            //JOIN menu_fonction ON ref_menu = refmenu
            //WHERE codefonction = @codefonction
            //AND menu.etat_menu = 'A' 
            //AND codepage_menu = @codepage_menu";
            //SqlCommand cmd = new SqlCommand(req, null);
            //cmd.Parameters.AddWithValue("codefonction", codeFonction);
            //cmd.Parameters.AddWithValue("codepage_menu", name);

            //string droits = Convert.ToString(Dao.GetScalarData(cmd));
            //if (droits.Contains(mode))
            //{
            //    return true;
            //}
            //return false;



            return true;
        }

        public static void LogToText(string msg, string file)
        {
            string path = HttpContext.Current.Server.MapPath("~/log");
            StreamWriter sw = new StreamWriter(path + "/" + file, true);
            sw.WriteLine(DateTime.Now.ToString() + " : " + msg);
            sw.Flush();
            sw.Close();
        }

        public static object getParamApplication(string paramName)
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 " + paramName + " FROM ParametresApplication", null);
            return Dao.GetScalarData(cmd);
        }
        public static string GetStringFromWebConfig(string key) {
            string value = System.Web.Configuration.WebConfigurationManager.AppSettings[key];
            //value = value.Replace("\\n", Environment.NewLine);
            value = value.Replace("\\n", "");
            //value = value.Replace("\n", Environment.NewLine);
            value = value.Replace("\n", "");
            return value;

        }
        public static string ExportDataTableToCsv(DataTable dt, string[] Cols, string[] indexes, string tableName)
        {
            string cheminDossier = HttpContext.Current.Server.MapPath("~/" + GlobalConstant.CHEMIN_BUDGET_EXPORT);
            string myString = "";

            if (!System.IO.Directory.Exists(cheminDossier))
            {
                Directory.CreateDirectory(cheminDossier);
            }
            DateTime d = DateTime.Now;
            // File name and location
            string cheminFichier = cheminDossier + "\\" + tableName + " " + d.ToString("dd-MM-yyy HH-mm-ss") + ".csv";
            System.IO.StreamWriter myWriter = new System.IO.StreamWriter(cheminFichier, false, System.Text.Encoding.UTF8);
            // header        
            foreach (string col in Cols)
            {
                if (!myString.Equals(""))
                    myString += ";";
                myString += col;
            }
            myString += Environment.NewLine;
            foreach (DataRow dr in dt.Rows)
            {
                var first = true;
                foreach (string index in indexes)
                {
                    if (!first)
                        myString += ";";
                    string chaine = Convert.ToString(dr[index]);
                    if (chaine.Contains("00:00"))
                    {
                        chaine = chaine.Substring(0, 10);
                    }
                    myString += chaine;
                    first = false;
                }
                //foreach (object field in dr.ItemArray)
                //{
                //    if (!first)
                //        myString += ",";
                //    myString += field.ToString();
                //    first = false;
                //}
                // New Line to differentiate next row 
                myString += Environment.NewLine;
            }
            // Write the String to the Csv File 
            myWriter.WriteLine(myString);
            // Clean up 
            myWriter.Close();


            return cheminFichier;
        }

        internal static void ChargerDropEmballage(DropDownList drop_emb)
        {
            string req = @"select  null as ARTCOD,' 'as  ARTDES
            UNION
            select 
            ARTCOD,ARTDES from liste_article

            WHERE etat = 'A'
            order by ARTDES ASC";

            if (drop_emb == null)
            {
                throw new Exception("drop emballage null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt emballage null");
            }
            drop_emb.DataSource = dt;
            drop_emb.DataTextField = "ARTDES";
            drop_emb.DataValueField = "ARTCOD";
            drop_emb.DataBind();
        }

        internal static void ChargerDropCalibre(DropDownList drop_calibre)
        {
            string req = @"select null as calibre
UNION
SELECT calibre
FROM liste_calibre
WHERE etat = 'A' 
ORDER BY calibre ASC";

            if (drop_calibre == null)
            {
                throw new Exception("drop calibre null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt calibre null");
            }
            drop_calibre.DataSource = dt;
            drop_calibre.DataTextField = "calibre";
            drop_calibre.DataValueField = "calibre";
            drop_calibre.DataBind();
        }

        internal static void ChargerStatut(DropDownList drop_statut)
        {
            string req = @"SELECT '' as code_statut UNION SELECT code_statut FROM liste_statut ORDER BY code_statut";

            if (drop_statut == null)
            {
                throw new Exception("drop statut null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt statut null");
            }
            drop_statut.DataSource = dt;
            drop_statut.DataTextField = "code_statut";
            drop_statut.DataValueField = "code_statut";
            //drop_statut.SelectedValue = "DRAFT";
            drop_statut.DataBind();
        }

        internal static void ChargerDropDevise(DropDownList drop_devise)
        {
            //string req = @"SELECT '' as TYPCOD, ' ' as TYPLIB  
            //UNION SELECT TYPCOD,TYPLIB FROM liste_type WHERE etat = 'A'  ORDER BY TYPLIB ASC";


            string req = @"select null as  DEVCOD,  ' ' as DEVDES   UNION select DEVCOD, DEVDES from liste_Devise
WHERE etat = 'A'
ORDER BY DEVDES";

            if (drop_devise == null)
            {
                throw new Exception("drop devise null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt devise null");
            }
            drop_devise.DataSource = dt;
            drop_devise.DataTextField = "DEVDES";
            drop_devise.DataValueField = "DEVCOD";
            drop_devise.DataBind();
        }

        internal static string ShowFloat(string v)
        {
            return v.Replace(",", ".");
        }

        internal static void ChargerDropType(DropDownList drop_type)
        {
            string req = @"SELECT '' as TYPCOD, ' ' as TYPLIB  
            UNION SELECT TYPCOD,TYPLIB FROM liste_type WHERE etat = 'A'  ORDER BY TYPLIB ASC";

            if (drop_type == null)
            {
                throw new Exception("drop couleur null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt couleur null");
            }
            drop_type.DataSource = dt;
            drop_type.DataTextField = "TYPLIB";
            drop_type.DataValueField = "TYPCOD";
            drop_type.DataBind();
        }

        internal static void ChargerDropConditionnement(DropDownList drop_conditionnnement)
        {
            string req = @"select null as ref_cond,' ' as lib_cond union 
            select ref_cond,lib_cond from  liste_conditionnement order by lib_cond";

            if (drop_conditionnnement == null)
            {
                throw new Exception("drop conditionnement null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt conditionnement null");
            }
            drop_conditionnnement.DataSource = dt;
            drop_conditionnnement.DataTextField = "lib_cond";
            drop_conditionnnement.DataValueField = "ref_cond";
            drop_conditionnnement.DataBind();
        }

        internal static void ChargerDropCouleur(DropDownList dropCouleur)
        {
            string req = @"SELECT '' as CLRCOD, ' ' as CLRDESF  
            UNION SELECT CLRCOD,CLRDESF FROM liste_couleur WHERE etat = 'A'  ORDER BY CLRDESF ASC";

            if (dropCouleur == null)
            {
                throw new Exception("drop couleur null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt couleur null");
            }
            dropCouleur.DataSource = dt;
            dropCouleur.DataTextField = "CLRDESF";
            dropCouleur.DataValueField = "CLRCOD";
            dropCouleur.DataBind();
        }

        internal static void ChargerDropProfil(DropDownList dropProfil)
        {
            string req = @"SELECT null as ref_profil, ' ' as libelle_profil  UNION SELECT ref_profil,libelle_profil FROM liste_profil  ORDER BY libelle_profil ASC";

            if (dropProfil == null)
            {
                throw new Exception("drop profil null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt profil null");
            }
            dropProfil.DataSource = dt;
            dropProfil.DataTextField = "libelle_profil";
            dropProfil.DataValueField = "ref_profil";
            dropProfil.DataBind();
        }

        internal static void ChargerDropPays(DropDownList drop_pays)
        {
            string req = @"SELECT '' as PAYCOD, ' ' as PAYDES  UNION SELECT PAYCOD,PAYDES FROM liste_pays WHERE etat = 'A' ORDER BY PAYDES ASC";

            if (drop_pays == null)
            {
                throw new Exception("drop pays null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt pays null");
            }
            drop_pays.DataSource = dt;
            drop_pays.DataTextField = "PAYDES";
            drop_pays.DataValueField = "PAYCOD";
            drop_pays.DataBind();
        }
        internal static void ChargerDropBureau(DropDownList drop_pays)
        {
            string req = @"SELECT '' as BURCOD, ' ' as BURDES  UNION SELECT BURCOD,BURDES FROM liste_bureau WHERE etat = 'A' ORDER BY BURDES ASC";

            if (drop_pays == null)
            {
                throw new Exception("drop bureau null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt bureau null");
            }
            drop_pays.DataSource = dt;
            drop_pays.DataTextField = "BURDES";
            drop_pays.DataValueField = "BURCOD";
            drop_pays.DataBind();
        }
        internal static void ChargerDropBanque(DropDownList txt_banque)
        {
            string req = @"select null as BQDES
                            UNION
                            SELECT BQDES
                            FROM liste_banque
                            
                            ORDER BY BQDES ASC";

            if (txt_banque == null)
            {
                throw new Exception("drop Banque null");
            }
            DataTable dt = Dao.GetData(new SqlCommand(req));
            if (dt == null)
            {
                throw new Exception("dt Banque null");
            }
            txt_banque.DataSource = dt;
            txt_banque.DataTextField = "BQDES";
            txt_banque.DataValueField = "BQDES";
            txt_banque.DataBind();
        }


        public static string ChargerNumFiche(string nomFonction)
        {
            return  Convert.ToString(Dao.GetScalarData(new SqlCommand("select dbo."+nomFonction+"(getdate());")));
        }
    }
}