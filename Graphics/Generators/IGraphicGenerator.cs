namespace AndrealImageGenerator.Graphics.Generators;

internal interface IGraphicGenerator
{
    Task<BackGround> Generate();
}
