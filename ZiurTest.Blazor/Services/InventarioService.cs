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

            var rawJson = await response.Content.ReadAsStringAsync();
            
            // Consologuear el JSON recibido en consola
            _logger.LogInformation("JSON recibido de la API: {RawJson}", rawJson);
            Console.WriteLine($"[JSON API REST]: {rawJson}");

            using var doc = JsonDocument.Parse(rawJson);
            var jsonElement = doc.RootElement;
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<InventarioItemDto> result = new();

            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                result = jsonElement.Deserialize<List<InventarioItemDto>>(options) ?? new List<InventarioItemDto>();
            }
            else if (jsonElement.ValueKind == JsonValueKind.Object && jsonElement.TryGetProperty("Value", out var valueProperty))
            {
                result = valueProperty.Deserialize<List<InventarioItemDto>>(options) ?? new List<InventarioItemDto>();
            }

            _logger.LogInformation("Total de elementos deserializados: {Count}", result.Count);
            Console.WriteLine($"[InventarioService] Elementos procesados: {result.Count}");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consumir la API de inventario: {Message}", ex.Message);
            throw;
        }
    }
}