using MongoDB.Driver;
using WatchListsAPI.Models;
using Microsoft.Extensions.Options;
using Amazon.Runtime.Internal;

namespace WatchListsAPI.Services
{
    public class WatchlistsService
    {
        private readonly IMongoCollection<Watchlist> _watchlistsCollectionName;

        public WatchlistsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                "mongodb+srv://kelvinfalzonuser:123@kelvinfalzondp.slfg6q4.mongodb.net/?retryWrites=true&w=majority&appName=KelvinFalzonDP");

            var mongoDatabase = mongoClient.GetDatabase(
                "KelvinFalzonDP");

            _watchlistsCollectionName = mongoDatabase.GetCollection<Watchlist>(
                "Watchlists");
        }

        public async Task<List<Watchlist>> GetAsync() =>
            await _watchlistsCollectionName.Find(_ => true).ToListAsync();

        public async Task<List<Watchlist>?> GetAsync(string userId) =>
            await _watchlistsCollectionName.Find(x => x.UserId == userId).ToListAsync();

        public async Task<Watchlist> GetAsync(string userId, string movieId) =>
            await _watchlistsCollectionName.Find(x => x.UserId == userId && x.MovieId == movieId).FirstAsync();

        public async Task CreateAsync(Watchlist newWatchlist) =>
            await _watchlistsCollectionName.InsertOneAsync(newWatchlist);

        public async Task RemoveAsync(string watchlistId) =>
            await _watchlistsCollectionName.DeleteOneAsync(x => x.Id == watchlistId);
    }
}
