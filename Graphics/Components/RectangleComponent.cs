using System.Drawing;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class RectangleComponent : IGraphicsComponent
{
    private readonly System.Drawing.Color _color;
    private readonly Rectangle _rect;

    internal RectangleComponent(System.Drawing.Color color, Rectangle rect)
    {
        _color = color;
        _rect = rect;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g)
    {
        using var brash = new SolidBrush(_color);
        g.FillRectangle(brash, _rect);
    }
}
