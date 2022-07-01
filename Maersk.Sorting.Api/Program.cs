using Maersk.Sorting.Contracts.Queue;
using Maersk.Sorting.QueueService;
using Maersk.Sorting.ServiceBusQueue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Maersk.Sorting.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using IHost host = (IHost)Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {       
        services.AddSingleton<JobQueue>();
        services.AddSingleton<BackgroundJob>();
        services.AddHostedService<QueuedHostedService>();
        services.AddHostedService<SubscriberBackgroundJob>();
        services.AddSingleton<IBackgroundTaskQueue>(_ =>
        {
            if (!int.TryParse(context.Configuration["QueueCapacity"], out var queueCapacity))
            {
                queueCapacity = 50;
            }
            return new DefaultBackgroundTaskQueue(queueCapacity);
        });

        
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build();

            BackgroundJob backgroundJob = host.Services.GetRequiredService<BackgroundJob>()!;
            backgroundJob.StartBackgroundWorkAsync();
            host.Run();
        }
    }
}
