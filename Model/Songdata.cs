using System.Collections.Concurrent;
using ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;
using ImageGenerator.Data.Json.Arcaea.Songlist;
using Newtonsoft.Json;

namespace ImageGenerator.Model;

[Serializable]
internal class Songdata : IEquatable<Songdata>
{
    [NonSerialized] private static Lazy<ConcurrentDictionary<string, Songdata>> _songList
        = new(() => new(JsonConvert.DeserializeObject<ResponseRoot>(File.ReadAllText(Path.ArcaeaSongs))!
                                   .DeserializeContent<SongListContent>().Songs.Select(i => new Songdata(i))
                                   .ToDictionary(i => i.SongId, i => i)));

    private Songdata(SongsItem data)
    {
        Data = data;
        Consts = new()
                 {
                     (double)data.Difficulties[0].RealRating / 10,
                     (double)data.Difficulties[1].RealRating / 10,
                     (double)data.Difficulties[2].RealRating / 10,
                     data.Difficulties.Count == 4
                         ? (double)data.Difficulties[3].RealRating / 10
                         : -0.1
                 };
        Notes = new()
                {
                    data.Difficulties[0].TotalNotes,
                    data.Difficulties[1].TotalNotes,
                    data.Difficulties[2].TotalNotes,
                    data.Difficulties.Count == 4
                        ? data.Difficulties[3].TotalNotes
                        : -1
                };
    }

    public SongsItem Data { get; }

    internal string SongId => Data.Id;

    internal string Songname => Data.TitleLocalized.En;

    internal Side PartnerSide => (Side)Data.Side;

    internal string PackageInfo => Data.Package!;

    internal List<double> Consts { get; }

    internal List<long> Notes { get; }

    public bool Equals(Songdata? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SongId == other.SongId;
    }

    internal string ConstString(sbyte difficulty) =>
        $"[{DifficultyInfo.GetByIndex(difficulty).ShortStr} {Consts[difficulty]:0.0}]";

    internal string GetSongName(byte length) =>
        Songname.Length < length + 3
            ? Songname
            : $"{Songname.Substring(0, length)}...";

    internal static Songdata? GetBySid(string? sid)
    {
        if (sid == null) return null;
        return _songList.Value.TryGetValue(sid, out var result)
            ? result
            : null;
    }
}
