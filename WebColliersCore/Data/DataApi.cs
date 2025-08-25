using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace WebLomelinCore.Data
{
    public class DataApi
    {
        public static async Task<HttpResponseMessage> CallPostMethod(Uri urlApi, object data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = urlApi;
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content); // Synchronous call for simplicity
                if (response.IsSuccessStatusCode)
                {
                    return response; // Synchronous call for simplicity
                }
                else
                {
                    throw new Exception($"Error updating data: {response.ReasonPhrase}");
                }
            }
        }
    }
}
