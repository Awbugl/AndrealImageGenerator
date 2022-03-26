using System.Text;

namespace ImageGenerator;

[Serializable]
internal class Path
{
    private static string _arcaeaBackgroundRoot = AppContext.BaseDirectory + "/Andreal/Background/";
    private static string _arcaeaSourceRoot = AppContext.BaseDirectory + "/Andreal/Source/";
    private static string _andreaConfigRoot = AppContext.BaseDirectory + "/Andreal/Config/";
    private static string _tempImageRoot = AppContext.BaseDirectory + "/Andreal/TempImage/";

    private static string _iconPath = "";
    private static string _songPath = "";

    internal static readonly Path ArcaeaDivider = new(_arcaeaSourceRoot + "Divider.png");

    internal static readonly Path ArcaeaGlass = new(_arcaeaSourceRoot + "Glass.png");

    internal static readonly Path ArcaeaBest30Bg = new(_arcaeaSourceRoot + "B30.png");

    internal static readonly Path ArcaeaUnknownBg = new(_arcaeaSourceRoot + "Unknown.png");

    internal static readonly Path ArcaeaBg1Mask = new(_arcaeaSourceRoot + "Mask.png");

    internal static readonly Path PartnerConfig = new(_andreaConfigRoot + "positioninfo.json");

    internal static readonly Path ArcaeaSongs = new(_andreaConfigRoot + "arcsong.json");

    internal static readonly Path Config = new(_andreaConfigRoot + "config.json");


    private readonly string _rawpath;

    private Path(string rawpath) { _rawpath = rawpath; }

    internal bool FileExists => File.Exists(this);

    internal static Path RandImageFileName() => new(_tempImageRoot + $"{RandStringHelper.GetRandString()}.jpg");

    internal static Path ArcaeaBg1(string sid, sbyte difficulty) =>
        new(_arcaeaBackgroundRoot + $"V1_{sid}{(difficulty == 3 ? "_3" : "")}.png");

    internal static Path ArcaeaBg2(string sid, sbyte difficulty) =>
        new(_arcaeaBackgroundRoot + $"V2_{sid}{(difficulty == 3 ? "_3" : "")}.png");

    internal static Path ArcaeaBg3(string sid, sbyte difficulty) =>
        new(_arcaeaBackgroundRoot + $"V3_{sid}{(difficulty == 3 ? "_3" : "")}.png");

    internal static Path ArcaeaBg3Mask(int side) => new(_arcaeaSourceRoot + $"RawV3Bg_{side}.png");

    internal static string ArcaeaSong(string sid, sbyte difficulty)
    {
        var diff = difficulty switch
                   {
                       0 => "0",
                       1 => "1",
                       2 => "base",
                       3 => "3",
                       _ => throw new ArgumentOutOfRangeException(nameof(difficulty))
                   };

        var pth = new DirectoryInfo($"{_songPath}/dl_{sid}").Exists
            ? $"{_songPath}/dl_{sid}"
            : $"{_songPath}/{sid}";

        var result = $"{pth}/{diff}.jpg";

        if (!File.Exists(result)) result = $"{pth}/base.jpg";

        return result;
    }


    internal static Path ArcaeaRating(short potential)
    {
        var img = potential switch
                  {
                      >= 1250 => "6",
                      >= 1200 => "5",
                      >= 1100 => "4",
                      >= 1000 => "3",
                      >= 700  => "2",
                      >= 350  => "1",
                      >= 0    => "0",
                      < 0     => "off"
                  };
        return new(_arcaeaSourceRoot + $"rating_{img}.png");
    }

    internal static Path ArcaeaPartner(int partner, bool awakened) =>
        new(_iconPath + $"/{partner}{(awakened ? "u" : "")}.png");

    internal static Path ArcaeaPartnerIcon(int partner, bool awakened) =>
        new(_iconPath + $"/{partner}{(awakened ? "u" : "")}_icon.png");

    internal static Path ArcaeaCleartypeV3(sbyte cleartype) => new(_arcaeaSourceRoot + $"clear_{cleartype}.png");

    internal static Path ArcaeaCleartypeV1(sbyte cleartype) => new(_arcaeaSourceRoot + $"end_{cleartype}.png");

    internal static Path ArcaeaDifficultyForV1(sbyte difficulty) => new(_arcaeaSourceRoot + $"con_{difficulty}.png");

    public static implicit operator string(Path path) => path._rawpath;

    public static void Init(Config config)
    {
        _iconPath = config.IconPath;
        _songPath = config.SongPath;
    }
}

internal static class RandStringHelper
{
    private static readonly Random Random = new();

    private static readonly string Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

    public static string GetRandString(int length = 10)
    {
        var res = new StringBuilder();
        for (var i = 0; i < length; i++) res.Append(Chars[Random.Next(36)]);
        return res.ToString();
    }
}
