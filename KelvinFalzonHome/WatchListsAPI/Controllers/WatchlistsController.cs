using WatchListsAPI.Models;
using WatchListsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace PaymentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistsController : ControllerBase
    {
        private readonly WatchlistsService _watchlistsService;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IEncryptor _encryptor;

        public WatchlistsController(WatchlistsService watchlistsService, IJwtBuilder jwtBuilder, IEncryptor encryptor)
        {
            _watchlistsService = watchlistsService;
            _jwtBuilder = jwtBuilder;
            _encryptor = encryptor;
        }

        [HttpPost("newWatchlist")]
        public async Task<ActionResult> NewWatchList([FromBody] Watchlist watchlists)
        {

            var watchlist = new Watchlist
            {
                UserId = watchlists.UserId,
                MovieId = watchlists.MovieId
            };

            await _watchlistsService.CreateAsync(watchlist);

            return Ok();
        }

        [HttpGet("getWatchlist")]
        public async Task<ActionResult> GetWatchlistAsync([FromQuery(Name = "userId")] string userId)
        {
            var watchlist = await _watchlistsService.GetAsync(userId);
            return Ok(watchlist);
        }

        [HttpGet("removeWatchlist")]
        public async Task<ActionResult> RemoveFromWatchlist([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "movieId")] string movieId)
        {

            return null;
        }
    }
}
