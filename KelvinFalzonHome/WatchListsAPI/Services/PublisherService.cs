using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using WatchListsAPI.Models;

namespace WatchListsAPI.Services
{
    public class PublisherService
    {
        public TopicName topicName;
        public PublisherService(IOptions<GCPSettings> settings)
        {
            topicName = new TopicName("kelvinfalzondistributed", "PubsubNotifications");
        }

        public async Task PublishNotification(PubsubMessage message)
        {
            PublisherClient publisher = PublisherClient.Create(topicName);
            await publisher.ShutdownAsync(TimeSpan.FromSeconds(10));
        }
    }
}
