using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YourAppNamespace.Pages
{
    public class GameModel : PageModel
    {
        public string DeckId { get; set; }

        public void OnGet(string deckId)
        {
            DeckId = deckId;
            // Additional logic can be added here if needed
        }
    }
}
