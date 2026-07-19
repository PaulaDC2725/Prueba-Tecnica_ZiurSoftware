using ZiurTest.Blazor.Models;

namespace ZiurTest.Blazor.Services;

public interface IInventarioService
{
    Task<List<InventarioItemDto>> GetItemsAsync();
}