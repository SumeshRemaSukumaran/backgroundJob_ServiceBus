using Azure.Messaging.ServiceBus;
using Sorting.Contracts.Queue;
using Sorting.Model.ViewModel;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sorting.ServiceBusQueue
{
    public class AzureServiceBusQueue : IJobQueue
    {
        string _connectionString = "Endpoint=sb://my-messages.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tz2l40ognbz1REakUIuiZczPN60dTq7tMK1xZ9h/OJM=";
        string _azureQueueName = "myqueue";
        ITopicClient _topicClient;
        public AzureServiceBusQueue(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }
        public Task Dequeue()
        {
            throw new NotImplementedException();
        }

        public Task EnqueueJob<T>(T obj)
        {
            var body = JsonSerializer.Serialize(obj);
            var message = new Message(Encoding.UTF8.GetBytes(body));
            message.UserProperties["messageType"] = typeof(T).Name;
            return _topicClient.SendAsync(message);
        }
    }
}
