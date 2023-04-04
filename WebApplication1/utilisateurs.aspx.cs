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
    public partial class utilisateurs : System.Web.UI.Page
    {
        string tableName = GlobalInfo.TABLE_USER;

        Utilisateur user;

        public string ref_ = "0";
        public string action = "a";

        private void ChargerEntete()
        {
            if (Request["i"] != null)
            {
                ref_ = Convert.ToString(Request["i"]);
            }
            if (Request["a"] != null)
            {
                action = Convert.ToString(Request["a"]);
            }
            if (Session[GlobalConstant.USER_CONNECTED] != null)
            {
                user = (Utilisateur)Session[GlobalConstant.USER_CONNECTED];
            }
        }
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

        public void InitFiche()
        {

            txtIndentifiant.Enabled = false;
            txtMdp.Enabled = false;
            txtNom.Enabled = false;
            txtPrenom.Enabled = false;
            DropEtat.Enabled = false;
            DropProfil.Enabled = false;


            btnCreer.Visible = false;

            btnAskModifier.Visible = false;
            btnUpdate.Visible = false;

            btnAskSupprimer.Visible = false;
            btnDelete.Visible = false;

            if (action == "a")
            {
                txtIndentifiant.Enabled = true;
                txtMdp.Enabled = true;
                txtNom.Enabled = true;
                txtPrenom.Enabled = true;
                DropEtat.Enabled = true;
                DropProfil.Enabled = true;

                btnCreer.Visible = true;

                btnCreer.Visible = GlobalFunction.HasDroit("btn", "utilisateurs", "a", user.refProfil);
            }
            else if (action == "u")
            {
                txtIndentifiant.Enabled = false;
                txtMdp.Enabled = true;
                txtNom.Enabled = true;
                txtPrenom.Enabled = true;
                DropEtat.Enabled = true;
                DropProfil.Enabled = true;


                btnUpdate.Visible = btnAskModifier.Visible = GlobalFunction.HasDroit("btn", "utilisateurs", "u", user.refProfil);
            }
            else if (action == "d")
            {
                btnAskSupprimer.Visible = true;
                btnDelete.Visible = true;

                btnDelete.Visible = btnAskSupprimer.Visible = GlobalFunction.HasDroit("btn", "utilisateurs", "d", user.refProfil);
            }

        }

        private void validerChamps()
        {
            string msg = "";
            if (DropProfil.SelectedItem.Text.Trim().Length == 0)
            {
                msg += " Choix profil";
            }
            if (txtIndentifiant.Text.Trim().Length == 0)
            {
                msg += " Champs identifiant";
            }
            if (txtMdp.Text.Trim().Length == 0)
            {
                msg += " Champs mot de passe";
            }
            if (txtNom.Text.Trim().Length == 0)
            {
                msg += " Champs nom";
            }
            if (txtPrenom.Text.Trim().Length == 0)
            {
                msg += " Champs prènom";
            }


            if (msg != "")
            {
                throw new Exception(msg + ", obligatoire(s)");
            }
        }
        private void checkExistIdentifiant()
        {
            string req = "SELECT COUNT(*) FROM " + tableName + @" WHERE etat_utilisateur <> 'S' AND LOWER(login_utilisateur) = LOWER(@login) ";
            SqlCommand cmd = new SqlCommand(req, null);
            if (ref_ != "0")
            {
                cmd.CommandText += " AND ref_utilisateur <> @ref_user";
                cmd.Parameters.AddWithValue("ref_user", ref_);
            }
            cmd.Parameters.AddWithValue("login", txtIndentifiant.Text.Trim());
            int nbre = (int)Dao.GetScalarData(cmd);
            if (nbre != 0)
            {
                throw new Exception("Impossible de créer plusieurs utilisateurs avec le même identifiant");
            }
        }

        #region "EVENTS"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ChargerEntete();
                if (!IsPostBack)
                {
                    ChargerTableau();
                    GlobalFunction.ChargerDropProfil(DropProfil);
                    if (ref_ != "0")
                    {
                        Charger();
                    }

                }
                InitFiche();
            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }
        }

        private void Charger()
        {
            string req = @"SELECT * 
            FROM " + tableName + @"             
            WHERE etat_utilisateur = 'A' AND ref_utilisateur = @ref";
            SqlCommand cmd = new SqlCommand(req);

            cmd.Parameters.AddWithValue("ref", ref_);
            DataTable dt = Dao.GetData(cmd);
            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception("ECHEC chargement");
            }
            DataRow r = dt.Rows[0];
            txtIndentifiant.Text = Convert.ToString(r["login_utilisateur"]);
            txtNom.Text = Convert.ToString(r["nom_utilisateur"]);
            txtPrenom.Text = Convert.ToString(r["prenom_utilisateur"]);
            txtMdp.TextMode = 0;
            txtMdp.Text = Convert.ToString(r["password_utilisateur"]);
            
            //txtEmail.Text = Convert.ToString(r["email_utilisateur"]);
            DropEtat.SelectedValue = Convert.ToString(r["etat_utilisateur"]);
            DropProfil.SelectedValue = Convert.ToString(r["refprofil_utilisateur"]);


        }

        protected void btnCreer_ServerClick(object sender, EventArgs e)
        {
            try
            {
                validerChamps();
                checkExistIdentifiant();
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
                Response.Redirect("utilisateurs.aspx?a=a&i=0");

            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                validerChamps();

                string password = "";
                if (txtMdp.Text != "")
                {
                    password = ",password_utilisateur = @password_utilisateur";
                }
                string req = @"UPDATE "+tableName+@" set
                                   login_utilisateur = @login_utilisateur
                                   " + password + @"
                                   ,nom_utilisateur= @nom_utilisateur
                                   ,prenom_utilisateur=@prenom_utilisateur                                   
                                   ,refprofil_utilisateur=@refprofil_utilisateur
                                   ,etat_utilisateur =@etat_utilisateur                                   
                                   ,dmodification_utilisateur = GETDATE()
                                   ,refacteur_utilisateur= @ref_acteur
                                    ,email_utilisateur= @email_utilisateur
                                WHERE ref_utilisateur = @refUser";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("login_utilisateur", txtIndentifiant.Text.Trim());
                if (password != "")
                {
                    cmd.Parameters.AddWithValue("password_utilisateur", txtMdp.Text);
                }
                cmd.Parameters.AddWithValue("nom_utilisateur", txtNom.Text);
                cmd.Parameters.AddWithValue("prenom_utilisateur", txtPrenom.Text);
                cmd.Parameters.AddWithValue("refprofil_utilisateur", DropProfil.SelectedValue);                
                cmd.Parameters.AddWithValue("etat_utilisateur", DropEtat.SelectedValue);
                cmd.Parameters.AddWithValue("ref_acteur", user.refUtilisteur);
                cmd.Parameters.AddWithValue("email_utilisateur", "");
                cmd.Parameters.AddWithValue("refUser", ref_);

                int res = Dao.SaveData(cmd);
                if (res < 0)
                {
                    throw new Exception("ECHEC modification");
                }
                Response.Redirect("utilisateurs.aspx?a=a&i=0");
            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string req = @"UPDATE " + tableName + @" set                                   
                                   etat_utilisateur ='S'                                   
                                   ,dmodification_utilisateur = GETDATE()
                                   ,refacteur_utilisateur= @ref_acteur
                                WHERE ref_utilisateur = @refUser";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("ref_acteur", user.refUtilisteur);
                cmd.Parameters.AddWithValue("refUser", ref_);

                int res = Dao.SaveData(cmd);
                if (res < 0)
                {
                    throw new Exception("ECHEC suppression");
                }
                Response.Redirect("utilisateurs.aspx?a=a&i=0");

            }
            catch (Exception ex)
            {
                GlobalFunction.AfficherDialog(lblMsg, srcpt1, ex.Message);
            }
        }
        #endregion
    }
}