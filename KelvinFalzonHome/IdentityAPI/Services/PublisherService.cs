using Google.Cloud.PubSub.V1;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using IdentityAPI.Models;

namespace IdentityAPI.Services
{
    public class PublisherService
    {
        public TopicName topicName;
        public PublisherService(IOptions<GCPSettings> settings)
        {
            topicName = new TopicName(settings.Value.Project, settings.Value.Topic);
        }

        public async Task PublishOrderPayment(PubsubMessage message)
        {
            PublisherClient publisher = PublisherClient.Create(topicName);
            string messageId = await publisher.PublishAsync(message);

            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}
