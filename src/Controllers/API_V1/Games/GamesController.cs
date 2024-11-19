using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TourDeApp.Models.Schemas;

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
        public async Task<IActionResult> Post([FromBody] Models.GameCreateUpdateRequest requestGame)
        {
            if (!ModelState.IsValid || !requestGame.BindDifficultyType())
            {
                return new ObjectResult(new Error
                {
                    Code = 400,
                    Message = $"Bad request: { ModelState.Values.First().Errors.First().ErrorMessage }"
                })
                {
                    StatusCode = 400
                };
            }

            string? error = requestGame.BoardState.IsBoardValid();
            if (error != null)
            {
                return new ObjectResult(new Error
                {
                    Code = 422,
                    Message = $"Unprocessable Entity: {error}"
                })
                {
                    StatusCode = 422
                };
            }
            
            var game = new Models.Game(requestGame.Name, requestGame.EnumDifficulty)
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                GameState = new GameState(),
                Uuid = Guid.NewGuid().ToString(),
                BoardState = mapper.Map<BoardState>(requestGame.BoardState)
            };
            
            // Creates a game
            Models.DataBaseModels.GameDb gameDb = mapper.Map<Models.DataBaseModels.GameDb>(game);
            // Inserting the game in the DB
            await context.Games.AddAsync(gameDb);
            
            return new ObjectResult(gameDb) { StatusCode = 201 };
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
