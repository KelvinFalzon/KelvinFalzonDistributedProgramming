using MongoDB.Driver;
using OrdersAPI.Models;
using Microsoft.Extensions.Options;
using Amazon.Runtime.Internal;

namespace OrdersAPI.Services
{
    public class OrdersService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrdersService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
               "mongodb+srv://kelvinfalzonuser:123@kelvinfalzondp.slfg6q4.mongodb.net/?retryWrites=true&w=majority&appName=KelvinFalzonDP");

            var mongoDatabase = mongoClient.GetDatabase(
                "KelvinFalzonDP");

            _ordersCollection = mongoDatabase.GetCollection<Order>(
                "Orders");
        }

        public async Task<List<Order>> GetAsync() =>
            await _ordersCollection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetAsync(string userId) =>
            await _ordersCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        public async Task<List<Order>> GetList(string userId) =>
            await _ordersCollection.Find(x => x.UserId == userId).ToListAsync();

        public async Task CreateAsync(Order newOrder) =>
            await _ordersCollection.InsertOneAsync(newOrder);

        public async Task RemoveAsync(string orderId) =>
            await _ordersCollection.DeleteOneAsync(x => x.Id == orderId);
    }
}
