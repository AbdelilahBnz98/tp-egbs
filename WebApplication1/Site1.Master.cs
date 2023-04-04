using CSA_Clementine1.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSA_Clementine1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        public string MenuItems = "";

        private int refUser = 0;
        private int refFProfil = 0;




        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[GlobalConstant.IS_CONNECTED] == null || Convert.ToBoolean(Session[GlobalConstant.IS_CONNECTED]) == false)
                {
                    Response.Redirect("authentification.aspx");
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ChargerEntete()
        {
            Utilisateur u = Session[GlobalConstant.USER_CONNECTED] as Utilisateur;

            txtEntete.Text = u.nomComplet + " - " + u.profil;
        }

        private void ChargerMenu(int refFonction)
        {
            //creer le menu par fonction de l'utilisateur connecter
            string req1 = "SELECT  * FROM [dbo].[menu] WHERE [isPrincipale] = N'1' AND [etat_menu] = N'A' ORDER BY order_menu";
            DataTable dtPrincipale = Dao.GetData(new SqlCommand(req1));
            foreach (DataRow menuPrincipale in dtPrincipale.Rows)
            {

                string htmlPrincipale = @"<li class='nav-item'>
                                        <a href='#' class='nav-link'>
                                            <i class='mdi mdi-monitor-dashboard menu-icon'></i>
                                            <span class='menu-title'>"+Convert.ToString(menuPrincipale["libelle_menu"]) +@"</span>
                                            <i class='menu-arrow'></i>
                                        </a>
                                        <div class='submenu' style='margin-left: auto; margin-right: 0;'>
                                                 <ul class='submenu-item'>                                                
                                            ";
                string refMenuPrincipale = Convert.ToString(menuPrincipale["ref_menu"]);
                string req2 = "SELECT  * FROM [dbo].[menu] WHERE [isPrincipale] = N'0' AND [refmenu_menu] = N'" + refMenuPrincipale + "' AND [etat_menu] = N'A' ORDER BY order_menu";

                DataTable dtSub = Dao.GetData(new SqlCommand(req2));
                foreach (DataRow menuSub in dtSub.Rows)
                {
                    htmlPrincipale += @"<li class='nav-item'><a class='nav-link' href='" + Convert.ToString(menuSub["url_menu"]) + @"'>" + Convert.ToString(menuSub["libelle_menu"]) + @"</a></li>";
                    if (Convert.ToInt32(menuSub["under_ligne"]) == 1)
                    {                        
                        htmlPrincipale += "<li class='nav-item' style='display:block;height : 1px;background:#ff0000'></li>";
                    }
                }

                htmlPrincipale += @"        </ul>
                                        </div>
                                    </li>";

                MenuItems += htmlPrincipale;
            }            

        }

    }
}