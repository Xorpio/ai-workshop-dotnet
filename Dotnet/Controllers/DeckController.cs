using Microsoft.AspNetCore.Mvc;
using BoilerplateWebApp.Services;

namespace BoilerplateWebApp.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class DeckController : ControllerBase
    {
        private readonly DeckOfCardsService _deckOfCardsService;

        public DeckController(DeckOfCardsService deckOfCardsService)
        {
            _deckOfCardsService = deckOfCardsService;
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> StartNewGame()
        {
            var deckId = await _deckOfCardsService.NewGame();
            if (string.IsNullOrEmpty(deckId))
            {
                return BadRequest("Unable to start a new game. Please try again later.");
            }

            return Ok(new { deck_id = deckId });
        }

        [HttpGet("{deckId}/draw")]
        public async Task<IActionResult> DrawCards(string deckId)
        {
            var cards = await _deckOfCardsService.Draw(deckId);

            if (cards == null || cards.Count == 0)
            {
                return BadRequest(new { message = "Failed to draw cards or no cards available." });
            }

            return Ok(new { cards });
        }
    }
}
