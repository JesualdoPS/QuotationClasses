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
            services.AddHttpClient("Calculator", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7299");
                client.Timeout = TimeSpan.FromSeconds(0.5);
            })
                .AddPolicyHandler(Policy.Handle<TaskCanceledException>()
                    .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .WaitAndRetryAsync(10, retryAttempt => 
                        { 
                            return TimeSpan.FromSeconds(3); 
                        }
                        ));
               

            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("Calculator");

            ApplicationConfiguration.Initialize();
            Application.Run(new CalculatorApp(httpClient));
        }
    }
}