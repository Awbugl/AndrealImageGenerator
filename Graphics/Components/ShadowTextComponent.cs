﻿using System.Drawing;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class ShadowTextComponent : IGraphicsComponent
{
    private static readonly int[] Alpha = { 85, 42, 28, 21, 17 };
    private readonly System.Drawing.Font _font;
    private readonly int _posX, _posY;
    private readonly string _text;

    internal ShadowTextComponent(
        string text,
        System.Drawing.Font font,
        int posX,
        int posY)
    {
        _text = text;
        _font = font;
        _posX = posX;
        _posY = posY;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g)
    {
        for (var i = 0; i < 5; ++i)
        {
            using Brush brush = new SolidBrush(Color.FromArgb(Alpha[i], Color.ArcPurple));
            for (var j = 0; j < 5; ++j)
            {
                g.DrawString(_text, _font, brush, _posX + i, _posY + j);
                g.DrawString(_text, _font, brush, _posX - i, _posY - j);
            }

            g.DrawString(_text, _font, brush, _posX - i, _posY + i);
            g.DrawString(_text, _font, brush, _posX + i, _posY - i);
        }

        g.DrawString(_text, _font, Brushes.White, _posX, _posY);
    }
}
