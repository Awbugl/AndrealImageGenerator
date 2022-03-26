﻿using System.Drawing;
using ImageGenerator.Model;
using ImageGenerator.UI.Model;

namespace ImageGenerator.UI.ImageGenerator;

#pragma warning disable CA1416

internal class ArcRecordImageGenerator
{
    private static readonly string[] CleartypeString = { "[TL]", "[NC]", "[FR]", "[PM]", "[EC]", "[HC]" };

    internal ArcRecordImageGenerator(PlayerInfo playerInfo, RecordInfo recordInfo)
    {
        PlayerInfo = playerInfo;
        RecordInfo = recordInfo;
    }

    private PlayerInfo PlayerInfo { get; }
    private RecordInfo RecordInfo { get; }

    internal BackGround UnknownSong(double theconst)
    {
        var bg = new BackGround(Path.ArcaeaUnknownBg);
        bg.Draw(new ImageModel(Path.ArcaeaCleartypeV3(RecordInfo.Cleartype), 185, 1035, 630),
                new ImageModel(Path.ArcaeaPartnerIcon(PlayerInfo.Partner, PlayerInfo.IsAwakened), 150, 160, 160),
                new TextOnlyModel(PlayerInfo.PlayerName, Font.Andrea36, Color.Black, 340, 200),
                new TextOnlyModel($"ArcID: {PlayerInfo.PlayerId}", Font.GeosansLight20, Color.GnaqGray, 340, 270),
                new TextOnlyModel($"{RecordInfo.DifficultyInfo.LongStr} | {theconst:0.00}", Font.Beatrice24,
                                  RecordInfo.DifficultyInfo.Color, 500, 925, StringAlignment.Center),
                new PotentitalModel(PlayerInfo.Potential, 215, 215, 140),
                new TextOnlyModel($"{RecordInfo.Score}  {RecordInfo.Rate}", Font.Exo44, Color.Black, 500, 1130,
                                  StringAlignment.Center),
                new TextOnlyModel(RecordInfo.Rating, Font.Exo20, Color.GnaqGray, 260, 1280),
                new TextOnlyModel(RecordInfo.TimeStr, Font.Exo20, Color.GnaqGray, 260, 1360),
                new TextOnlyModel(RecordInfo.Pure + $" (+{RecordInfo.MaxPure})", Font.Exo20, Color.Black, 730, 1265),
                new TextOnlyModel(RecordInfo.Far, Font.Exo20, Color.Black, 730, 1320),
                new TextOnlyModel(RecordInfo.Lost, Font.Exo20, Color.Black, 730, 1375));
        return bg;
    }

    internal BackGround Version1()
    {
        var bg = new ArcBackgroundGenerator(RecordInfo.SongInfo).ArcV1();

        using var song = RecordInfo.GetSongImg();

        bg.Draw(new PartnerModel(PlayerInfo.Partner, PlayerInfo.IsAwakened, ImgVersion.ImgV1),
                new PotentitalModel(PlayerInfo.Potential, 87, 60), new ImageModel(Path.ArcaeaGlass, 810, 240, 630),
                new TextWithStrokeModel(PlayerInfo.PlayerName, Font.KazesawaLight40, Color.White, 275, 100),
                new TextWithStrokeModel("ID " + PlayerInfo.PlayerId, Font.KazesawaLight32, Color.White, 275, 160),
                new ImageModel(Path.ArcaeaCleartypeV1(RecordInfo.Cleartype), -56, 224, 700),
                new ImageModel(song, 150, 430, 290),
                new ImageModel(Path.ArcaeaDifficultyForV1(RecordInfo.Difficulty), 333, 680, 150),
                new TextWithStrokeModel(RecordInfo.Const.ToString("0.0"), Font.Exo36, Color.White, 350, 677,
                                        Color.Black, 2, StringAlignment.Center),
                new TextWithStrokeModel(RecordInfo.SongName(23), Font.KazesawaRegular56, Color.White, 105, 253),
                new TextWithStrokeModel(RecordInfo.Score, Font.Exo64, RecordInfo.Cleartype == 3
                                            ? Color.PmColor
                                            : Color.White, 515, 370),
                new TextWithStrokeModel(RecordInfo.Pure + $" (+{RecordInfo.MaxPure})", Font.Exo40, Color.White, 638,
                                        488),
                new TextWithStrokeModel(RecordInfo.Far, Font.Exo40, Color.White, 638, 553),
                new TextWithStrokeModel(RecordInfo.Lost, Font.Exo40, Color.White, 638, 618),
                new TextWithStrokeModel(RecordInfo.Rating, Font.Exo40, Color.White, 638, 683),
                new TextWithStrokeModel("Played at  " + RecordInfo.TimeStr, Font.Exo40, Color.White, 368, 758));
        return bg;
    }

