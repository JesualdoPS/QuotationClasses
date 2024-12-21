using Calc.Persistance;
using Contracts;
using Newtonsoft.Json;
using UnitsNet;
using Polly;

namespace CalculatorApp
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Calculator : ICalculator
    {
        private readonly HttpClient _httpClient;

        public List<MathLog> Memory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Calculator(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _httpClient.BaseAddress = new Uri("https://localhost:5044");
        }

        private static System.Text.Json.JsonSerializerOptions CreateSerializerSettings()
        {
            var retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(10, _ => TimeSpan.FromSeconds(3));

            var request = new CalculateRequest { Input = input };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {  
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

        public virtual System.Threading.Tasks.Task<double> Add(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
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

            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
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

            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
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

        public virtual System.Threading.Tasks.Task<double> Divide(double v1, double v2)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            return DivideAsync(request, System.Threading.CancellationToken.None);
        }
                
        public virtual async System.Threading.Tasks.Task<double> DivideAsync(NormalRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var json_ = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(request, JsonSerializerSettings);
                    var content_ = new System.Net.Http.ByteArrayContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    urlBuilder_.Append("Calculator/DivideNumbers");

                    PrepareRequest(client_, request_, urlBuilder_);

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
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

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<double>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public virtual async Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3)
        {
            var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

            var request = new VolumeRequest()
            {
                Length1 = length1.ToSerializable(),
                Length2 = length2.ToSerializable(),
                Length3 = lenght3.ToSerializable()
            };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
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

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }
}