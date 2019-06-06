using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace prabhuEticket.AppCode
{
    public class JsonObject
    {
        public string Serialize<T>(T allObj)
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(allObj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, allObj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public T Deserialize<T>(string json)
        {
            T allObj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(allObj.GetType());
            allObj = (T)serializer.ReadObject(ms);
            ms.Close();
            return allObj;
        }
        public T DeserializeObject<T>(string json)
        {
            JavaScriptSerializer JS = new JavaScriptSerializer();
            T allObj = (T)JS.Deserialize(json, typeof(T));
            return allObj;
        }
    }
}