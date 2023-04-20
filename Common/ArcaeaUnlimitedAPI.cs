using AndrealImageGenerator.Beans.Json;
using Newtonsoft.Json;

namespace AndrealImageGenerator.Common;

internal static class ArcaeaUnlimitedAPI
{
    private static readonly HttpClient? Client = new() { BaseAddress = new("https://server.awbugl.top/botarcapi/") };

    private static async Task<ResponseRoot<T>?> GetString<T>(string url)
        => JsonConvert.DeserializeObject<ResponseRoot<T>>(await (await Client!.SendAsync(new(HttpMethod.Get, url))).Content.ReadAsStringAsync());

    private static async Task GetImage(string url, Path filename)
    {
        FileStream? fileStream = null;
        var message = await Client!.GetAsync(url);

        if (message.Content.Headers.ContentType?.MediaType?.StartsWith("image/") != true)
            throw new ArgumentException(JsonConvert.DeserializeObject<ResponseRoot<object>>(await message.Content.ReadAsStringAsync())!.Message);

        var exflag = false;

        try
        {
            fileStream = new(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            await message.Content.CopyToAsync(fileStream);
        }
        catch
        {
            exflag = true;
            throw;
        }
        finally
        {
            try
            {
                if (fileStream is not null)
                {
                    fileStream.Close();
                    await fileStream.DisposeAsync();
                }
            }
            catch
            {
                // ignore
            }

            try
            {
                if (exflag && File.Exists(filename)) File.Delete(filename);
            }
            catch
            {
                // ignore
            }
        }
    }

    internal static async Task<ResponseRoot<SongListContent>?> SongList() => await GetString<SongListContent>("song/list");

    internal static async Task SongAssets(string sid, int difficulty, Path pth)
        => await GetImage($"assets/song?songid={sid}&difficulty={difficulty}", pth);

    internal static async Task CharAssets(int partner, bool awakened, Path pth)
        => await GetImage($"assets/char?partner={partner}&awakened={(awakened ? "true" : "false")}", pth);

    internal static async Task IconAssets(int partner, bool awakened, Path pth)
        => await GetImage($"assets/icon?partner={partner}&awakened={(awakened ? "true" : "false")}", pth);
}
