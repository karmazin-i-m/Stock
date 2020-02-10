using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.Helpers
{
    public static class WebHelper
    {
        public static async Task<String> PostRequestAsync(String uri, Object serializeObj, String contentType = "application/json")
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";

            string data = JsonConvert.SerializeObject(serializeObj, Formatting.Indented);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = contentType;
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            String stringResponse;
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    stringResponse = reader.ReadToEnd();
                }
            }
            response.Close();
            return stringResponse;
        }
    }
}
