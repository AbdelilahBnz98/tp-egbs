using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;


namespace CSA_Clementine1.Util
{
    public class Utilitaire
    {

        public  static string ShowFloat(string v)
        {
            return v.Replace(",", ".");
        }
        public static string formaterArgent(string value,bool avecVirgule) {
            if (value != "") {
                double d = Convert.ToDouble(value);
                if (avecVirgule)
                {
                    return d.ToString("### ### ### ### ##0.#0");
                }
                else {
                    return d.ToString("### ### ### ### ##0.##");
                }
                
            }
            return value;
        }

        internal static string formaterDate(string value,string format)
        {
            if (value != "")
            {
                DateTime d = Convert.ToDateTime(value);
                return d.ToString(format);
            }
            return value;
        }

        public static string GetExchangeRate(string from, string to = "MAD")
        {
            //Examples:
            //from = "EUR"
            //to = "USD"
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    string result = client.DownloadString("https://free.currencyconverterapi.com"+ $"/api/v6/convert?q={from}_{to}&compact=y");

                    //API_Obj Test = (API_Obj)JsonConvert.DeserializeObject(result);
                    //return Test.result.result.ToString();
                                                            
                    //data = {"EUR_USD":{"val":1.140661}}
                    //I want to return 1.140661
                    //EUR_USD is dynamic depending on what from/to is
                    
                }
                catch (Exception httpRequestException)
                {
                    Console.WriteLine(httpRequestException.StackTrace);
                    return "Error calling API. Please do manual lookup.";
                }
            }
        }
    }
    public class API_Obj
    {
        public val result { get; set; }
        
    }
    public class val
    {
        public float result { get; set; }

    }
}