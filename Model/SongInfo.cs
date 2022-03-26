using System.Drawing;
using Image = ImageGenerator.UI.Image;

namespace ImageGenerator.Model;

[Serializable]
internal class SongInfo
{
    private SongInfo(Songdata songMetadata, sbyte difficulty)
    {
        Songdata = songMetadata;
        Difficulty = difficulty;
    }

    internal SongInfo(string sid, sbyte difficulty)
    {
        Songdata = Songdata.GetBySid(sid);
        Difficulty = difficulty;
    }

    private Songdata? Songdata { get; }

    internal sbyte Difficulty { get; }

    internal string SongName => Songdata!.Songname;

    internal string SongId => Songdata!.SongId;

    internal double Const => Songdata!.Consts[Difficulty];

    internal Side PartnerSide => Songdata!.PartnerSide;

    internal DifficultyInfo DifficultyInfo => DifficultyInfo.GetByIndex(Difficulty);

    internal Color MainColor
    {
        get
        {
            using var img = GetSongImg();
            return img.MainColor;
        }
    }

    internal string ConstString => $"[{DifficultyInfo.ShortStr} {Const:0.0}]";

    internal Image GetSongImg()
    {
        var pth = Path.ArcaeaSong(Songdata!.SongId, Difficulty);

        var img = new Image(pth);
        if (img.Width == 512) return img;

        var newimg = new Image(img, 512, 512);
        img.Dispose();
        return newimg;
    }


    internal string GetSongName(byte length) => Songdata!.GetSongName(length);


    public static implicit operator SongInfo((Songdata, sbyte) valueTuple) => new(valueTuple.Item1, valueTuple.Item2);

    private static IEnumerable<SongInfo> Convert(IEnumerable<(Songdata, sbyte)> ls) =>
        ls.Select(i => new SongInfo(i.Item1, i.Item2));
}
