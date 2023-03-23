using System.Collections.Concurrent;
using ImageGenerator.Json.ArcaeaUnlimited;
using Newtonsoft.Json;

namespace ImageGenerator.Model;

internal static class ArcaeaCharts
{
    private static readonly ConcurrentDictionary<string, ArcaeaSong> Songs = new();

    static ArcaeaCharts()
    {
        Songs.Clear();

        List<SongsItem> slst = JsonConvert.DeserializeObject<SongListContent>(File.ReadAllText(Path.ArcaeaSongs))!.Songs;

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
