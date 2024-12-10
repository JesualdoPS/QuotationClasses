using System.Text;
using Calc.Persistance;
using Contracts;
using Newtonsoft.Json;

namespace CalculatorApp
{
    internal class WebApiClient
    {
        private readonly HttpClient _httpClient;
        public WebApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7299");
        }
        private class CalculateRequest
        {
            public string Input { get; set; }
        }

        public async Task<MathLog> Calculate(string input)
        {
            var request = new CalculateRequest { Input = input };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Calculator/Calculate", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var mathLog = JsonConvert.DeserializeObject<MathLogEntity>(jsonResult);
                var result = mathLog.FromEntity();
                return result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}

