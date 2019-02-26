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
        public async Task<ActionResult> Get(string id)
        {
            return new JsonResult(await _context.ReadAsync(g => g._id == id));
        }

        // POST api/games
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game game)
        {
            try
            {
                await _context.CreateAsync(game);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        // PUT api/games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Game game)
        {
            try
            {
                await _context.UpdateAsync(g => g._id == id, game);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }

        // DELETE api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _context.DeleteAsync(g => g._id == id);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
            return new OkResult();
        }
    }
}
