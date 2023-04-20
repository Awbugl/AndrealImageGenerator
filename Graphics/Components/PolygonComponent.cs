using System.Drawing;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class PolygonComponent : IGraphicsComponent
{
    private readonly System.Drawing.Color _color;
    private readonly Point[] _region;

    internal PolygonComponent(System.Drawing.Color color, params Point[] region)
    {
        _color = color;
        _region = region;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g)
    {
        using var brash = new SolidBrush(_color);
        g.FillPolygon(brash, _region);
    }
}
