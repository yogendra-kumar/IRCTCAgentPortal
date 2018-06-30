using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mpower.Rail.NGETSystem.Models.Response;
using Newtonsoft.Json;

namespace Mpower.Rail.NGETSystem.Processor
{
    public class ServiceProxy
    {
        public int _service;
        //public Error error = new Error();
        public ServiceProxy()
        {

        }

        // public HttpClient MakeRequest(string url, string body,string methodtype,long? usersession)
        // {
        //     ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

        //     HttpClient req = HttpClient.Create(url) as HttpWebRequest;
        //     req.Method = methodtype;
        //     req.Accept = "application/json";
        //     req.MediaType = "application/json";
        //     req.ContentType = "application/json";
        //     //req.Host = "irctc.co.in";
        //     req.KeepAlive = true;
        //     string authInfo = ApplicationConfig.NgetMasterId + ":" + ApplicationConfig.NgetMasterpwd;
        //     authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        //     req.Headers["Authorization"] = "Basic " + authInfo;
        //     req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";

        //     req.CookieContainer = new CookieContainer();

        //     if (methodtype == "POST")
        //     {
        //         byte[] byteData = UTF8Encoding.UTF8.GetBytes(body);
        //         req.ContentLength = byteData.Length;
        //         using (Stream postStream = req.GetRequestStream())
        //         {
        //             postStream.Write(byteData, 0, byteData.Length);
        //         }
        //     }

        //     //Utility.SaveLog("WS", CommanFunctions.LogType.IRCTCCOMMUNICATION.ToString(), "Request URI : "+uri, usersession);

        //     return req;
        // }

        public Object GetResponse(string body, string methodType, string uri, long? usersession,string authInfo)
        {
            //string authInfo = "b2bms6" + ":" + "Testing1";
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            string response = "";
            //set certificates
            using (var client = new HttpClient())
            {
                if (methodType == "GET")
                {
                    var contentData = new StringContent(body, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    using (var result = client.GetAsync(uri).Result)
                    {
                        if (!result.IsSuccessStatusCode)
                        {
                            return null;
                        }
                       // Encoding enc = System.Text.Encoding.GetEncoding(1252);
                        response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject(response);
                    }
                }
                if (methodType == "POST")
                {
                    var contentData = new StringContent(body, Encoding.UTF8, "application/json");
                    //contentData.Headers.Add("Authorization", "Basic" + authInfo);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                    using (var result = client.PostAsync(uri, contentData).Result)
                    {
                        if (!result.IsSuccessStatusCode)
                        {
                            return null;
                        }
                        //Encoding enc = System.Text.Encoding.GetEncoding(1252);
                        response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject(response);
                    }
                }
                return "";
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }
    }
}