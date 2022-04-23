using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SlipAdvice
{
    public class Management
    {
        public async Task<List<string>> GetSlipAdvices(int count)
        {
            List<string> advices = new List<string>();
            try
            {
                var client = new HttpClient();
                int requisicoes = 0;

                while (requisicoes < count)
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api.adviceslip.com/advice")
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var resp = await response.Content.ReadAsStringAsync();
                        SlipJson slipJson = JsonConvert.DeserializeObject<SlipJson>(resp);

                        if (!advices.Contains(slipJson.slip.advice))
                        {
                            advices.Add(slipJson.slip.advice);
                            requisicoes++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return advices;
        }
    }
}
