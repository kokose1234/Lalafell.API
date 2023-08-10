using Lalafell.API.Data.Options;
using Lumina;
using Lumina.Data;
using Lumina.Data.Structs;
using Microsoft.Extensions.Options;

namespace Lalafell.API.Infrastructure.Lumina.Provider;

public class LuminaProvider
{
    public const string EXCEL_LOAD_ERROR = "Failed to load Excel sheet.";

    public GameData Data { get; }


    public LuminaProvider(IOptions<LuminaOption> option)
    {
        var config = new LuminaOptions
        {
            DefaultExcelLanguage = Language.Korean,
            CurrentPlatform = PlatformId.Win32,
            PanicOnSheetChecksumMismatch = false,
            ExcelSheetStrictCastingEnabled = true
        };
        Data = new GameData(option.Value.DataPath, config);
    }
}