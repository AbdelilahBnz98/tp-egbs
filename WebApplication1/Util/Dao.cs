using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSA_Clementine1.Util
{
    public class Dao
    {

        private static SqlConnection cnx;

        private static SqlConnection GetSqlConnection() {
            string strg_cnx = GlobalInfo.CNX_STRING_SQL;
            
            return new SqlConnection(strg_cnx);
        }

        public static DataTable GetData(SqlCommand cmd) {
            DataTable dt=new DataTable();
            cmd.Connection = GetSqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);       
            return dt;
        }
        public static object GetScalarData(SqlCommand cmd)
        {
            object v=null;
            DataTable dt = new DataTable();
            cmd.Connection = GetSqlConnection();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count >=1) {
               v= dt.Rows[0][0];
            }
            return v;
        }

        internal static int SaveData(SqlCommand cmd)
        {
            int resultat = 0;
            if (cmd.Connection == null) {
                cmd.Connection = GetSqlConnection();
            }           
            try
            {
                if (cmd.Connection.State != ConnectionState.Open) {
                    cmd.Connection.Open();
                }
                
                resultat = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (cmd.Transaction == null) {
                    cmd.Connection.Close();
                }
                
            }
            return resultat;
        }

        internal static SqlConnection GetTransaction()
        {
            if (cnx == null) {
                cnx = GetSqlConnection();
            }
            return cnx;
        }

        internal static void CloseCnx()
        {
            if (cnx != null && cnx.State != ConnectionState.Closed) {                
                cnx.Close();
            }
        }
    }
}