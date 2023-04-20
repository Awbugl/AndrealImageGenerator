using System.Drawing;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class LineComponent : IGraphicsComponent
{
    private readonly System.Drawing.Color _color;
    private readonly Point _end;
    private readonly int _penwidth;
    private readonly Point _start;

    internal LineComponent(
        System.Drawing.Color color,
        int penwidth,
        Point start,
        Point end)
    {
        _color = color;
        _penwidth = penwidth;
        _start = start;
        _end = end;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g) => g.DrawLine(new(_color, _penwidth), _start, _end);
}
