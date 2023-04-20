using AndrealImageGenerator.Beans;
using AndrealImageGenerator.Beans.Json;
using AndrealImageGenerator.Graphics;
using AndrealImageGenerator.Graphics.Generators;
using Microsoft.AspNetCore.Mvc;

namespace AndrealImageGenerator;

[ApiController]
public sealed class ImageAPI : ControllerBase
{
    private static Task<BackGround> GetBestsGeneratorImage(UserBestsContent content, ImgVersion imgVersion)
        => imgVersion switch
           {
               ImgVersion.ImgV2 => new Best40Generator(content).Generate(),
               _                => new Best30Generator(content).Generate()
           };

    private static FileStreamResult FileStreamResult(BackGround backGround)
    {
        var ms = new MemoryStream();
        backGround.SaveAsJpgWithQuality(ms);
        ms.Position = 0;
        return new(ms, "image/jpeg");
    }

    [HttpPost("user/best30")]
    public async Task<FileStreamResult> GetUserBest30([FromBody] ResponseRoot<UserBestsContent> json, ImgVersion imgVersion)
    {
        var backGround = await GetBestsGeneratorImage(json.Content, imgVersion);
        return FileStreamResult(backGround);
    }

    [HttpPost("user/best")]
    public async Task<FileStreamResult> GetUserBest([FromBody] ResponseRoot<UserBestContent> json, ImgVersion imgVersion)
    {
        var backGround = await RecordGenerator.Generate(json.Content, imgVersion);
        return FileStreamResult(backGround);
    }

    [HttpPost("user/info")]
    public async Task<FileStreamResult> GetUserInfo([FromBody] ResponseRoot<UserInfoContent> json, ImgVersion imgVersion)
    {
        var backGround = await RecordGenerator.Generate(json.Content, imgVersion);
        return FileStreamResult(backGround);
    }
}
