using Newtonsoft.Json;
using prabhuEticket_api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace prabhuEticket_api.AppCode
{
    public class globFunction
    {
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

        public SqlConnection SqlConnection;
        public static string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                string info = new WebClient().DownloadString("https://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

            return ipInfo.Country;
        }
        public DataTable RunSQL(String sql)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
        }
        public System.Threading.Tasks.Task<DataTable> RunSQLAsync(String sql)
        {
            SqlConnection = new SqlConnection(ConfigurationManager.AppSettings["DBConnString"].ToString());
            DataTable dt = new DataTable();

            return System.Threading.Tasks.Task<DataTable>.Factory.StartNew(() =>

            {

                using (SqlConnection con = SqlConnection)

                {

                    string sqlSelect = sql;

                    SqlDataAdapter da = new SqlDataAdapter(sqlSelect, con);

                    da.Fill(dt);
                    con.Close();

                }

                Console.WriteLine("Thread: " + System.Threading.Thread.CurrentThread.Name);

                return dt;

            });

        }
        public DataSet RunSQLDataSet(String sql)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet dt = new DataSet();
                        da.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
        }
        public string wordReader(string str)
        {
            try
            {
                byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(str);

                // Convert utf-8 bytes to a string.
                string s_unicode2 = System.Text.Encoding.UTF8.GetString(utf8Bytes);
                string converted = "";
                try
                {
                    converted = HttpUtility.HtmlDecode(s_unicode2).ToString();
                    TextBox te = new TextBox();
                    te.Text = System.Net.WebUtility.HtmlDecode(converted);
                    converted = te.Text;
                }
                catch
                {
                    converted = s_unicode2;
                }

                return converted;
            }
            catch
            {
                return "";
            }
        }
        public string singleQuote(string str)
        {
            try
            {
                if (str == "" || str == null || string.IsNullOrEmpty(str) == true)
                {
                    return "Null";
                }
                else
                {
                    str = str.Replace("'", "''");
                    return "'" + str + "'";
                }
            }
            catch
            {
                return "Null";
            }
        }
        public string singleQuoteNvarchar(string str)
        {
            try
            {
                if (str == "" || str == null || string.IsNullOrEmpty(str) == true)
                {
                    return "Null";
                }
                else
                {
                    var bytes = Encoding.UTF8.GetBytes(str.Replace("'", "''"));
                    var new_str = Encoding.UTF8.GetString(bytes);
                    return "N'" + new_str + "'";
                }
            }
            catch
            {
                return "Null";
            }
        }
        public string parseDatetime(object rowDate, string format)
        {
            string fulldate = "";
            try
            {
                fulldate = Convert.ToDateTime(rowDate).ToString(format);
            }
            catch
            {
                fulldate = "";
            }
            return fulldate;
        }
        public string ToXML(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public string GetSHA256(string text)
        {
            SHA256 sha = new SHA256Managed();
            if (text == null || text == "")
            {
                return null;
            }

            Byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
                //stringBuilder.AppendFormat("{0:x3}", b);
            }
            return stringBuilder.ToString();
        }
        public string GetSHA256X3(string text)
        {
            SHA256 sha = new SHA256Managed();
            if (text == null || text == "")
            {
                return null;
            }

            Byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                //stringBuilder.AppendFormat("{0:x2}", b);
                stringBuilder.AppendFormat("{0:x3}", b);
            }
            return stringBuilder.ToString();
        }

        public void ShowError(Label lblMessage, HtmlControl information, string error_list)
        {
            lblMessage.Text = error_list;
            lblMessage.CssClass = "Error";
            information.Style.Value = "color: #FE2400; border: 1px solid #FE2400; clear: both; padding: 5px 3px 1px 3px; font-weight: bold; margin: 15px; text-align: center; height: 30px; line-height: 30px; font-size: 12px; background-image: url(../images/failure.jpg); background-repeat: no-repeat; background-position: 5px center;";
            information.Visible = true;
            lblMessage.Visible = true;
        }
        public void ShowMessage(Label lblMessage, HtmlControl information, string Message)
        {
            lblMessage.Text = Message;
            lblMessage.CssClass = "Message";
            information.Style.Value = "color: green; border: 1px solid green; clear: both; padding: 3px; font-weight: bold; margin: 15px; text-align: center; height: 30px; line-height: 30px; font-size: 12px; background-image: url(../images/success.jpg); background-repeat: no-repeat; background-position: 5px center;";
            information.Visible = true;
            lblMessage.Visible = true;
        }
        public string FormatShortDate(string strDate)
        {
            if (strDate != null && strDate != "")
            {
                DateTime dt = new DateTime();
                dt = DateTime.Parse(strDate);
                strDate = dt.ToString("MMM/dd/yyyy") + dt.ToString(" HH:mm:ss");
            }
            return strDate;
        }
        public string FormatDate(string strDate)
        {
            if (strDate != null && strDate != "")
            {
                DateTime dt = new DateTime();
                dt = DateTime.Parse(strDate);
                strDate = dt.ToString("MMM/dd/yyyy");
            }
            return strDate;
        }
        public String catchError(object ex, String ErrorFrom)
        {
            string Client_ipAdd = "";
            try
            {
                Client_ipAdd = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            catch
            {
                Client_ipAdd = "182.93.89.26";
            }
            String error_id = string.Empty;
            try
            {
                string sqlQuery = "spa_SOAPError " + singleQuote(ex.ToString()) +
                ",'100'," + singleQuote(ErrorFrom) + ",'SendAPI','SOAP'," + singleQuote(Client_ipAdd);

                DataTable dt = RunSQL(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    error_id = dt.Rows[0]["error_id"].ToString();
                }

            }
            catch { }
            return error_id;
        }
        public string GetSHA1(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }

        public string getXMlValue(XmlDocument xmlObj, string NodeName)
        {
            string nodeValue = string.Empty;
            XmlNodeList xmlNodeObj = xmlObj.GetElementsByTagName(NodeName);
            if (xmlNodeObj.Count > 0)
            {
                nodeValue = xmlObj.GetElementsByTagName(NodeName).Item(0).InnerText;

            }
            else
                nodeValue = "";

            return nodeValue;
        }
        public string getXMlAttributeValue(XmlDocument xmlObj, string NodeName, string AttributeName)
        {
            string nodeValue = string.Empty;
            XmlNodeList xmlNodeObj = xmlObj.GetElementsByTagName(NodeName);
            for (int i = 0; i < xmlNodeObj.Count; i++)
            {
                nodeValue = xmlNodeObj[i].Attributes[AttributeName].Value;
            }

            return nodeValue;
        }
        public string getXMlAttrValue(XmlDocument xmlObj, string NodeName, string AttributeName)
        {
            string nodeValue = string.Empty;

            XmlElement root = xmlObj.DocumentElement;

            // Check to see if the element has a genre attribute.
            if (root.HasAttribute(AttributeName))
            {
                nodeValue = root.GetAttribute(AttributeName);

            }

            return nodeValue;
        }

        public string getXMlSameNodeValue(XmlDocument xmlObj, string NodeName, int Index)
        {
            string nodeValue = string.Empty;
            XmlNodeList xmlNodeObj = xmlObj.GetElementsByTagName(NodeName);
            if (xmlNodeObj.Count > 0)
            {
                nodeValue = xmlObj.GetElementsByTagName(NodeName).Item(Index).InnerText;

            }
            else
                nodeValue = "0";

            return nodeValue;
        }

        public string GetIPAddressClient()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
        public string XMLtoJSON(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
    }
}
