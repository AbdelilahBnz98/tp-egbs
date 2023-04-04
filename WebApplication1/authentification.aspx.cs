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
    public partial class authentification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session.Clear();
                }
            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }
            
        }

       

        protected void btn_connect_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string login = txtLogin.Value.Trim();
                string password = txtMdp.Value;

                if (login == "") {
                    throw new Exception("Champs login obligatoire");
                }

                string req = @"SELECT * 
                FROM "+GlobalInfo.TABLE_USER+@" 
                JOIN liste_profil ON refprofil_utilisateur = ref_profil
                WHERE login_utilisateur = @login_utilisateur
                AND password_utilisateur = @password_utilisateur AND etat_utilisateur = 'A'";

                SqlCommand cmd = new SqlCommand(req);
                cmd.Parameters.AddWithValue("password_utilisateur", password);
                cmd.Parameters.AddWithValue("login_utilisateur", login);

                DataTable dt = Dao.GetData(cmd);
                if (dt == null || dt.Rows.Count == 0) {
                    throw new Exception("Login ou mot de passe incorrect");
                }
                Utilisateur u = new Utilisateur();
                u.login = login;
                u.refUtilisteur = Convert.ToInt32(dt.Rows[0]["ref_utilisateur"]);
                u.nomComplet = Convert.ToString(dt.Rows[0]["prenom_utilisateur"])+" "+ Convert.ToString(dt.Rows[0]["nom_utilisateur"]);                
                u.refProfil = Convert.ToInt32(dt.Rows[0]["refprofil_utilisateur"]);
                u.profil = Convert.ToString(dt.Rows[0]["libelle_profil"]);
                Session[GlobalConstant.IS_CONNECTED] = true;
                Session[GlobalConstant.USER_CONNECTED] = u;


                Response.Redirect("accueil.aspx");

            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }


            
        }
    }
}