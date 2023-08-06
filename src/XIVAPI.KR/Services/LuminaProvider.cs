using Lumina;
using Lumina.Data;
using Lumina.Data.Structs;
using Lumina.Excel.GeneratedSheets;
using Microsoft.Extensions.Options;
using XIVAPI.KR.Data.Dto;
using XIVAPI.KR.Data.Options;

namespace XIVAPI.KR.Services;

public class LuminaProvider
{
    private const string ExcelLoadError = "Failed to load Excel sheet.";

    private readonly IReadOnlyDictionary<int, ItemDto> _items;
    private readonly IReadOnlyDictionary<int, ItemSearchDto> _searchItems;
    private readonly IReadOnlyList<ItemSearchCategoryDto> _itemSearchCategories;
    private readonly IReadOnlyList<ItemUICategoryDto> _itemUICategories;
    private readonly IReadOnlyList<ClassJobCategoryDto> _classJobCategories;

    public LuminaProvider(IOptions<LuminaOption> option)
    {
        var config = new LuminaOptions
        {
            DefaultExcelLanguage = Language.Korean,
            CurrentPlatform = PlatformId.Win32,
            PanicOnSheetChecksumMismatch = false,
            ExcelSheetStrictCastingEnabled = true
        };
        var data = new GameData(option.Value.DataPath, config);

        _itemSearchCategories = LoadItemSearchCategories(data);
        _itemUICategories = LoadItemUICategories(data);
        _classJobCategories = LoadClassJobCategories(data);
        _items = LoadItems(data);
        _searchItems = LoadSearchItems(data);
    }

    public IReadOnlyDictionary<int, ItemDto> GetItems() => _items;

    public IReadOnlyDictionary<int, ItemSearchDto> GetSearchItems() => _searchItems;

    public IReadOnlyList<ItemSearchCategoryDto> GetItemSearchCategories() => _itemSearchCategories;

    public IReadOnlyList<ItemUICategoryDto> GetItemUICategories() => _itemUICategories;

    public IReadOnlyList<ClassJobCategoryDto> GetClassJobCategories() => _classJobCategories;

    private IReadOnlyDictionary<int, ItemDto> LoadItems(GameData data)
    {
        var items = data.GetExcelSheet<Item>();

        if (items == null)
        {
            throw new InvalidOperationException(ExcelLoadError);
        }

        return items.Where(i => !string.IsNullOrEmpty(i.Name) && i.ItemUICategory.Value?.RowId >= 1)
                    .ToDictionary(i => Convert.ToInt32(i.RowId), i => new ItemDto
                    {
                        Id = Convert.ToInt32(i.RowId),
                        Name = i.Name,
                        Description = i.Description,
                        IconId = i.Icon,
                        ItemLevel = Convert.ToInt32(i.LevelItem.Row),
                        EquipLevel = Convert.ToInt32(i.LevelEquip),
                        Rarity = i.Rarity,
                        ItemKind = GetItemKindId(GetItemKind(Convert.ToInt32(i.ItemUICategory.Row))),
                        StackSize = Convert.ToInt32(i.StackSize),
                        CanBeHq = i.CanBeHq,
                        ItemSearchCategory = Convert.ToInt32(i.ItemSearchCategory.Row),
                        ItemUICategory = Convert.ToInt32(i.ItemUICategory.Row),
                        ClassJobCategory = Convert.ToInt32(i.ClassJobCategory.Row)
                    });
    }

    private IReadOnlyDictionary<int, ItemSearchDto> LoadSearchItems(GameData data)
    {
        var items = data.GetExcelSheet<Item>();

        if (items == null)
        {
            throw new InvalidOperationException(ExcelLoadError);
        }

        return items.Where(i => !string.IsNullOrEmpty(i.Name) && i.ItemSearchCategory.Value?.RowId >= 1)
                    .ToDictionary(i => Convert.ToInt32(i.RowId), i => new ItemSearchDto
                    {
                        Id = Convert.ToInt32(i.RowId),
                        Icon = $"/i/{(i.Icon / 1000) * 1000:D6}/{i.Icon:D6}.png",
                        Kind = new ItemSearchDto.ItemKind
                        {
                            Name = GetItemKind(Convert.ToInt32(i.ItemUICategory.Row))
                        },
                        Category = new ItemSearchDto.ItemSearchCategory
                        {
                            Id = Convert.ToInt32(i.ItemSearchCategory.Row),
                            Name = i.ItemSearchCategory.Value?.Name.ToString() ?? string.Empty
                        },
                        ItemLevel = Convert.ToInt32(i.LevelItem.Row),
                        Name = i.Name.ToString(),
                        Rarity = i.Rarity
                    });
    }

    private IReadOnlyList<ItemSearchCategoryDto> LoadItemSearchCategories(GameData data)
    {
        var itemSearchCategories = data.GetExcelSheet<ItemSearchCategory>();

        if (itemSearchCategories == null)
        {
            throw new InvalidOperationException(ExcelLoadError);
        }

        return itemSearchCategories.Select(i => new ItemSearchCategoryDto
        {
            Category = i.Category,
            Id = Convert.ToInt32(i.RowId),
            Name = i.Name,
            Order = i.Order
        }).ToList();
    }

    private IReadOnlyList<ItemUICategoryDto> LoadItemUICategories(GameData data)
    {
        var itemUICategories = data.GetExcelSheet<ItemUICategory>();

        if (itemUICategories == null)
        {
            throw new InvalidOperationException(ExcelLoadError);
        }

        return itemUICategories.Select(i => new ItemUICategoryDto
        {
            Id = Convert.ToInt32(i.RowId),
            Name = i.Name.ToString()
        }).ToList();
    }

    private IReadOnlyList<ClassJobCategoryDto> LoadClassJobCategories(GameData data)
    {
        var classJobCategories = data.GetExcelSheet<ClassJobCategory>();

        if (classJobCategories == null)
        {
            throw new InvalidOperationException(ExcelLoadError);
        }

        return classJobCategories.Select(i => new ClassJobCategoryDto
        {
            Id = Convert.ToInt32(i.RowId),
            Name = i.Name.ToString()
        }).ToList();
    }

    private static string GetItemKind(int uiCategory)
    {
        switch (uiCategory)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 84:
            case 87:
            case 88:
            case 89:
            case 96:
            case 97:
            case 98:
                return "무기";
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
            case 99:
                return "도구";
            case 11:
            case 34:
            case 35:
            case 36:
            case 37:
            case 38:
            case 39:
                return "방어구";
            case 40:
            case 41:
            case 42:
            case 43:
                return "장신구";
            case 44:
            case 45:
            case 46:
            case 47:
                return "약품 및 식품";
            case 48:
            case 49:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
                return "재료";
            default:
                return "기타";
        }
    }

    private int GetItemKindId(string kind)
    {
        return kind switch
        {
            "무기" => 1,
            "도구" => 2,
            "방어구" => 3,
            "장신구" => 4,
            "약품 및 식품" => 5,
            "재료" => 6,
            _ => 7
        };
    }
}