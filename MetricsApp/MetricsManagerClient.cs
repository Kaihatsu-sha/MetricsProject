using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsApp
{
    internal static class MetricsManagerClient<T>
    {
        private static readonly string _managerUrl = "http://localhost:5000/api/";
        public static async Task<IEnumerable<T>> GetValues(string url)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_managerUrl}{url}");
            HttpResponseMessage response = await client.SendAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        List<T> result = await JsonSerializer.DeserializeAsync<List<T>>(
                            responseStream,
                            new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return null;
        }
    }
}
