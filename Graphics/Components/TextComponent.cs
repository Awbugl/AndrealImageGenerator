using System.Drawing;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class TextComponent : IGraphicsComponent
{
    private readonly System.Drawing.Color _color;
    private readonly System.Drawing.Font _font;
    private readonly int _posX, _posY;
    private readonly StringAlignment _stringAlignment;
    private readonly string _text;

    internal TextComponent(
        string text,
        System.Drawing.Font font,
        System.Drawing.Color color,
        int posX,
        int posY,
        StringAlignment stringAlignment = StringAlignment.Near)
    {
        _text = text;
        _color = color;
        _font = font;
        _posX = posX;
        _posY = posY;
        _stringAlignment = stringAlignment;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g)
    {
        using var sf = new StringFormat { Alignment = _stringAlignment };
        using var brush = new SolidBrush(_color);
        g.DrawString(_text, _font, brush, _posX, _posY, sf);
    }
}
