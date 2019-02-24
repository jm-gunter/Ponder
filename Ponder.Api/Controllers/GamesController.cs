using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ponder.Models;
using Ponder.Data;

namespace Ponder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IDataContext<Game> _context;

        public GamesController(IDataContext<Game> context)
        {
            _context = context;
        }

        // GET api/games
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(await _context.ReadAsync());
        }

        // GET api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return new JsonResult(await _context.ReadAsync()); // TODO add id after MongoContext is complete
        }

        // POST api/games
        [HttpPost]
        public async Task Post([FromBody] Game game)
        {
            await _context.CreateAsync(game);
        }

        // PUT api/games/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/games/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
