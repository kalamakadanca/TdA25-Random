using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TourDeApp.Controllers.API_V1.Games
{
    [Route("/api/v1/games")]
    public class GamesController(DatabaseContext context, IMapper mapper) : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Create response with all games from db and return with status code 200.

            return StatusCode(200);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Models.GameCreateUpdateRequest game)
        {
            // Creates a game
            Models.DataBaseModels.GameDb gameDb = mapper.Map<Models.DataBaseModels.GameDb>(new Models.Game(game.Name, game.Difficulty)
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                GameState = new Models.Schemas.GameState(),
                Uuid = Guid.NewGuid().ToString(),
                BoardState = mapper.Map<Models.Schemas.BoardState>(game.BoardState)
            });

            return Ok(gameDb);
        }

        [HttpGet("{uuid}")]
        public IActionResult Get(string uuid)
        {
            // TODO: Returns a game with defined uuid or a 404 NotFound if it is not in DB.

            return StatusCode(200);
        }

        [HttpPut("{uuid}")]
        public IActionResult Put(string uuid)
        {
            // TODO: Updates a game

            return StatusCode(200);
        }

        [HttpDelete("{uuid}")]
        public IActionResult Delete(string uuid)
        {
            // TODO: Deletes a game

            return StatusCode(200);
        }
    }
}
