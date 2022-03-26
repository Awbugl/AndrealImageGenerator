using System.Drawing;

namespace ImageGenerator.UI.Model;

#pragma warning disable CA1416

internal class ImageModel : IGraphicsModel
{
    private readonly Image _image;
    private readonly int? _newWidth, _newHeight;
    private readonly Path _path;
    private readonly int _posX, _posY;

    internal ImageModel(Image image, int posX, int posY, int? newWidth = null, int? newHeight = null)
    {
        _posX = posX;
        _posY = posY;
        _path = null;
        _image = image;
        _newWidth = newWidth;
        _newHeight = newHeight;
    }

    internal ImageModel(Path path, int posX, int posY, int? newWidth = null, int? newHeight = null)
    {
        _posX = posX;
        _posY = posY;
        _path = path;
        _image = null;
        _newWidth = newWidth;
        _newHeight = newHeight;
    }

    void IGraphicsModel.Draw(Graphics g)
    {
        var image = _image ?? new Image(_path);

        if (_newWidth == null && _newHeight == null)
            Image.ImageExtend.DrawImage(g, image, _posX, _posY);
        else
        {
            var newWidth = _newWidth ?? _newHeight * image.Width / image.Height;
            var newHeight = _newHeight ?? _newWidth * image.Height / image.Width;
            Image.ImageExtend.DrawImage(g, image, _posX, _posY, (int)newWidth, (int)newHeight);
        }

        image.Dispose();
    }
}
