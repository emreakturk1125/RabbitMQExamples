using _6RabbitMQ.FileCreate.WorkerService.Models;
using _6RabbitMQ.FileCreate.WorkerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _6RabbitMQ.FileCreate.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddDbContext<AdventureWorks2019Context>(opt =>
                    {
                        opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    });

                    services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

                    services.AddSingleton<RabbitMQClientService>();

                    services.AddHostedService<Worker>();
                });
    }
}
