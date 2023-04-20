using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using AndrealImageGenerator.Graphics.Components;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics;

#pragma warning disable CA1416

internal class BackGround : Image, IDisposable
{
    private System.Drawing.Graphics? _g;

    internal BackGround(Path path) : base(path) { }

    private BackGround(Bitmap bitmap) : base(bitmap) { }

    internal BackGround(int width, int height) : base(width, height) { }

    internal BackGround(Image origin, int width, int height) : base(origin, width, height) { }

    public new void Dispose()
    {
        if (Alreadydisposed) return;
        _g?.Dispose();
        base.Dispose();
    }

    ~BackGround()
    {
        _g?.Dispose();
        base.Dispose();
    }

    internal System.Drawing.Graphics GraphicsFromBackGround()
    {
        if (_g is not null) return _g;
        _g = System.Drawing.Graphics.FromImage(Bitmap);
        _g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        _g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        _g.CompositingQuality = CompositingQuality.HighQuality;
        _g.SmoothingMode = SmoothingMode.HighQuality;
        _g.TextRenderingHint = TextRenderingHint.AntiAlias;
        return _g;
    }

    internal new BackGround Cut(Rectangle rectangle) => new(Bitmap.Clone(rectangle, PixelFormat.Format32bppArgb));

    internal void FillColor(System.Drawing.Color color, int alpha = 120)
        => GraphicsFromBackGround().FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(alpha, color)), 0, 0, Width, Height);

    internal void Draw(params IGraphicsComponent[] graphicsModelCollection)
    {
        foreach (var i in graphicsModelCollection) i.Draw(GraphicsFromBackGround());
    }

    internal BackGround Blur(byte round)
    {
        StackBlur.StackBlurRGBA32(Bitmap, round);
        return this;
    }
}
