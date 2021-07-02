using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RuilwinkelVaals.WebApp.Data;
using System;
using System.Linq;

namespace RuilwinkelVaals.WebApp
{
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    DbConstructor dbInit = scope.ServiceProvider.GetRequiredService<DbConstructor>();
                    dbInit.Init().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                .OutputMetrics.AsPrometheusProtobuf()
                .Build();

            return Host.CreateDefaultBuilder(args)
                    .ConfigureMetrics(Metrics)
                    .UseMetrics(
                        options =>
                        {
                            options.EndpointOptions = endpointsOptions =>
                            {
                                endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusProtobufOutputFormatter>().First();
                            };
                        })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddScoped<DbConstructor>();
                    });
        }
    }
}
