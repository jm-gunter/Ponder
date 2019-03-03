using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Ponder.Api;
using Ponder.Models;

namespace Ponder.Pages
{
    public class GameDetailModel : PageModel
    {
        private IConfiguration _config;
        public Game Game {get; private set;}

        public GameDetailModel(IConfiguration config)
        {
            _config = config;
        }

        public async Task OnGet(string id)
        {
            var client = new HttpClient();
            var uri = new UriBuilder()
            {
                Scheme = _config.GetSection("Api")["Scheme"],
                Host = _config.GetSection("Api")["Host"],
                Port = Convert.ToInt32(_config.GetSection("Api")["Port"]),
                Path = $"{Endpoints.Games}/{id}"
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
            var result = await client.SendAsync(request);
            Game = (await result.Content.ReadAsAsync<IEnumerable<Game>>()).FirstOrDefault();
        }
    }
}
