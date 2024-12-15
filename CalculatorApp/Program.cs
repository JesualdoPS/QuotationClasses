using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net.Http;

namespace CalculatorApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddHttpClient("CalculatorApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7299");
            })
                .AddPolicyHandler(Policy.Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(3)));

            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("CalculatorApi");

            ApplicationConfiguration.Initialize();
            Application.Run(new CalculatorApp(httpClient));
        }
    }
}