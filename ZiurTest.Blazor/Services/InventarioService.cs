using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ZiurTest.Blazor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ZiurTest.Blazor.Services;

public class InventarioService : IInventarioService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventarioService> _logger;
    private readonly string _endpointUrl;
    private readonly string _token;

    private const string DefaultEndpointUrl = "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos";
    private const string DefaultToken = "ae8bad44-7348-11ee-b962-0242ac120002";

    public InventarioService(HttpClient httpClient, ILogger<InventarioService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _endpointUrl = configuration["InventarioApi:EndpointUrl"] ?? DefaultEndpointUrl;
        _token = configuration["InventarioApi:BearerToken"] ?? DefaultToken;
    }

    public async Task<List<InventarioItemDto>> GetItemsAsync()
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, _endpointUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var rawJson = await response.Content.ReadAsStringAsync();
            
            // Consologuear el JSON recibido en consola
            _logger.LogInformation("JSON received from API: {RawJson}", rawJson);
            
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

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consumir la API de inventario: {Message}", ex.Message);
            throw;
        }
    }
}