using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Maersk.Sorting.BusinessService;
using Maersk.Sorting.Contracts.BusinessService.SortJob;
using Maersk.Sorting.Contracts.DataService.Entities;
using Maersk.Sorting.DataService.Entities;
using Maersk.Sorting.Contracts.DataService;
using Maersk.Sorting.DataService.SortJob;
using Maersk.Sorting.BusinessService.Mapper;
using Microsoft.OpenApi.Models;
using Maersk.Sorting.QueueService;
using Maersk.Sorting.Contracts.Queue;
using Maersk.Sorting.ServiceBusQueue;
using Microsoft.Extensions.Azure;
using Microsoft.Azure.ServiceBus;

namespace Maersk.Sorting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMappertProfile).Assembly);
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Maersk Sort Job API",
                    Version = "v1",
                    Description = "",
                });
            });

            string connectionString = Configuration.GetValue<string>("ServiceBus:ConnectionString");
            string queueName = Configuration.GetValue<string>("ServiceBus:QueueName");
            string topicName = Configuration.GetValue<string>("ServiceBus:TopicName");
            string subscriptionName = Configuration.GetValue<string>("ServiceBus:SubscriptionName"); 
            services.AddSingleton<ITopicClient> ( s =>             
                new TopicClient (connectionString,topicName)           
            );

           
            services.AddTransient<ISortJobProcessorService, SortJobProcessorService>();
            services.AddTransient<ISortJobProcessorRepository, SortJobProcessorRepository>();
            services.AddSingleton<IJobs, Jobs>();
            services.AddTransient<IJobQueue, AzureServiceBusQueue>();
            services.AddSingleton<ISubscriptionClient>(s =>            
                new SubscriptionClient(connectionString, topicName, subscriptionName)
            );
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Maersk Sort Job API");
                s.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
