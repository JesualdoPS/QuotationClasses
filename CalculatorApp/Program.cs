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
            .AddPolicyHandler((IAsyncPolicy<HttpResponseMessage>)Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("CalculatorApi");

            ApplicationConfiguration.Initialize();
            Application.Run(new Calculator(httpClient));
        }
    }
}