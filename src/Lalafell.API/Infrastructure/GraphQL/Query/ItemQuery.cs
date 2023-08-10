using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina.Provider;

namespace Lalafell.API.Infrastructure.GraphQL.Query;

public class ItemQuery
{
    private readonly ItemProvider _itemProvider;

    public ItemQuery(ItemProvider item)
    {
        _itemProvider = item;
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<ItemDto> GetItems()
    {
        return _itemProvider.GetItems().Values;
    }
}