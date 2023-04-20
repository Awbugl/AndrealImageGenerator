using System.Drawing;
using AndrealImageGenerator.Beans;
using AndrealImageGenerator.Beans.Json;
using AndrealImageGenerator.Graphics.Components;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics.Generators;

internal abstract class RecordGenerator : IGraphicGenerator
{
    protected static readonly string[] CleartypeString = { "[TL]", "[NC]", "[FR]", "[PM]", "[EC]", "[HC]" };

    public static Task<BackGround> Generate(UserBestContent content, ImgVersion version)
    {
        RecordGenerator g = version switch
                            {
                                ImgVersion.ImgV2 => new RecordGeneratorV2(content),
                                ImgVersion.ImgV3 => new RecordGeneratorV3(content),
                                _                => new RecordGeneratorV1(content),
                            };
        return g.Generate();
    }

    public static Task<BackGround> Generate(UserInfoContent content, ImgVersion version)
    {
        RecordGenerator g = version switch
                            {
                                ImgVersion.ImgV2 => new RecordGeneratorV2(content),
                                ImgVersion.ImgV3 => new RecordGeneratorV3(content),
                                _                => new RecordGeneratorV1(content),
                            };

        return g.Generate();
    }

    protected RecordGenerator(UserBestContent content)
    {
        PlayerInfo = new(content.AccountInfo);
        RecordInfo = new(content.Record);
    }

    protected RecordGenerator(UserInfoContent content)
    {
        PlayerInfo = new(content.AccountInfo);
        RecordInfo = new(content.RecentScore[0]);
    }

    protected PlayerInfo PlayerInfo { get; }
    protected RecordInfo RecordInfo { get; }

    public abstract Task<BackGround> Generate();
}

internal class RecordGeneratorV1 : RecordGenerator
{
    public RecordGeneratorV1(UserBestContent content) : base(content) { }
    public RecordGeneratorV1(UserInfoContent content) : base(content) { }

    public override async Task<BackGround> Generate()
    {
        var bg = await new BackgroundGenerator(RecordInfo).ArcV1();

        using var song = await RecordInfo.GetSongImage();

        bg.Draw(new PartnerComponent(PlayerInfo.Partner, PlayerInfo.IsAwakened, ImgVersion.ImgV1),
                new PotentitalComponent(PlayerInfo.Potential, 87, 60), new ImageComponent(Path.ArcaeaGlass, 810, 240, 630),
                new StrokeTextComponent(PlayerInfo.PlayerName, Font.KazesawaLight40, Color.White, 275, 100),
                new StrokeTextComponent("Code " + PlayerInfo.PlayerCode, Font.KazesawaLight32, Color.White, 275, 160),
                new ImageComponent(Path.ArcaeaCleartypeV1(RecordInfo.Cleartype), -56, 224, 700), new ImageComponent(song, 150, 430, 290),
                new ImageComponent(Path.ArcaeaDifficultyForV1(RecordInfo.Difficulty), 333, 680, 150),
                new StrokeTextComponent(RecordInfo.Const.ToString("0.0"), Font.Exo36, Color.White, 350, 677, Color.Black, 2, StringAlignment.Center),
                new StrokeTextComponent(RecordInfo.SongName(23), Font.KazesawaRegular56, Color.White, 105, 253),
                new StrokeTextComponent(RecordInfo.Score, Font.Exo64, RecordInfo.Cleartype == 3 ? Color.PmColor : Color.White, 515, 370),
                new StrokeTextComponent(RecordInfo.Pure + $" (+{RecordInfo.MaxPure})", Font.Exo40, Color.White, 638, 488),
                new StrokeTextComponent(RecordInfo.Far, Font.Exo40, Color.White, 638, 553),
                new StrokeTextComponent(RecordInfo.Lost, Font.Exo40, Color.White, 638, 618),
                new StrokeTextComponent(RecordInfo.Rating, Font.Exo40, Color.White, 638, 683),
                new StrokeTextComponent("Played at  " + RecordInfo.TimeStr, Font.Exo40, Color.White, 368, 758));
        return bg;
    }
}

