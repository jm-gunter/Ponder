using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Ponder.Models;
using Ponder.Api;
using Newtonsoft.Json;

namespace Ponder.Pages
{
    public class GamesModel : PageModel
    {
        private IConfiguration _config;
        public IEnumerable<Game> Games { get; private set; }

        public GamesModel(IConfiguration config)
        {
            _config = config;
        }
                
        public async Task OnGet()
        {
            var client = new HttpClient();
            var uri = new UriBuilder()
            {
                Scheme = _config.GetSection("Api")["Scheme"],
                Host = _config.GetSection("Api")["Host"],
                Port = Convert.ToInt32(_config.GetSection("Api")["Port"]),
                Path = Endpoints.Games
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
            var result = await client.SendAsync(request);
            Games = (await result.Content.ReadAsAsync<IEnumerable<Game>>()).OrderByDescending(g => g.Date);
        }
    }
}
