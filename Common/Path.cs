using AndrealImageGenerator.Beans;

namespace AndrealImageGenerator.Common;

[Serializable]
public class Path
{
    private static readonly string BaseDirectory = AppContext.BaseDirectory;

    public static readonly string ArcaeaBackgroundRoot = BaseDirectory + "/Andreal/Background/",
                                  ArcaeaImageRoot = BaseDirectory + "/Andreal/Arcaea/",
                                  ArcaeaSourceRoot = BaseDirectory + "/Andreal/Source/",
                                  ArcaeaFontRoot = BaseDirectory + "/Andreal/Fonts/",
                                  AndreaConfigRoot = BaseDirectory + "/Andreal/Config/";

    public static readonly Path PartnerConfig = new(AndreaConfigRoot + "positioninfo.json"),
                                TmpSongList = new(AndreaConfigRoot + "tempsonglist.json"),
                                ArcaeaDivider = new(ArcaeaSourceRoot + "Divider.png"),
                                ArcaeaGlass = new(ArcaeaSourceRoot + "Glass.png"),
                                ArcaeaBest30Bg = new(ArcaeaSourceRoot + "B30.png"),
                                ArcaeaBest40Bg = new(ArcaeaSourceRoot + "B40.png"),
                                ArcaeaBg1Mask = new(ArcaeaSourceRoot + "Mask.png"),
                                ExceptionReport = new(BaseDirectory + "/Andreal/ExceptionReport.log");

    private readonly string _rawpath;

    private FileInfo? _fileInfo;

    static Path()
    {
        Directory.CreateDirectory(ArcaeaBackgroundRoot);
        Directory.CreateDirectory(ArcaeaSourceRoot);
        Directory.CreateDirectory(ArcaeaFontRoot);
        Directory.CreateDirectory(ArcaeaImageRoot + "Song/");
        Directory.CreateDirectory(ArcaeaImageRoot + "Char/");
        Directory.CreateDirectory(ArcaeaImageRoot + "Icon/");
    }

    private Path(string rawpath)
    {
        _rawpath = rawpath;
    }

    public FileInfo FileInfo => _fileInfo ??= new(this);

    public static Path ArcaeaBackground(int version, ArcaeaChart chart) => new(ArcaeaBackgroundRoot + $"V{version}_{ArcaeaTempSong(chart)}.png");

    public static Path ArcaeaBg3Mask(Side side) => new(ArcaeaSourceRoot + $"RawV3Bg_{(int)side}.png");

    public static async Task<Path> ArcaeaSong(ArcaeaChart chart)
    {
        var song = ArcaeaTempSong(chart);

        var pth = new Path($"{ArcaeaImageRoot}Song/{song}.jpg");

        if (pth.FileInfo.Exists)
        {
            if (pth.FileInfo.Length > 10240) return pth;
            pth.FileInfo.Delete();
        }

        await ArcaeaUnlimitedAPI.SongAssets(chart.SongID, chart.RatingClass, pth);

        return pth;
    }

    private static string ArcaeaTempSong(ArcaeaChart chart)
    {
        var song = chart switch
                   {
                       _ when chart.JacketOverride           => $"{chart.SongID}_{chart.RatingClass}",
                       _ when chart.SongID == "melodyoflove" => $"melodyoflove{(DateTime.Now.Hour is > 19 or < 6 ? "_night" : "")}",
                       _                                     => chart.SongID
                   };
        return song;
    }

    public static Path ArcaeaRating(short potential)
    {
        var img = potential switch
                  {
                      >= 1300 => "7",
                      >= 1250 => "6",
                      >= 1200 => "5",
                      >= 1100 => "4",
                      >= 1000 => "3",
                      >= 700  => "2",
                      >= 350  => "1",
                      >= 0    => "0",
                      < 0     => "off"
                  };
        return new(ArcaeaSourceRoot + $"rating_{img}.png");
    }

    public static async Task<Path> ArcaeaPartner(int partner, bool awakened)
    {
        var pth = new Path(ArcaeaImageRoot + $"Char/{partner}{(awakened ? "u" : "")}.png");

        if (pth.FileInfo.Exists)
        {
            if (pth.FileInfo.Length > 10240) return pth;
            pth.FileInfo.Delete();
        }

        await ArcaeaUnlimitedAPI.CharAssets(partner, awakened, pth);

        return pth;
    }

    public static async Task<Path> ArcaeaPartnerIcon(int partner, bool awakened)
    {
        var pth = new Path(ArcaeaImageRoot + $"Icon/{partner}{(awakened ? "u" : "")}.png");

        if (pth.FileInfo.Exists)
        {
            if (pth.FileInfo.Length > 10240) return pth;
            pth.FileInfo.Delete();
        }

        await ArcaeaUnlimitedAPI.IconAssets(partner, awakened, pth);

        return pth;
    }

    public static Path ArcaeaCleartypeV1(sbyte cleartype) => new(ArcaeaSourceRoot + $"end_{cleartype}.png");

    public static Path ArcaeaCleartypeV3(sbyte cleartype) => new(ArcaeaSourceRoot + $"clear_{cleartype}.png");

    public static Path ArcaeaDifficultyForV1(int difficulty) => new(ArcaeaSourceRoot + $"con_{difficulty}.png");

    public static implicit operator string(Path path) => path._rawpath;
}
