using AndrealImageGenerator.Beans;
using AndrealImageGenerator.Common;
using Path = AndrealImageGenerator.Common.Path;

namespace AndrealImageGenerator.Graphics.Components;

#pragma warning disable CA1416

internal class PartnerComponent : IGraphicsComponent
{
    private readonly ImageComponent _imageModel;

    internal PartnerComponent(int partner, bool awakened, ImgVersion imgVersion)
    {
        var location = PartnerLocator.Get($"{partner}{(awakened ? "u" : "")}", imgVersion)!;
        _imageModel = new(Path.ArcaeaPartner(partner, awakened).Result, location.PositionX, location.PositionY, location.Size, location.Size);
    }

    void IGraphicsComponent.Draw(System.Drawing.Graphics g) => (_imageModel as IGraphicsComponent).Draw(g);
}
