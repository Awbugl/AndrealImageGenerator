﻿using System.Drawing;
using AndrealImageGenerator.Beans;
using AndrealImageGenerator.Graphics.Components;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics.Generators;

#pragma warning disable CA1416

internal class BackgroundGenerator
{
    private readonly ArcaeaChart _info;

    public BackgroundGenerator(RecordInfo recordInfo)
    {
        _info = recordInfo.SongInfo;
    }

    internal async Task<BackGround> ArcV1()
    {
        var path = Path.ArcaeaBackground(1, _info);
        return path.FileInfo.Exists ? new(path) : await GenerateArcV1(path);
    }

    private async Task<BackGround> GenerateArcV1(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(0, 87, 512, 341));
        var background = new BackGround(temp, 1440, 960);
        using var masktmp = background.Blur(20).Cut(new(50, 50, 1340, 860)).Blur(80);
        background.FillColor(song.MainColor);
        background.Draw(new ImageComponent(Path.ArcaeaBg1Mask, 0, 0, 1440, 960), new ImageComponent(masktmp, 50, 50, 1340, 860),
                        new ImageComponent(Path.ArcaeaDivider, 100, 840, 1240), new StrokeTextComponent("Pure", Font.Exo40, Color.White, 518, 488),
                        new StrokeTextComponent("Far", Font.Exo40, Color.White, 518, 553),
                        new StrokeTextComponent("Lost", Font.Exo40, Color.White, 518, 618),
                        new StrokeTextComponent("PTT", Font.Exo40, Color.White, 518, 683),
                        new StrokeTextComponent("Generated by Project Andreal", Font.KazesawaLight24, Color.White, 80, 865));
        background.SaveAsPng(path);
        return background;
    }

    internal async Task<BackGround> ArcV2()
    {
        var path = Path.ArcaeaBackground(2, _info);
        return path.FileInfo.Exists ? new(path) : await GenerateArcV2(path);
    }

    private async Task<BackGround> GenerateArcV2(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(0, 112, 512, 288));
        var background = new BackGround(temp, 1920, 1080).Blur(60);
        background.FillColor(song.MainColor);
        background.Draw(new ShadowTextComponent("Play PTT", Font.Exo36, 123, 355), new ShadowTextComponent("Pure", Font.Exo32, 127, 455),
                        new ShadowTextComponent("Far", Font.Exo32, 127, 525), new ShadowTextComponent("Lost", Font.Exo32, 410, 525),
                        new ShadowTextComponent("Played at", Font.Exo32, 127, 595),
                        new ImageComponent(new BackGround(song.Cut(new(0, 19, 512, 48)), 1920, 180).Blur(20), 0, 740),
                        new LineComponent(Color.White, 3, new(0, 740), new(1920, 740)), new LineComponent(Color.White, 3, new(0, 920), new(1920, 920)),
                        new LineComponent(Color.White, 1, new(0, 705), new(1920, 705)), new LineComponent(Color.White, 1, new(0, 955), new(1920, 955)),
                        new RectangleComponent(Color.GetBySide(_info.Side), new(145, 685, 320, 320)), new ImageComponent(song, 130, 670, 320, 320),
                        new ShadowTextComponent(_info.GetSongName(50), Font.Andrea56, 510, 750));
        background.SaveAsPng(path);
        return background;
    }

    internal async Task<BackGround> ArcV3()
    {
        var path = Path.ArcaeaBackground(3, _info);
        return path.FileInfo.Exists ? new(path) : await GenerateArcV3(path);
    }

    private async Task<BackGround> GenerateArcV3(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(78, 0, 354, 512));
        var background = new BackGround(temp, 1000, 1444).Blur(10);
        background.FillColor(Color.White, 100);
        background.Draw(new ImageComponent(Path.ArcaeaBg3Mask(_info.Side), 0, 0, 1000),
                        new TextComponent(_info.GetSongName(30), Font.Beatrice36, Color.Black, 500, 860, StringAlignment.Center),
                        new ImageComponent(song, 286, 408, 428), new TextComponent("PlayPtt:", Font.Exo24, Color.GnaqGray, 110, 1275),
                        new TextComponent("PlayTime:", Font.Exo24, Color.GnaqGray, 110, 1355),
                        new TextComponent("Pure", Font.Exo24, Color.Black, 635, 1260), new TextComponent("Far", Font.Exo24, Color.Black, 635, 1315),
                        new TextComponent("Lost", Font.Exo24, Color.Black, 635, 1370));
        background.SaveAsPng(path);
        return background;
    }
}