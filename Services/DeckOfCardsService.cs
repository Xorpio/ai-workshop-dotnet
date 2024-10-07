using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoilerplateWebApp.Services
{
    public class DeckOfCardsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DeckOfCardsService> _logger;

        // Constructor to inject HttpClient and Logger
        public DeckOfCardsService(HttpClient httpClient, ILogger<DeckOfCardsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Method to start a new game and return the deck_id
        public async Task<string> NewGame()
        {
            const string apiUrl = "https://www.deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";
            try
            {
                // Make the GET request to the external API
                var response = await _httpClient.GetAsync(apiUrl);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the content as a string
                    var content = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation("Response: {Content}", content);

                    // Parse the JSON response to get the deck_id
                    var jsonResponse = JsonSerializer.Deserialize<DeckOfCardsResponse>(content);

                    if (jsonResponse != null && jsonResponse.Success)
                    {
                        // Return the deck_id if found
                        return jsonResponse.DeckId;
                    }
                }

                // Log a warning if the response is unsuccessful or invalid
                _logger.LogWarning("Failed to retrieve deck_id. Status Code: {StatusCode}", response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception if there's an error making the request
                _logger.LogError(ex, "Error occurred while calling the Deck of Cards API");
            }
            catch (JsonException ex)
            {
                // Log the exception if there's an error parsing the response
                _logger.LogError(ex, "Error occurred while parsing the Deck of Cards API response");
            }

            // If something goes wrong, return an empty string
            return string.Empty;
        }

        public async Task<List<Card>> Draw(string deckId)
        {
            var url = $"https://www.deckofcardsapi.com/api/deck/{deckId}/draw/?count=2";
            var cards = new List<Card>();

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"API response: {content}");

                    // Deserialize the response JSON to extract card details
                    var apiResponse = JsonSerializer.Deserialize<DeckApiResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // If the response is successful and has cards, return them
                    if (apiResponse?.Cards != null)
                    {
                        cards = apiResponse.Cards;
                    }
                }
                else
                {
                    _logger.LogError($"Failed to draw cards from deck {deckId}. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while drawing cards: {ex.Message}");
            }

            return cards;
        }
    }

    // Class to represent the JSON response from the API
    public class DeckOfCardsResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("deck_id")]
        public string DeckId { get; set; }

        [JsonPropertyName("shuffled")]
        public bool Shuffled { get; set; }

        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }
    }

    public class Card
    {
        public string Code { get; set; }
        public string Image { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
    }
    public class DeckApiResponse
    {
        public List<Card> Cards { get; set; }
    }

}
