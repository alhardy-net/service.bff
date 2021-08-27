using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace Bff.Api
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(new LokiJsonTextFormatter())
                .CreateBootstrapLogger();
            
            Log.Information("Starting up");
            
            try
            {
                var host = CreateHostBuilder(args).Build();

                await host.RunAsync();

                Log.Information("Stopped");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}