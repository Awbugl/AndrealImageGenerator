using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class ImageComponent : IGraphicsComponent
{
    private readonly Image? _image;
    private readonly int? _newWidth, _newHeight;
    private readonly Path? _path;
    private readonly int _posX, _posY;

    internal ImageComponent(
        Image image,
        int posX,
        int posY,
        int? newWidth = null,
        int? newHeight = null)
    {
        _posX = posX;
        _posY = posY;
        _path = null;
        _image = image;
        _newWidth = newWidth;
        _newHeight = newHeight;
    }

    internal ImageComponent(
        Path path,
        int posX,
        int posY,
        int? newWidth = null,
        int? newHeight = null)
    {
        _posX = posX;
        _posY = posY;
        _path = path;
        _image = null;
        _newWidth = newWidth;
        _newHeight = newHeight;
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g)
    {
        var image = _image ?? new Image(_path!);

        if (_newWidth == null && _newHeight == null)
        {
            Image.ImageExtend.DrawImage(g, image, _posX, _posY);
        }
        else
        {
            var newWidth = _newWidth ?? _newHeight * image.Width / image.Height ?? image.Width;
            var newHeight = _newHeight ?? _newWidth * image.Height / image.Width ?? image.Height;
            Image.ImageExtend.DrawImage(g, image, _posX, _posY, newWidth, newHeight);
        }

        image.Dispose();
    }
}
