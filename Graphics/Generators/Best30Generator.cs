using AndrealImageGenerator.Beans;
using AndrealImageGenerator.Beans.Json;
using AndrealImageGenerator.Graphics.Components;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics.Generators;

internal class Best30Generator : IGraphicGenerator
{
    public Best30Generator(UserBestsContent content)
    {
        B30data = new(content);
        Info = new(content.AccountInfo);
    }

    private Best30Data B30data { get; }
    private PlayerInfo Info { get; }

    public async Task<BackGround> Generate()
    {
        var bg = new BackGround(Path.ArcaeaBest30Bg);
        bg.Draw(new TextComponent(Info.PlayerName, Font.Andrea72, Color.White, 345, 175),
                new TextComponent($"ArcCode: {Info.PlayerCode}", Font.ExoLight28, Color.White, 370, 320),
                new TextComponent($"Total Best 30: {B30data.Best30Avg}", Font.Andrea60, Color.White, 145, 480),
                new TextComponent($"Recent Best 10: {B30data.Recent10Avg}", Font.Andrea60, Color.White, 1095, 480),
                new ImageComponent(await Path.ArcaeaPartnerIcon(Info.Partner, Info.IsAwakened), 75, 130, 255),
                new PotentitalComponent(Info.Potential, 175, 235));

        var len = Math.Min(B30data.Best30List.Count, 30);
        for (var i = 0; i < len; ++i)
        {
            var record = B30data.Best30List[i];
            int x = 75 + i % 2 * 950, y = 660 + i / 2 * 350;
            using var song = await record.GetSongImage();

            bg.Draw(
                    new PolygonComponent(Color.White, new(x + 9, y), new(x + 891, y), new(x + 900, y + 9), new(x + 900, y + 291), new(x + 891, y + 300),
                                     new(x + 9, y + 300), new(x, y + 291), new(x, y + 9)),
                    new PolygonComponent(record.DifficultyInfo.Color, new(x + 278, y + 22), new(x + 278, y + 70), new(x + 503, y + 70),
                                     new(x + 458, y + 22)), new ImageComponent(song, x + 22, y + 22, 256, 256),
                    new TextComponent($"[{record.Const:0.0}] {record.SongName(11)}", Font.Beatrice36, song.MainColor, x + 295, y + 80),
                    new TextComponent(record.Score, Font.Exo44, song.MainColor, x + 290, y + 145),
                    new TextComponent(record.Rating, Font.Exo26, System.Drawing.Color.White, x + 297, y + 24),
                    new TextComponent($"#{i + 1}", Font.Beatrice26, System.Drawing.Color.Black, x + 800, y + 24),
                    new TextComponent($"Pure: {record.Pure} (+{record.MaxPure})", Font.Beatrice20, System.Drawing.Color.FromArgb(105, 68, 100),
                                      x + 300, y + 235),
                    new TextComponent($"Far: {record.Far}", Font.Beatrice20, System.Drawing.Color.FromArgb(216, 157, 49), x + 570, y + 235),
                    new TextComponent($"Lost: {record.Lost}", Font.Beatrice20, System.Drawing.Color.FromArgb(159, 83, 109), x + 730, y + 235));
        }

        return bg;
    }
}
