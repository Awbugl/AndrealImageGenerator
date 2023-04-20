using System.Collections.Concurrent;
using AndrealImageGenerator.Beans.Json;
using AndrealImageGenerator.Common;
using Newtonsoft.Json;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Beans;

internal static class ArcaeaCharts
{
    private static readonly ConcurrentDictionary<string, ArcaeaSong> Songs = new();

    static ArcaeaCharts()
    {
        Songs.Clear();

        List<SongsItem>? slst;

        try
        {
            slst = ArcaeaUnlimitedAPI.SongList().Result?.Content.Songs;
            if (slst != null) File.WriteAllText(Path.TmpSongList, JsonConvert.SerializeObject(slst));
        }
        catch
        {
            if (!File.Exists(Path.TmpSongList)) throw;
            slst = JsonConvert.DeserializeObject<List<SongsItem>>(File.ReadAllText(Path.TmpSongList));
        }

        if (slst == null) return;

        foreach (var songitem in slst)
        {
            songitem.Difficulties.SongID = songitem.SongID;

            for (var i = 0; i < songitem.Difficulties.Count; ++i)
            {
                songitem.Difficulties[i].RatingClass = i;
                songitem.Difficulties[i].SongID = songitem.SongID;
            }

            Songs.TryAdd(songitem.SongID, songitem.Difficulties);
        }
    }

    internal static ArcaeaSong? QueryByID(string? songid) => songid is not null && Songs.TryGetValue(songid, out var value) ? value : null;
}
