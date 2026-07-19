using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ZiurTest.Blazor.Models;
using Microsoft.Extensions.Logging;

namespace ZiurTest.Blazor.Services;

public class InventarioService : IInventarioService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventarioService> _logger;

    private const string Token = "ae8bad44-7348-11ee-b962-0242ac120002";
    private const string EndpointUrl = "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos";

    public InventarioService(HttpClient httpClient, ILogger<InventarioService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<InventarioItemDto>> GetItemsAsync()
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, EndpointUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonElement = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return jsonElement.Deserialize<List<InventarioItemDto>>(options) ?? new List<InventarioItemDto>();
            }
            else if (jsonElement.ValueKind == JsonValueKind.Object && jsonElement.TryGetProperty("Value", out var valueProperty))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return valueProperty.Deserialize<List<InventarioItemDto>>(options) ?? new List<InventarioItemDto>();
            }

            return new List<InventarioItemDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consumir la API de inventario: {Message}", ex.Message);
            throw;
        }
    }
}