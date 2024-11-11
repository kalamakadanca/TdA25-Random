using Microsoft.AspNetCore.Mvc;

namespace TourDeApp.Controllers.API_V1.Games
{
    [Route("/api/v1/games")]
    public class GamesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Create response with all games from db and return with status code 200.

            return StatusCode(200);
        }

        [HttpPost]
        public IActionResult Post() 
        {
            // TODO: 

            return StatusCode(200);
        }

        [HttpGet]
        public IActionResult Get(string uuid)
        {
            // TODO: Returns a game with defined uuid or a 404 NotFound if it is not in DB.

            return StatusCode(200);
        }

        [HttpPut]
        public IActionResult Put(string uuid)
        {
            // TODO: Updates a game

            return StatusCode(200);
        }

        [HttpDelete]
        public IActionResult Delete(string uuid)
        {
            // TODO: Deletes a game

            return StatusCode(200);
        }
    }
}
