using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSA_Clementine1.Util;
using CSA_Clementine1;

namespace WebApplication1
{
    public partial class accueil : System.Web.UI.Page
    {
        protected SqlConnection cnx = new SqlConnection();


        string tableName = GlobalInfo.TABLE_USER;

        Utilisateur user;

        public string ref_ = "0";
        public string action = "a";


        public void ChargerTableau()
        {
            string req = @"SELECT * 
            FROM " + tableName + @" 
            JOIN liste_profil ON refprofil_utilisateur = ref_profil
            WHERE etat_utilisateur IN ('A','D') ORDER BY dmodification_utilisateur desc";
            SqlCommand cmd = new SqlCommand(req);
            DataTable dt = Dao.GetData(cmd);
            GridData.DataSource = dt;
            GridData.DataBind();
        }


        private void Charger()
        {



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChargerTableau();
            }
        }


        protected void btnCreer_ServerClick(object sender, EventArgs e)
        {
            try
            {

                string req = @"INSERT INTO " + tableName + @"
                                   (login_utilisateur
                                   ,password_utilisateur
                                   ,nom_utilisateur
                                   ,prenom_utilisateur                                   
                                   ,refprofil_utilisateur
                                   ,etat_utilisateur
                                   ,dcreation_utilisateur
                                   ,dmodification_utilisateur
                                   ,refacteur_utilisateur,email_utilisateur)
                             VALUES
                                   (@login_utilisateur
                                   ,@password_utilisateur
                                   ,@nom_utilisateur
                                   ,@prenom_utilisateur                                   
                                   ,@refprofil_utilisateur
                                   ,@etat_utilisateur
                                   ,GETDATE()
                                   ,GETDATE()
                                   ,@ref_user,@email_utilisateur)";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("login_utilisateur", txtIndentifiant.Text.Trim());
                cmd.Parameters.AddWithValue("password_utilisateur", txtMdp.Text);
                cmd.Parameters.AddWithValue("nom_utilisateur", txtNom.Text);
                cmd.Parameters.AddWithValue("prenom_utilisateur", txtPrenom.Text);
                cmd.Parameters.AddWithValue("refprofil_utilisateur", DropProfil.SelectedValue);
                cmd.Parameters.AddWithValue("etat_utilisateur", DropEtat.SelectedValue);
                cmd.Parameters.AddWithValue("email_utilisateur", "");
                cmd.Parameters.AddWithValue("ref_user", user.refUtilisteur);

                int res = Dao.SaveData(cmd);
                if (res < 0)
                {
                    throw new Exception("ECHEC création");
                }
                Response.Redirect("Todolist.aspx?a=a&i=0");

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {

            }
        }
    }
    }