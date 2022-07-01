using Maersk.Sorting.Contracts.BusinessService.SortJob;
using Maersk.Sorting.Contracts.DataService.Entities;
using Maersk.Sorting.Model.ViewModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maersk.Sorting.ServiceBusQueue
{
    public class SubscriberBackgroundJob : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;

        private readonly ISortJobProcessorService _sortJobProcessorService;
        private readonly IJobs _jobs;
        public SubscriberBackgroundJob(ISubscriptionClient subscriptionClient,
            ISortJobProcessorService sortJobProcessorService,
            IJobs jobs)
        {
            _subscriptionClient = subscriptionClient;
            _sortJobProcessorService = sortJobProcessorService;
            _jobs = jobs;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler(async (message, token) =>
            {
                var sortJob = JsonConvert.DeserializeObject<SortJobModel>(Encoding.UTF8.GetString(message.Body));
                sortJob.Status = Model.Enum.SortJobStatus.Completed;
                sortJob = await _sortJobProcessorService.Process(sortJob);               
                _jobs.Update(sortJob.Id, sortJob);
               // return  Task.CompletedTask;
            }, new MessageHandlerOptions(args => Task.CompletedTask));
            return Task.CompletedTask;
        }
    }
}
