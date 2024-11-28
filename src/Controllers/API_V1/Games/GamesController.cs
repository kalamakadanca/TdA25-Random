using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourDeApp.Models;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Controllers.API_V1.Games
{
    [Route("/api/v1/games")]
    public class GamesController(DatabaseContext context, IMapper mapper) : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var games = context.Games.ToArray()
                .Select(mapper.Map<Game>)
                .ToList();

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
                Board = requestGame.Board
            };
            
            Console.WriteLine(game.Uuid); // Abych věděl UUID
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

            return new ObjectResult(mapper.Map<Game>(foundGame))
            {
                StatusCode = 200
            };
        }

        [HttpPut("{uuid}")]
        public async Task<IActionResult> Put(string uuid, [FromBody] Models.GameCreateUpdateRequest requestGame)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new Error
                {
                    Code = 400,
                    Message = $"Bad request: {ModelState.Values.First().Errors.First().ErrorMessage}"
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

            foundGame.Board = requestGame.Board;
            foundGame.Difficulty = requestGame.EnumDifficulty;
            foundGame.Name = requestGame.Name;

            await context.SaveChangesAsync();

            return new ObjectResult(mapper.Map<Game>(foundGame))
            {
                StatusCode = 200
            };
        }

        [HttpDelete("{uuid}")]
        public async Task<IActionResult> Delete(string uuid)
        {
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

            context.Games.Remove(foundGame);
            await context.SaveChangesAsync();
            
            return StatusCode(204);
        }
    }
}
