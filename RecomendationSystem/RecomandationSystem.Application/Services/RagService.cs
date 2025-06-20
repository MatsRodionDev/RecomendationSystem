using RecomandationSystem.Application.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using RecomandationSystem.Application.Interfaces;
using Microsoft.Extensions.AI;

namespace RecomandationSystem.Application.Services
{
    internal sealed class RagService() : IRagService
    {
        private const string API_KEY = "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";
        private const string ENDPOINT = "https://api.groq.com/openai/v1/chat/completions";

        public async Task<string> GetQueryAsync(string query, CancellationToken cancellationToken)
        {
            //var response = await chatClient.GetResponseAsync(
            //    new ChatMessage(ChatRole.User, query),
            //    cancellationToken: cancellationToken);

            //return response.Text;

            return query;
        }

        public async Task<List<ProductDto>> FilterProductsAsync(string query, List<ProductDto> products, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            var productsList = string.Join("\n", products.Select((p, i) =>
                $"Product name: {p.Name}. Product Description {p.Description}. Brand: {p.Brand}. Product category: {p.Category} (Id: {p.Id})"));

            var sysMessage = """
                    You are a strict product search engine. Your task is to analyze a user's query and return ONLY product IDs that satisfy ALL conditions precisely.

                    ### Core Principles:
                    1. **Absolute Match**:
                       - Query: "iPhone 15 Pro" → Only matches "iPhone 15 Pro", not "iPhone 15" or "iPhone 14 Pro".
                       - "Nike shoes size 10" → Must include Nike + shoes + size 10 (no Adidas, no size 9).

                    2. **Attribute Strictness**:
                       - Color: "Red" ≠ "Crimson" unless explicitly defined as synonyms.
                       - Brand: "Apple" ≠ "Samsung" even if similar products exist.

                    3. **Terminology Control**:
                       - Allowed synonyms: "smartphone" → ["iPhone", "Galaxy"], "sneakers" → ["running shoes"] (only pre-approved mappings).
                       - Forbidden interpretations: "apple" → "Apple Inc.", "running" → "jogging".

                    4. **Zero Tolerance**:
                       - If any attribute is missing/not matched → Exclude the product.
                       - Empty array [] is preferred over partial matches.

                    ### Response Format (JSON):
                    {
                      "ids": ["id1", "id2"],
                      "reasoning": "Brief explanation (for debugging)",
                      "strict_match": true
                    }
                    """;

            var ragPrompt = $"""
                    Analyze the query and return ONLY product IDs that EXACTLY match ALL specified attributes (brand, type, size, color, etc.). 
                    Follow STRICTLY:
                    1. Match ALL query terms - e.g., "Nike running size 10" → must include Nike + running + size 10.
                    2. No partial matches - "running shoes" ≠ "trail running".
                    3. Synonyms ONLY for obvious cases (e.g., "cellphone" → "iPhone").
                    4. Return [] if no perfect match exists.

                    Products:
                    {productsList}

                    Query: {query}

                    Output (ONLY IDs as JSON array): 
                    """;

            var requestBody = new
            {
                model = "llama3-8b-8192",
                messages = new[]
                {
                    new { role = "system", content = sysMessage },
                    new { role = "user", content = ragPrompt }
                },
                max_tokens = 256,
                response_format = new { type = "json_object" } 
            };

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ENDPOINT, content, cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseString);
            var obj = jsonResponse
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content");

            using var doc = JsonDocument.Parse(obj.GetString()!);
            var answer = doc.RootElement
                .GetProperty("ids")
                .EnumerateArray()
                .Select(id => id.GetString())
                .ToArray();
            var ids = answer
                .Select(e => Guid.TryParse(e, out var g) ? g : (Guid?)null)
                .Where(g => g.HasValue)
                .Select(g => g!.Value)
                .ToList();

            return products.Where(p => ids.Contains(p.Id)).ToList();
        
        }
    }
}
