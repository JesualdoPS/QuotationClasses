using System.Text;
using Calc.Persistance;
using Contracts;
using Newtonsoft.Json;
using UnitsNet;
using Polly;

namespace CalculatorApp
{
    public class Calculator : ICalculator
    {
        private readonly HttpClient _httpClient;
        public List<MathLog> Memory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Calculator()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _httpClient.BaseAddress = new Uri("https://localhost:7299");
        }

        public async Task<MathLog> Calculate(string input)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new CalculateRequest { Input = input };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/Calculate", content);
                    response.EnsureSuccessStatusCode();

                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var mathLog = JsonConvert.DeserializeObject<MathLogEntity>(jsonResult);
                    return mathLog.FromEntity();
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<double> Add(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("/Calculator/AddNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<double>(responseBody);
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<double> Subtract(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/SubtractNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<double>(responseBody);
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<double> Multiply(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/MultiplyNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<double>(responseBody);
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<double> Divide(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/DivideNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<double>(responseBody);
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<Mass> CalculateWeight(Volume materialVolume, Materials material)
        {
            throw new NotImplementedException();
        }

        public async Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    var request = new VolumeRequest()
                    {
                        Length1 = length1.ToSerializable(),
                        Length2 = length2.ToSerializable(),
                        Length3 = lenght3.ToSerializable()
                    };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/MultiplyVolume", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var unitsValue = JsonConvert.DeserializeObject<SerializableUnitsValue>(responseBody);
                    return (Volume)unitsValue.ToIQuantity();
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Connection failed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}");
            }
        }
    }
}

