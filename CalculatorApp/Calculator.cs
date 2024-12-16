using System.Text;
using Calc.Persistance;
using Contracts;
using Newtonsoft.Json;
using UnitsNet;

namespace CalculatorApp
{
    public class Calculator : ICalculator
    {
        private readonly HttpClient _httpClient;

        public List<MathLog> Memory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Calculator()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7299");
        }

        public async Task<MathLog> Calculate(string input)
        {
            int maxTries = 3;
            int tryNumber = 1;

            while (tryNumber <= maxTries)
            {
                try
                {
                    var request = new CalculateRequest { Input = input };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/Calculate", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResult = await response.Content.ReadAsStringAsync();
                        var mathLog = JsonConvert.DeserializeObject<MathLogEntity>(jsonResult);
                        var result = mathLog.FromEntity();
                        return result;
                    }
                    else
                    {
                        throw new Exception($"Calulation error: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new HttpRequestException("Number of tries was reached");
        }

        public async Task<double> Add(double v1, double v2)
        {
            int maxTries = 3;
            int tryNumber = 0;
            while (tryNumber < maxTries)
            {
                try
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("/Calculator/AddNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<double>(responseBody);

                    return result;
                }
                catch (HttpRequestException ex)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new Exception("Number of tries was reached");
        }

        public async Task<double> Subtract(double v1, double v2)
        {
            int maxTries = 3;
            int tryNumber = 0;
            while (tryNumber < maxTries)
            {
                try
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/SubtractNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<double>(responseBody);

                    return result;
                }
                catch (HttpRequestException ex)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new Exception("Number of tries was reached");
        }

        public async Task<double> Multiply(double v1, double v2)
        {
            int maxTries = 3;
            int tryNumber = 0;
            while (tryNumber < maxTries)
            {
                try
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/MultiplyNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<double>(responseBody);

                    return result;
                }
                catch (HttpRequestException ex)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new Exception("Number of tries was reached");
        }

        public async Task<double> Divide(double v1, double v2)
        {
            int maxTries = 3;
            int tryNumber = 0;
            while (tryNumber < maxTries)
            {
                try
                {
                    var request = new NormalRequest { Value1 = v1, Value2 = v2 };
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("Calculator/DivideNumbers", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<double>(responseBody);

                    return result;
                }
                catch (HttpRequestException ex)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new Exception("Number of tries was reached");
        }

        public async Task<Mass> CalculateWeight(Volume materialVolume, Materials material)
        {
            throw new NotImplementedException();
        }

        public async Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3)
        {
            int maxTries = 3;
            int tryNumber = 0;
            while (tryNumber < maxTries)
            {
                try
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
                    var result = (Volume)unitsValue.ToIQuantity();

                    return result;
                }
                catch (HttpRequestException ex)
                {
                    tryNumber++;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            throw new Exception("Number of tries was reached");
        }
    }
}

