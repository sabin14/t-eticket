using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
namespace prabhuEticket.AppCode
{
    public class httpConnect
    {
        public T sendRequest<T>(String url, WebHeaderCollection headers = null, string data = "", string method = "GET", string contentType = "application/json")
        {
            T output = default(T);
            string response = "";
            var configUrl = ConfigurationManager.AppSettings["api_url"];
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(configUrl == null ? "http://prabhudigital.com/api/" : configUrl.ToString() + url + (method.ToUpper() == "GET" ? "?" + data : ""));
            System.Diagnostics.Debug.WriteLine("url=" + configUrl + url + (method.ToUpper() == "GET" ? "?" + data : ""));
            //httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = contentType;
            string postData = data;
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            httpWebRequest.Headers.Add(headers);
            if (method.ToUpper() == "POST" || method.ToUpper() == "PUT" || method.ToUpper() == "DELETE")
            {
                httpWebRequest.ContentLength = byteArray.Length;
                try
                {
                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var responseString = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
                    response = responseString.Trim();
                    httpResponse.Close();
                }
                catch (WebException ex)
                {
                    if (ex.Response == null || ex.Status != WebExceptionStatus.ProtocolError)
                    {
                        response = "{\"status\":false,\"status_code\":\"400\",\"message\":\"No Response from server\"}";
                    }
                    else
                    {
                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)ex.Response;
                        Stream responseStream = myHttpWebResponse.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                        response = myStreamReader.ReadToEnd().Trim();
                        myStreamReader.Close();
                        responseStream.Close();
                        myHttpWebResponse.Close();
                    }
                }
            }
            else
            {
                try
                {
                    WebResponse resp = httpWebRequest.GetResponse();
                    StreamReader sr = new StreamReader(resp.GetResponseStream());
                    response = sr.ReadToEnd().Trim();
                    sr.Close();
                    resp.Close();
                }
                catch (WebException ex)
                {
                    if (ex.Response == null || ex.Status != WebExceptionStatus.ProtocolError)
                    {
                        response = "{\"status\":false,\"status_code\":\"400\",\"message\":\"No Response from server\"}";
                    }
                    else
                    {
                        HttpWebResponse myHttpWebResponse = (HttpWebResponse)ex.Response;
                        Stream responseStream = myHttpWebResponse.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                        response = myStreamReader.ReadToEnd().Trim();
                        myStreamReader.Close();
                        responseStream.Close();
                        myHttpWebResponse.Close();
                    }
                }
            }
            JsonObject jObject = new JsonObject();
            output = jObject.DeserializeObject<T>(response);
            return output;
        }
    }
}