using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Stock.ClientWPF.Helpers
{
    public static class WebHelper
    {
        /// <summary>
        /// Метод ля создания пост запроса на ресурс
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="serializeObj"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<String> PostRequestAsync(String uri, Object serializeObj, String contentType = "application/json")
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";

            string data = JsonConvert.SerializeObject(serializeObj, Formatting.Indented);
            MessageBox.Show(data);
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