    internal BackGround Version2()
    {
        var bg = new ArcBackgroundGenerator(RecordInfo.SongInfo).ArcV2();
        bg.Draw(new TextWithShadowModel($"{RecordInfo.DifficultyInfo.LongStr}  {RecordInfo.Const:0.0}", Font.Andrea36, 514, 850),
                new PartnerModel(PlayerInfo.Partner, PlayerInfo.IsAwakened, ImgVersion.ImgV2),
                new PotentitalModel(PlayerInfo.Potential, 79, 38), new TextWithShadowModel(RecordInfo.IsRecent
                                                                                               ? "Recent"
                                                                                               : "Best", Font.Exo44,
                                                                                           120, 260),
                new TextWithShadowModel(RecordInfo.Score, Font.Exo36, 398, 270),
                new TextWithShadowModel($"{RecordInfo.Rate}{CleartypeString[RecordInfo.Cleartype]}", Font.Exo36, 730,
                                        270), new TextWithShadowModel(RecordInfo.Rating, Font.Exo36, 398, 354),
                new TextWithShadowModel(RecordInfo.Pure, Font.Exo32, 240, 455),
                new TextWithShadowModel($"(+{RecordInfo.MaxPure})", Font.Exo32, 415, 455),
                new TextWithShadowModel(RecordInfo.Far, Font.Exo32, 240, 525),
                new TextWithShadowModel(RecordInfo.Lost, Font.Exo32, 560, 525),
                new TextWithShadowModel(RecordInfo.TimeStr, Font.Exo32, 350, 595),
                new TextWithShadowModel(PlayerInfo.PlayerName, Font.Andrea56, 290, 60),
                new TextWithShadowModel($"ArcID: {PlayerInfo.PlayerId}", Font.Andrea28, 297, 150));
        return bg;
    }

    internal BackGround Version3()
    {
        var bg = new ArcBackgroundGenerator(RecordInfo.SongInfo).ArcV3();
        bg.Draw(new ImageModel(Path.ArcaeaCleartypeV3(RecordInfo.Cleartype), 185, 1035, 630),
                new ImageModel(Path.ArcaeaPartnerIcon(PlayerInfo.Partner, PlayerInfo.IsAwakened), 150, 160, 160),
                new PotentitalModel(PlayerInfo.Potential, 215, 215, 140),
                new TextOnlyModel(PlayerInfo.PlayerName, Font.Andrea36, Color.Black, 340, 200),
                new TextOnlyModel($"ArcID: {PlayerInfo.PlayerId}", Font.GeosansLight20, Color.GnaqGray, 340, 270),
                new TextOnlyModel($"{RecordInfo.DifficultyInfo.LongStr} | {RecordInfo.Const:0.0}", Font.Beatrice24,
                                  RecordInfo.DifficultyInfo.Color, 500, 925, StringAlignment.Center),
                new TextOnlyModel($"{RecordInfo.Score}  {RecordInfo.Rate}", Font.Exo44, Color.Black, 500, 1130,
                                  StringAlignment.Center),
                new TextOnlyModel(RecordInfo.Rating, Font.Exo20, Color.GnaqGray, 260, 1280),
                new TextOnlyModel(RecordInfo.TimeStr, Font.Exo20, Color.GnaqGray, 260, 1360),
                new TextOnlyModel(RecordInfo.Pure + $" (+{RecordInfo.MaxPure})", Font.Exo20, Color.Black, 730, 1265),
                new TextOnlyModel(RecordInfo.Far, Font.Exo20, Color.Black, 730, 1320),
                new TextOnlyModel(RecordInfo.Lost, Font.Exo20, Color.Black, 730, 1375));
        return bg;
    }
}
