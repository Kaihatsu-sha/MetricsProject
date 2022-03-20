using MetricsManager.Entities;
using MetricsManager.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public class HttpClientService<TEntityDto> : IHttpClientService<TEntityDto>
        where TEntityDto : BaseEntityDto
    {
        private readonly HttpClient _client;

        public HttpClientService(string clietnName, IHttpClientFactory factory)
        {
            _client = factory.CreateClient(clietnName);
        }

        public async Task<List<TEntityDto>> GetResponse(Uri url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                {
                    List<TEntityDto> result = await JsonSerializer.DeserializeAsync<List<TEntityDto>>(
                        responseStream, 
                        new JsonSerializerOptions(JsonSerializerDefaults.Web));

                    return result;
                }
            }

            throw new ArgumentNullException("Bad response");
        }
    }
}
