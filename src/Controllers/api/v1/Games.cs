using Microsoft.AspNetCore.Mvc;
using TourDeApp.Models;

namespace TourDeApp.Controllers.api.v1
{
    [ApiController]
    [Route("api/v1/games")]
    public class Games : Controller
    {
        [HttpPost]
        [ProducesResponseType(typeof(Game), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateGame([FromBody] GameCreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { code = 400, message = "Bad request: Invalid data" });

            // Logic to insert new game into database
            var game = new Game
            {
                Uuid = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Name = request.Name,
                Difficulty = request.Difficulty,
                GameState = "opening", // Default game state
                Board = request.Board
            };

            // Assume game is successfully created in DB
            return CreatedAtAction(nameof(GetGameById), new { uuid = game.Uuid }, game);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Game>), 200)]
        public async Task<IActionResult> GetAllGames()
        {
            // Logic to retrieve all games from the database
            var games = new List<Game>(); // TODO: Replace with actual data from DB

            return Ok(games);
        }

        [HttpGet("{uuid:guid}")]
        [ProducesResponseType(typeof(Game), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGameById(Guid uuid)
        {
            // Logic to retrieve game by UUID
            var game = new Game(); // TODO: Replace with actual data from DB
            if (game == null)
                return NotFound(new { code = 404, message = "Resource not found" });

            return Ok(game);
        }

        [HttpPut("{uuid:guid}")]
        [ProducesResponseType(typeof(Game), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdateGame(Guid uuid, [FromBody] GameCreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { code = 400, message = "Bad request: Invalid data" });

            // Logic to update game in the database
            var existingGame = new Game(); // TODO: Replace with actual data from DB
            if (existingGame == null)
                return NotFound(new { code = 404, message = "Resource not found" });

            // Update fields
            existingGame.Name = request.Name;
            existingGame.Difficulty = request.Difficulty;
            existingGame.Board = request.Board;
            existingGame.UpdatedAt = DateTime.UtcNow;

            return Ok(existingGame);
        }

        [HttpDelete("{uuid:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGame(Guid uuid)
        {
            // Logic to delete game by UUID
            var existingGame = new Game(); // TODO: Replace with actual data from DB
            if (existingGame == null)
                return NotFound(new { code = 404, message = "Resource not found" });

            // Assume successful deletion
            return NoContent();
        }
    }
}