internal class RecordGeneratorV2 : RecordGenerator
{
    public RecordGeneratorV2(UserBestContent content) : base(content) { }
    public RecordGeneratorV2(UserInfoContent content) : base(content) { }

    public override async Task<BackGround> Generate()
    {
        var bg = await new BackgroundGenerator(RecordInfo).ArcV2();
        bg.Draw(new ShadowTextComponent($"{RecordInfo.DifficultyInfo.LongStr}  {RecordInfo.Const:0.0}", Font.Andrea36, 514, 850),
                new PartnerComponent(PlayerInfo.Partner, PlayerInfo.IsAwakened, ImgVersion.ImgV2),
                new PotentitalComponent(PlayerInfo.Potential, 79, 38),
                new ShadowTextComponent(RecordInfo.IsRecent ? "Recent" : "Best", Font.Exo44, 120, 260),
                new ShadowTextComponent(RecordInfo.Score, Font.Exo36, 398, 270),
                new ShadowTextComponent($"{RecordInfo.Rate}{CleartypeString[RecordInfo.Cleartype]}", Font.Exo36, 730, 270),
                new ShadowTextComponent(RecordInfo.Rating, Font.Exo36, 398, 354), new ShadowTextComponent(RecordInfo.Pure, Font.Exo32, 240, 455),
                new ShadowTextComponent($"(+{RecordInfo.MaxPure})", Font.Exo32, 415, 455),
                new ShadowTextComponent(RecordInfo.Far, Font.Exo32, 240, 525), new ShadowTextComponent(RecordInfo.Lost, Font.Exo32, 560, 525),
                new ShadowTextComponent(RecordInfo.TimeStr, Font.Exo32, 350, 595),
                new ShadowTextComponent(PlayerInfo.PlayerName, Font.Andrea56, 290, 60),
                new ShadowTextComponent($"ArcCode: {PlayerInfo.PlayerCode}", Font.Andrea28, 297, 150));
        return bg;
    }
}

internal class RecordGeneratorV3 : RecordGenerator
{
    public RecordGeneratorV3(UserBestContent content) : base(content) { }
    public RecordGeneratorV3(UserInfoContent content) : base(content) { }

    public override async Task<BackGround> Generate()
    {
        var bg = await new BackgroundGenerator(RecordInfo).ArcV3();
        bg.Draw(new ImageComponent(Path.ArcaeaCleartypeV3(RecordInfo.Cleartype), 185, 1035, 630),
                new ImageComponent(await Path.ArcaeaPartnerIcon(PlayerInfo.Partner, PlayerInfo.IsAwakened), 150, 160, 160),
                new PotentitalComponent(PlayerInfo.Potential, 215, 215, 140),
                new TextComponent(PlayerInfo.PlayerName, Font.Andrea36, Color.Black, 340, 200),
                new TextComponent($"ArcCode: {PlayerInfo.PlayerCode}", Font.Andrea20, Color.GnaqGray, 340, 270),
                new TextComponent($"{RecordInfo.DifficultyInfo.LongStr} | {RecordInfo.Const:0.0}", Font.Beatrice24, RecordInfo.DifficultyInfo.Color,
                                  500, 925, StringAlignment.Center),
                new TextComponent($"{RecordInfo.Score}  {RecordInfo.Rate}", Font.Exo44, Color.Black, 500, 1130, StringAlignment.Center),
                new TextComponent(RecordInfo.Rating, Font.Exo20, Color.GnaqGray, 260, 1280),
                new TextComponent(RecordInfo.TimeStr, Font.Exo20, Color.GnaqGray, 260, 1360),
                new TextComponent(RecordInfo.Pure + $" (+{RecordInfo.MaxPure})", Font.Exo20, Color.Black, 730, 1265),
                new TextComponent(RecordInfo.Far, Font.Exo20, Color.Black, 730, 1320),
                new TextComponent(RecordInfo.Lost, Font.Exo20, Color.Black, 730, 1375));
        return bg;
    }
}
