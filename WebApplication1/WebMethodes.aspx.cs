using CSA_Clementine1.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSA_Clementine1
{
    public partial class WebMethodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //GetFormule

        [WebMethod]
        public static string[] GetFacAvByClient(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(ref_facture,'$',FACNUM)  
                        from facture
                        WHERE etat = 'A' AND FACCLT =  @FACCLT
                        AND FACNUM LIKE  CONCAT('%',@name,'%') 
                        union select CONCAT(ref_avoir,'$',AVNUM)  
                        from avoir
                        WHERE etat = 'A' AND AVCLT =  @FACCLT
                        AND AVNUM LIKE  CONCAT('%',@name,'%') 
                       
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("FACCLT", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune facture ouverte pour ce client)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetVarieteByEspeceEtFact(string prefix, string condition)
        {

            string espece = "";
            string numFact = "";
            if (condition.Split('!').Length == 2)
            {
                numFact = condition.Split('!')[0];
                espece = condition.Split('!')[1];
            }

            List<string> ls_result = new List<string>();
            try
            {
                string req = @"select 
CONCAT(VARCOD,'!',fd.COLQTE,'!',fd.pu_facture,'$',VARDESF) 
from facture f 
JOIN facture_detail fd ON f.ref_facture = fd.refcol_detailcolisage AND fd.etat = 'A'
JOIN liste_variete lv ON lv.VARCOD = fd.COLVAR
WHERE lv.etat = 'A'
AND  CONCAT(VARCOD,'$',VARDESF) LIKE CONCAT('%',@name,'%')
AND  lv.TYPE = @condition1
AND  f.ref_facture = @condition2
ORDER BY VARDESF";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("condition1", espece);
                cmd.Parameters.AddWithValue("condition2", numFact);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune variété trouvée pour cette espèce)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetEspeceByFact(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select CONCAT(TYPCOD,'$',TYPLIB) 
                
                from facture f 
                JOIN facture_detail fd ON f.ref_facture = fd.refcol_detailcolisage AND fd.etat = 'A'
                JOIN [liste_type] lt ON lt.TYPCOD = fd.COLMAR
                
                WHERE lt.etat = 'A' AND f.ref_facture = @FACTNUM
                AND  CONCAT(TYPCOD,'$',TYPLIB) LIKE CONCAT('%',@name,'%')
                order by TYPLIB ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("FACTNUM", condition);
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune espèce trouvée pour cette facture)");
                }

            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }


        //GetFacture
        [WebMethod]
        public static string[] GetFacture(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(FACNUM,'$',FACNUM)  
                        from facture
                        WHERE etat = 'A' 
                        AND FACNUM LIKE  CONCAT('%',@name,'%') 
                        
                        ORDER BY dcreation DESC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune facture trouvée)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }


        [WebMethod]
        public static string[] GetFacByClient(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(ref_facture,'$',FACNUM)  
                        from facture
                        WHERE etat = 'A' AND FACCLT =  @FACCLT
                        AND FACNUM LIKE  CONCAT('%',@name,'%') 
                        
                        ORDER BY dcreation DESC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("FACCLT", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune facture trouvée pour ce client)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
        //getFactureByColisage
        [WebMethod]
        public static string[] GetFacByColi(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(ref_facture,'$',FACNUM)  
                        from facture
                        WHERE etat = 'A' AND FACCOL1 =  @FACCLT
                        AND FACNUM LIKE  CONCAT('%',@name,'%') 
                        
                        ORDER BY dcreation DESC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("FACCLT", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune facture trouvée pour ce client)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetEspece(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(TYPCOD,'$',TYPLIB) 
                from [liste_type] 
                WHERE etat = 'A'
                AND  CONCAT(TYPCOD,'$',TYPLIB) LIKE CONCAT('%',@name,'%')
                AND liste_type.TYPCOD IN (select distinct TYPE from liste_variete)
                order by TYPLIB ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste espèce vide)");
                }

            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        //getPort
        [WebMethod]
        public static string[] GetPort(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(PORCOD,'$',PORDES)
                from liste_port
                WHERE etat = 'A'
                AND CONCAT(PORCOD,'$',PORDES) LIKE CONCAT('%',@name,'%')
                order by PORCOD";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste port vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetVariete(string prefix)
        {

            List<string> ls_result = new List<string>();
            try
            {
                string req = @"select 
                                 CONCAT(VARCOD,'$',VARDESF) 
                                from liste_variete
                                WHERE etat = 'A'
                                AND  CONCAT(VARCOD,'$',VARDESF) LIKE CONCAT('%',@name,'%')
                                ORDER BY VARDESF";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste variété vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        //
        [WebMethod]
        public static string[] GetVarieteByEspece(string prefix, string condition)
        {

            List<string> ls_result = new List<string>();
            try
            {
                string req = @"select 
                                 CONCAT(VARCOD,'$',VARDESF) 
                                from liste_variete
                                WHERE etat = 'A'
                                AND  CONCAT(VARCOD,'$',VARDESF) LIKE CONCAT('%',@name,'%')
                                AND  TYPE = @condition
                                ORDER BY VARDESF";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("condition", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune variété trouvée pour cette espèce)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetFormule(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select CONCAT(FORNUM,'$',FORNUM)
from formule
WHERE etat = 'A'
AND CONCAT(FORNUM,'$',FORNUM) LIKE CONCAT('%',@name,'%')
order by FORNUM";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste formule vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }


        [WebMethod]
        public static string[] GetClient(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(CLTCOD,'$',CLTDES)
                from liste_client
                WHERE etat = 'A'
                AND CONCAT(CLTCOD,'$',CLTDES) LIKE CONCAT('%',@name,'%')
                order by CLTCOD";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste client vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }


        //gettransitair
        [WebMethod]
        public static string[] GetTransitaire(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(CLTCOD,'$',CLTDES)
                from liste_transitaire
                WHERE etat = 'A'
                AND CONCAT(CLTCOD,'$',CLTDES) LIKE CONCAT('%',@name,'%')
                order by CLTCOD";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste transitaire vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
        //GetTransporteur
        [WebMethod]
        public static string[] GetTransporteur(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select top 10 CONCAT(CLTCOD,'$',CLTDES)
                from liste_trasporteur
                WHERE etat = 'A'
                AND CONCAT(CLTCOD,'$',CLTDES) LIKE CONCAT('%',@name,'%')
                order by CLTCOD";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
        [WebMethod]
        public static string[] GetColisage(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(COLNUM,'$',COLNUM)
                        from colisage where etat = 'A' 
                        AND COLNUM LIKE  CONCAT('%',@name,'%')  
                        ORDER BY COLNUM                                                                       
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucun colisage trouvé)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
        [WebMethod]
        public static string[] GetPO(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(po_commande,'$',po_commande)  
                        from commande
                        WHERE etat = 'A'
                        AND po_commande LIKE  CONCAT('%',@name,'%')                         
                        ORDER BY po_commande ASC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucun PO trouvé)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetCmdByClient(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(ref_commande,'!',CLTPAYS,'!',CA,'$',CDENUM)  
                        from commande
						join liste_client c ON c.CLTCOD = CDECLT AND c.etat = 'A'
                        WHERE commande.etat = 'A' AND CDECLT =  @CDECLT
                        AND CDENUM LIKE  CONCAT('%',@name,'%') 
                        AND statut_commande = 'VALIDEE'
                        ORDER BY commande.dcreation DESC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                cmd.Parameters.AddWithValue("CDECLT", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune commande ouverte pour ce client)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetColByClient(string prefix, string condition)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(COLNUM,'!',colnbc,'!',cmd.po_commande,'!',MONO,'$',COLNUM)    
                        from colisage c
                        JOIN commande cmd ON c.COLNBC = cmd.CDENUM AND cmd.etat = 'A'
                        
                        WHERE c.etat = 'A'
                        AND CDECLT = @filter
                        AND COLNUM LIKE CONCAT('%',@prefix,'%')
                        AND c.statut_colisage = 'VALIDEE'
                        ORDER BY c.ref_colisage
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("prefix", prefix);
                cmd.Parameters.AddWithValue("filter", condition);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1! $(Aucun colisage trouvé pour ce client)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetAvoir(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(AVNUM,'$',AVNUM)  
                        from avoir
                        WHERE etat = 'A'
                        AND AVNUM LIKE  CONCAT('%',@name,'%')                         
                        ORDER BY AVNUM ASC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucun avoir trouvé)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetCmd(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"
                        select CONCAT(CDENUM,'$',CDENUM)  
                        from commande
                        WHERE etat = 'A'
                        AND CDENUM LIKE  CONCAT('%',@name,'%')                         
                        ORDER BY CDENUM ASC
                        ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucune commande trouvée)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
        [WebMethod]
        public static string[] GetDum(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"                                                
                        select  CONCAT(num_dum,'$',num_dum)
                        from DUM
                        WHERE etat = 'A'
                        AND num_dum LIKE CONCAT('%',@prefix,'%')
                        ORDER BY dmodif DESC ";
                SqlCommand cmd = new SqlCommand(req, null);
                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("prefix", prefix);

                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Aucun DUM encours trouvé)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetDevise(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(DEVCOD,'$',DEVDES) 
                from [liste_devise] 
                WHERE etat = 'A'
                AND  CONCAT(DEVCOD,'$',DEVDES) LIKE CONCAT('%',@name,'%')
                order by DEVDES ";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                if (prefix == " ") {
                    prefix = "%";
                }
                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }

                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(liste devise vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetPays(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select  CONCAT(PAYCOD,'$',PAYDES) 
                from [liste_pays] 
                WHERE etat = 'A'
                AND  CONCAT(PAYCOD,'$',PAYDES) LIKE CONCAT('%',@name,'%')
                order by PAYDES ";
                SqlCommand cmd = new SqlCommand(req, null);

                if (prefix == " ")
                {
                    prefix = "%";
                }
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste pays vide)");
                }

            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetVille(string prefix)
        {
            List<string> ls_result = new List<string>();
            try
            {
                string req = @"select  CONCAT(VILCOD,'$',VILDES) 
                from [liste_ville] 
                WHERE etat = 'A'
                AND  CONCAT(VILCOD,'$',VILDES) LIKE CONCAT('%',@name,'%')
                order by VILDES";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste ville vide)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetVillePays(string prefix,string condition)
        {
            List<string> ls_result = new List<string>();
            try
            {
                string req = @"select  CONCAT(VILCOD,'$',VILDES) 
                from [liste_ville] 
                WHERE etat = 'A' 
                AND VILPAYS = @condition
                AND  CONCAT(VILCOD,'$',VILDES) LIKE CONCAT('%',@name,'%')
                order by VILDES";
                SqlCommand cmd = new SqlCommand(req, null);

                if (prefix == " ") {
                    prefix = "%";
                }

                cmd.Parameters.AddWithValue("condition", condition);
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);



                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste ville vide pour ce pays)");
                }
            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }

        [WebMethod]
        public static string[] GetBanque(string prefix)
        {
            List<string> ls_result = new List<string>();

            try
            {
                string req = @"select CONCAT(BQCOD,'$',BQDES) 
                from [liste_banque] 
                WHERE 
CONCAT(BQCOD,'$',BQDES) LIKE CONCAT('%',@name,'%')
                order by BQDES";
                SqlCommand cmd = new SqlCommand(req, null);
                cmd.Parameters.AddWithValue("name", prefix);
                DataTable dt = Dao.GetData(cmd);

                foreach (DataRow r in dt.Rows)
                {
                    ls_result.Add(Convert.ToString(r[0]));
                }
                if (dt.Rows.Count == 0)
                {
                    ls_result.Add("-1$(Liste banque vide)");
                }

            }
            catch (Exception ex)
            {
                ls_result = new List<string>();
                ls_result.Add("" + ex.Message);
            }
            return ls_result.ToArray();
        }
    }
}