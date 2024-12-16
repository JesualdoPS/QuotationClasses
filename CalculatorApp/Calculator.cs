using Calc.Persistance;
using Contracts;
using Newtonsoft.Json;
using UnitsNet;

namespace CalculatorApp
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Calculator : ICalculator
    {
        private string _baseUrl;

        private System.Net.Http.HttpClient _httpClient;
        private static System.Lazy<System.Text.Json.JsonSerializerOptions> _settings = new System.Lazy<System.Text.Json.JsonSerializerOptions>(CreateSerializerSettings, true);
        private System.Text.Json.JsonSerializerOptions _instanceSettings;

        public Calculator(System.Net.Http.HttpClient httpClient)
        {
            BaseUrl = "https://localhost:7299";
            _httpClient = httpClient;
            Initialize();
        }

        private static System.Text.Json.JsonSerializerOptions CreateSerializerSettings()
        {
            var settings = new System.Text.Json.JsonSerializerOptions();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                if (!string.IsNullOrEmpty(_baseUrl) && !_baseUrl.EndsWith("/"))
                    _baseUrl += '/';
            }
        }

        protected System.Text.Json.JsonSerializerOptions JsonSerializerSettings { get { return _instanceSettings ?? _settings.Value; } }
        static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings);
        partial void Initialize();
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        public virtual System.Threading.Tasks.Task<MathLog> Calculate(string input)
        {
            var request = new CalculateRequest { Input = input };
            return CalculateAsync(request, System.Threading.CancellationToken.None);
        }

        public virtual async System.Threading.Tasks.Task<MathLog> CalculateAsync(CalculateRequest request, System.Threading.CancellationToken cancellationToken)
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
                    if (!string.IsNullOrEmpty(_baseUrl))
                        urlBuilder_.Append(_baseUrl);
                    urlBuilder_.Append("Calculator/Calculate");
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;

                    if (!response_.IsSuccessStatusCode)
                        throw new HttpRequestException($"Request send error. Status Code: {response_.StatusCode}");

                    try
                    {
                        if (response_.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var responseContent = await response_.Content.ReadAsStringAsync();
                            try
                            {
                                var objectResponse_ = JsonConvert.DeserializeObject<MathLogEntity>(responseContent);
                                if (objectResponse_ != null)
                                {
                                    var result = objectResponse_.FromEntity();
                                    return result;
                                }
                                else
                                {
                                    throw new ApiException("No answear.", 200, responseContent, null, null);
                                }
                            }
                            catch (JsonSerializationException ex)
                            {
                                throw new ApiException("Deserializing error.", 200, responseContent, null, ex);
                            }
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("Unexpected status code.", (int)response_.StatusCode, responseData_, null, null);
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

        public virtual System.Threading.Tasks.Task<double> Add(double v1, double v2)
        {
            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            return AddAsync(request, System.Threading.CancellationToken.None);
        }

        public virtual async System.Threading.Tasks.Task<double> AddAsync(NormalRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            int retryCount = 0;
            const int maxRetries = 1;
            bool sucesso = false;
            double result = 0;

            while (!sucesso && retryCount < maxRetries)
            {
                try
                {
                    var client_ = _httpClient;
                    var disposeClient_ = false;

                    using (var request_ = new System.Net.Http.HttpRequestMessage())
                    {
                        var json_ = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(request, JsonSerializerSettings);
                        var content_ = new System.Net.Http.ByteArrayContent(json_);
                        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                        request_.Content = content_;
                        request_.Method = new System.Net.Http.HttpMethod("POST");
                        request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                        var urlBuilder_ = new System.Text.StringBuilder();
                        if (!string.IsNullOrEmpty(_baseUrl))
                            urlBuilder_.Append(_baseUrl);
                        urlBuilder_.Append("Calculator/AddNumbers");
                        PrepareRequest(client_, request_, urlBuilder_);
                        var url_ = urlBuilder_.ToString();
                        request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                        PrepareRequest(client_, request_, url_);

                        Console.WriteLine($"Try number {retryCount + 1} of {maxRetries}");
                        await Task.Delay(100);

                        var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                        var disposeResponse_ = true;

                        try
                        {
                            var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                            foreach (var item_ in response_.Headers)
                                headers_[item_.Key] = item_.Value;
                            if (response_.Content != null && response_.Content.Headers != null)
                            {
                                foreach (var item_ in response_.Content.Headers)
                                    headers_[item_.Key] = item_.Value;
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
                                result = objectResponse_.Object;
                                sucesso = true;
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
                catch (HttpRequestException ex)
                {
                    retryCount++;
                    Console.WriteLine($"Erro de conexão. Try number {retryCount} of {maxRetries}.");
                    throw new ApiException("Erro de conexão", 500, null, null, ex);
                }
            }
            if (!sucesso)
            {
                throw new ApiException("Falha após " + maxRetries + " tentativas.", 500, null, null, null);
            }
            return result;
        }

        public virtual System.Threading.Tasks.Task<double> Subtract(double v1, double v2)
        {
            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            return SubtractAsync(request, System.Threading.CancellationToken.None);
        }

        public virtual async System.Threading.Tasks.Task<double> SubtractAsync(NormalRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            int retryCount = 0;
            const int maxRetries = 1;
            bool sucesso = false;
            double result = 0;

            while (!sucesso && retryCount < maxRetries)
            {
                try
                {
                    var client_ = _httpClient;
                    var disposeClient_ = false;

                    using (var request_ = new System.Net.Http.HttpRequestMessage())
                    {
                        var json_ = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(request, JsonSerializerSettings);
                        var content_ = new System.Net.Http.ByteArrayContent(json_);
                        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                        request_.Content = content_;
                        request_.Method = new System.Net.Http.HttpMethod("POST");
                        request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                        var urlBuilder_ = new System.Text.StringBuilder();
                        if (!string.IsNullOrEmpty(_baseUrl))
                            urlBuilder_.Append(_baseUrl);
                        urlBuilder_.Append("Calculator/SubtractNumbers");
                        PrepareRequest(client_, request_, urlBuilder_);
                        var url_ = urlBuilder_.ToString();
                        request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                        PrepareRequest(client_, request_, url_);

                        Console.WriteLine($"Try number {retryCount + 1} of {maxRetries}");
                        await Task.Delay(100);

                        var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                        var disposeResponse_ = true;

                        try
                        {
                            var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                            foreach (var item_ in response_.Headers)
                                headers_[item_.Key] = item_.Value;
                            if (response_.Content != null && response_.Content.Headers != null)
                            {
                                foreach (var item_ in response_.Content.Headers)
                                    headers_[item_.Key] = item_.Value;
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
                                result = objectResponse_.Object;
                                sucesso = true;
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
                catch (HttpRequestException ex)
                {
                    retryCount++;
                    Console.WriteLine($"Connection Error. Try number {retryCount} of {maxRetries}.");
                    throw new ApiException("Connection Error", 500, null, null, ex);
                }
            }
            if (!sucesso)
            {
                throw new ApiException("Failed after " + maxRetries + " tries.", 500, null, null, null);
            }
            return result;
        }

        public virtual System.Threading.Tasks.Task<double> Multiply(double v1, double v2)
        {
            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            return MultiplyAsync(request, System.Threading.CancellationToken.None);
        }

        public virtual async System.Threading.Tasks.Task<double> MultiplyAsync(NormalRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            int retryCount = 0;
            const int maxRetries = 1;
            bool sucesso = false;
            double result = 0;

            while (!sucesso && retryCount < maxRetries)
            {
                try
                {
                    var client_ = _httpClient;
                    var disposeClient_ = false;

                    using (var request_ = new System.Net.Http.HttpRequestMessage())
                    {
                        var json_ = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(request, JsonSerializerSettings);
                        var content_ = new System.Net.Http.ByteArrayContent(json_);
                        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                        request_.Content = content_;
                        request_.Method = new System.Net.Http.HttpMethod("POST");
                        request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                        var urlBuilder_ = new System.Text.StringBuilder();
                        if (!string.IsNullOrEmpty(_baseUrl))
                            urlBuilder_.Append(_baseUrl);
                        urlBuilder_.Append("Calculator/MultiplyNumbers");
                        PrepareRequest(client_, request_, urlBuilder_);
                        var url_ = urlBuilder_.ToString();
                        request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                        PrepareRequest(client_, request_, url_);

                        Console.WriteLine($"Try number {retryCount + 1} of {maxRetries}");
                        await Task.Delay(100);

                        var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                        var disposeResponse_ = true;

                        try
                        {
                            var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                            foreach (var item_ in response_.Headers)
                                headers_[item_.Key] = item_.Value;
                            if (response_.Content != null && response_.Content.Headers != null)
                            {
                                foreach (var item_ in response_.Content.Headers)
                                    headers_[item_.Key] = item_.Value;
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
                                result = objectResponse_.Object;
                                sucesso = true;
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
                catch (HttpRequestException ex)
                {
                    retryCount++;
                    Console.WriteLine($"Connection Error. Try number {retryCount} of {maxRetries}.");
                    throw new ApiException("Connection Error", 500, null, null, ex);
                }
            }
            if (!sucesso)
            {
                throw new ApiException("Failed after " + maxRetries + " tries.", 500, null, null, null);
            }
            return result;
        }

        public virtual System.Threading.Tasks.Task<double> Divide(double v1, double v2)
        {
            var request = new NormalRequest { Value1 = v1, Value2 = v2 };
            return DivideAsync(request, System.Threading.CancellationToken.None);
        }

        public virtual async System.Threading.Tasks.Task<double> DivideAsync(NormalRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            int retryCount = 0;
            const int maxRetries = 1;
            bool sucesso = false;
            double result = 0;

            while (!sucesso && retryCount < maxRetries)
            {
                try
                {
                    var client_ = _httpClient;
                    var disposeClient_ = false;

                    using (var request_ = new System.Net.Http.HttpRequestMessage())
                    {
                        var json_ = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(request, JsonSerializerSettings);
                        var content_ = new System.Net.Http.ByteArrayContent(json_);
                        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                        request_.Content = content_;
                        request_.Method = new System.Net.Http.HttpMethod("POST");
                        request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                        var urlBuilder_ = new System.Text.StringBuilder();
                        if (!string.IsNullOrEmpty(_baseUrl))
                            urlBuilder_.Append(_baseUrl);
                        urlBuilder_.Append("Calculator/DivideNumbers");
                        PrepareRequest(client_, request_, urlBuilder_);
                        var url_ = urlBuilder_.ToString();
                        request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                        PrepareRequest(client_, request_, url_);

                        Console.WriteLine($"Try number {retryCount + 1} of {maxRetries}");
                        await Task.Delay(100);

                        var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                        var disposeResponse_ = true;

                        try
                        {
                            var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                            foreach (var item_ in response_.Headers)
                                headers_[item_.Key] = item_.Value;
                            if (response_.Content != null && response_.Content.Headers != null)
                            {
                                foreach (var item_ in response_.Content.Headers)
                                    headers_[item_.Key] = item_.Value;
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
                                result = objectResponse_.Object;
                                sucesso = true;
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
                catch (HttpRequestException ex)
                {
                    retryCount++;
                    Console.WriteLine($"Connection Error. Try number {retryCount} of {maxRetries}.");
                    throw new ApiException("Connection Error", 500, null, null, ex);
                }
            }
            if (!sucesso)
            {
                throw new ApiException("Failed after " + maxRetries + " tries.", 500, null, null, null);
            }
            return result;
        }

        public virtual async Task<Volume> MultiplyVolume(Length length1, Length length2, Length lenght3)
        {
            var request = new VolumeRequest()
            {
                Length1 = length1.ToSerializable(),
                Length2 = length2.ToSerializable(),
                Length3 = lenght3.ToSerializable()
            };
            var response = await MultiplyVolumeAsync(request, System.Threading.CancellationToken.None);
            return response;
        }

        public virtual async System.Threading.Tasks.Task<Volume> MultiplyVolumeAsync(VolumeRequest request, System.Threading.CancellationToken cancellationToken)
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
                    if (!string.IsNullOrEmpty(_baseUrl))
                        urlBuilder_.Append(_baseUrl);
                    urlBuilder_.Append("Calculator/MultiplyVolume");
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    try
                    {
                        var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                        var disposeResponse_ = true;

                        if (!response_.IsSuccessStatusCode)
                            throw new HttpRequestException($"Request send error. Status Code: {response_.StatusCode}");

                        try
                        {
                            if (response_.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var responseContent = await response_.Content.ReadAsStringAsync();
                                try
                                {
                                    var objectResponse_ = JsonConvert.DeserializeObject<SerializableUnitsValue>(responseContent);
                                    if (objectResponse_ != null)
                                    {
                                        return Volume.FromCubicMeters((double)objectResponse_.Value);
                                    }
                                    else
                                    {
                                        throw new ApiException("No answear.", 200, responseContent, null, null);
                                    }
                                }
                                catch (JsonSerializationException ex)
                                {
                                    throw new ApiException("Deserializing error.", 200, responseContent, null, ex);
                                }
                            }
                            else
                            {
                                var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                                throw new ApiException("Unexpected status code.", (int)response_.StatusCode, responseData_, null, null);
                            }
                        }
                        finally
                        {
                            if (disposeResponse_)
                                response_.Dispose();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        throw new ApiException("Request send error", 0, null, null, ex);
                    }
                    catch (TaskCanceledException ex)
                    {
                        throw new ApiException("Connection timeout", 0, null, null, ex);
                    }
                    catch (Exception ex)
                    {
                        throw new ApiException("Unknown error", 0, null, null, ex);
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }
        public List<MathLog> Memory { get; set; } = new List<MathLog>();
        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = System.Text.Json.JsonSerializer.Deserialize<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (System.Text.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var typedBody = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerSettings, cancellationToken).ConfigureAwait(false);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (System.Text.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value is string[])
            {
                return string.Join(",", (string[])value);
            }
            else if (value.GetType().IsArray)
            {
                var valueArray = (System.Array)value;
                var valueTextArray = new string[valueArray.Length];
                for (var i = 0; i < valueArray.Length; i++)
                {
                    valueTextArray[i] = ConvertToString(valueArray.GetValue(i), cultureInfo);
                }
                return string.Join(",", valueTextArray);
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }

        public Task<Mass> CalculateWeight(Volume materialVolume, Materials material)
        {
            throw new NotImplementedException();
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
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