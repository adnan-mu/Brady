using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BradyCodeChallenge.Model;
using BradyCodeChallenge.Service;

// Setup Host
var host = CreateDefaultBuilder().Build();

// Invoke Worker
using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;
var dataProcessor = provider.GetRequiredService<DataProcessor>();
dataProcessor.ProcessData();

host.Run();


static IHostBuilder CreateDefaultBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices(services =>
        {
            services.AddSingleton<DataProcessor>();
            services.AddTransient<IDataReader, DataReader>();
            services.AddTransient<IDataWriter, DataWriter>();
            services.AddTransient<ICalculatorService, CalculatorService>();
        });
}
