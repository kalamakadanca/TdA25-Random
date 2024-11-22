using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Controllers.API_V1.Games
{
    [Route("/api/v1/games")]
    public class GamesController(DatabaseContext context, IMapper mapper) : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var games = context.Games.ToArray();

            return new ObjectResult(games)
            {
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.GameCreateUpdateRequest requestGame)
        {
            if (!ModelState.IsValid)
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
            if (error != null || !requestGame.BindDifficultyType())
            {
                return new ObjectResult(new Error
                {
                    Code = 422,
                    Message = $"Semantic error: {error ?? "Difficulty field is not valid"}"
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
                BoardState = requestGame.Board
            };
            
            // Creates a game
            Models.DataBaseModels.GameDb gameDb = mapper.Map<Models.DataBaseModels.GameDb>(game);
            // Inserting the game in the DB
            await context.Games.AddAsync(gameDb);
            await context.SaveChangesAsync();
            
            return new ObjectResult(game) { StatusCode = 201 };
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> Get(string uuid)
        {
            // TODO: Returns a game with defined uuid or a 404 NotFound if it is not in DB.

            if (string.IsNullOrEmpty(uuid))
            {
                return new ObjectResult(new Error
                {
                    Code = 400,
                    Message = $"Bad request: Cannot to find UUID."
                })
                {
                    StatusCode = 400
                };
            }

            if (!Guid.TryParse(uuid, null, out Guid result))
            {
                return new ObjectResult(new Error
                {
                    Code = 422,
                    Message = $"Semantic error: UUID is invalid"
                })
                {
                    StatusCode = 422
                };
            }

            var foundGame = await context.Games.FirstOrDefaultAsync(game => game.Uuid == uuid);

            if (foundGame is null)
            {
                return new ObjectResult(new Error
                {
                    Code = 404,
                    Message = $"Resource not found"
                })
                {
                    StatusCode = 404
                };
            }

            return new ObjectResult(foundGame)
            {
                StatusCode = 200
            };
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
