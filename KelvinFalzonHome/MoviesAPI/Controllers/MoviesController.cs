using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        public MoviesController()
        {

        }

        [HttpGet("byId")]
        public async Task<ActionResult> byId([FromQuery(Name ="MovieId")] string id)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com/");
            var request = new RestRequest($"/titles/{id}?info=base_info");

            request.AddHeader("X-RapidAPI-Key", "6986a79c14msh5c34266c8b784afp1c58e4jsn75fc2c15011c");
            request.AddHeader("X-RapidAPI-Host", "moviesdatabase.p.rapidapi.com");
            var response = await client.GetAsync(request); if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JObject>(response.Content);
                string Title = content["results"]["titleText"]["text"].Value<string>();
                string Type = content["results"]["titleType"]["text"].Value<string>();
                string Genre = content["results"]["genres"]["genres"][0]["text"].Value<string>();
                var movie = new Movie
                {
                    Id = id,
                    Title = Title,
                    Type = Type,
                    Genre = Genre
                };
                return Ok(movie);
            }
            else
            {
                return null;
            }
        }

        [HttpGet("byGenre")]
        public async Task<ActionResult<List<Movie>>> byGenre(string genre)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com/");
            var request = new RestRequest($"/titles?genre={genre}");
            request.AddHeader("X-RapidAPI-Key", "6986a79c14msh5c34266c8b784afp1c58e4jsn75fc2c15011c");
            request.AddHeader("X-RapidAPI-Host", "moviesdatabase.p.rapidapi.com");

            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JObject>(response.Content);
                var results = content["results"].Children<JObject>();
                List<Movie> movieList = new List<Movie>();

                foreach (var item in results)
                {
                    var movie = new Movie
                    {
                        Id = item["id"].Value<string>(),
                        Title = item["originalTitleText"]["text"].Value<string>(),
                        Type = item["titleType"]["text"].Value<string>()
                    };
                    movieList.Add(movie); 
                }

                return Ok(movieList);
            }
            else
            {
                return StatusCode((int)response.StatusCode); 
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> getbyTypeGenre([FromQuery(Name = "type")] string type, [FromQuery(Name = "genre")] string genre)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com/");
            var request = new RestRequest($"/titles?genre={genre}&titleType={type}");
            request.AddHeader("X-RapidAPI-Key", "6986a79c14msh5c34266c8b784afp1c58e4jsn75fc2c15011c");
            request.AddHeader("X-RapidAPI-Host", "moviesdatabase.p.rapidapi.com");

            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JObject>(response.Content);
                var results = content["results"].Children<JObject>();
                List<Movie> movieList = new List<Movie>();

                foreach (var item in results)
                {
                    var movie = new Movie
                    {
                        Id = item["id"].Value<string>(),
                        Title = item["originalTitleText"]["text"].Value<string>(),
                        Type = item["titleType"]["text"].Value<string>()
                    };
                    movieList.Add(movie);
                }

                return Ok(movieList);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }

}
