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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto>(EndpointUrl);
            return response?.Value ?? new List<InventarioItemDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consumir la API: {Message}", ex.Message);
            throw;
        }
    }
}