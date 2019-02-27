using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ponder.Models;
using Ponder.Data;
using Microsoft.AspNetCore.Http;

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
            IEnumerable<Game> result;
            try
            {
                result = await _context.ReadAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new PonderResult(e.Message));
            }

            return Ok(result);
        }

        // GET api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            IEnumerable<Game> result;
            try
            {
                result = await _context.ReadAsync(g => g._id == id);
            }
            catch (Exception e)
            {
                return StatusCode(500, new PonderResult(e.Message));
            }

            return Ok(result);
        }

        // POST api/games
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game game)
        {
            try
            {
                await _context.CreateAsync(game);
            }
            catch (Exception e)
            {
                return StatusCode(500, new PonderResult(e.Message));
            }

            return Ok(game);
        }

        // PUT api/games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Game game)
        {
            try
            {
                await _context.UpdateAsync(g => g._id == id, game);
            }
            catch (Exception e)
            {
                return StatusCode(500, new PonderResult(e.Message));
            }

            return Ok(game);
        }

        // DELETE api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _context.DeleteAsync(g => g._id == id);
            }
            catch (Exception e)
            {
                return StatusCode(500, new PonderResult(e.Message));
            }

            return Ok(new PonderResult($"Successfully deleted game (id = {id})"));
        }
    }
}
