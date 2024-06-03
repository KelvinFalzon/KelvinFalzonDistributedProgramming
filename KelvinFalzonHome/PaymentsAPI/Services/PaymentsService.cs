using MongoDB.Driver;
using PaymentsAPI.Models;
using Microsoft.Extensions.Options;
using Amazon.Runtime.Internal;

namespace PaymentsAPI.Services
{
    public class PaymentsService
    {
        private readonly IMongoCollection<Payment> _paymentsCollection;

        public PaymentsService(IOptions<DatabaseSettings> databaseSettings)
        {

            var mongoClient = new MongoClient(
                "mongodb+srv://kelvinfalzonuser:123@kelvinfalzondp.slfg6q4.mongodb.net/?retryWrites=true&w=majority&appName=KelvinFalzonDP");

            var mongoDatabase = mongoClient.GetDatabase(
                "KelvinFalzonDP");

            _paymentsCollection = mongoDatabase.GetCollection<Payment>(
                "Payments");

        }

        public async Task<List<Payment>> GetAsync() =>
            await _paymentsCollection.Find(_ => true).ToListAsync();

        public async Task<Payment?> GetAsync(string paymentId) =>
            await _paymentsCollection.Find(x => x.Id == paymentId).FirstOrDefaultAsync();   

        public async Task CreateAsync(Payment newPayment) =>
            await _paymentsCollection.InsertOneAsync(newPayment);

        public async Task RemoveAsync(string paymentId) =>
            await _paymentsCollection.DeleteOneAsync(x => x.Id == paymentId);
    }
}
