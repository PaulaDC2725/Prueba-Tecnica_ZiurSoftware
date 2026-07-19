namespace ZiurTest.Blazor.Models;

public class ApiResponseDto
{
    public List<InventarioItemDto>? Value { get; set; }
    public int Count { get; set; }
}

public class InventarioItemDto
{
    public int Codigo { get; set; }
    public string? Descripcion { get; set; }
    public bool VActiva { get; set; }
}

