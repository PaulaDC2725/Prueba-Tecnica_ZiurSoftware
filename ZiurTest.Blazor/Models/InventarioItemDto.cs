using System.Text.Json.Serialization;

namespace ZiurTest.Blazor.Models;

public class InventarioItemDto
{
    [JsonPropertyName("Codigo")]
    public int Codigo { get; set; }

    [JsonPropertyName("Descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("VActiva")]
    public bool VActiva { get; set; }
}

