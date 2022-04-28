using System.Text;
using ImageGenerator.Json.ArcaeaLimited;
using ImageGenerator.Json.ArcaeaUnlimited;
using ImageGenerator.Model;
using ImageGenerator.UI;
using ImageGenerator.UI.ImageGenerator;
using Newtonsoft.Json;

namespace ImageGenerator;

internal static class Program
{
    private static T Deserialize<T>(string str) where T : class, new() =>
        JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(str))) ?? new T();

    // args[0]             args[1]     args[2]   args[3]                 args[4]
    // 1,2,3,b30,b40,ala   best,info   jsonb64   jsonb64(ala-userinfo)   usercode

    //args[3/4] only for alab30
    public static async Task<int> Main(string[] args)
    {
        var config = JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync(Path.Config))!;
        Path.Init(config);

        using var result = args[0] switch
                           {
                               "1" or "2" or "3" => await GetRecords(),
                               "b30"             => await GetBest30(),
                               "b40"             => await GetBest40(),
                               "ala"             => await GetAlaBest30(),
                               _                 => throw new ArgumentOutOfRangeException()
                           };

        var pth = Path.RandImageFileName();
        result.SaveAsJpgWithQuality(pth);

        Console.WriteLine(new FileInfo(pth).FullName);

        Task<BackGround> GetRecords()
        {
            ArcRecordImageGenerator generator;
            if (args[1] == "info")
            {
                var content = Deserialize<ResponseRoot>(args[2]).DeserializeContent<UserInfoContent>();
                generator = new(new(content.AccountInfo), new(content.RecentScore[0]));
            }
            else
            {
                var content = Deserialize<ResponseRoot>(args[2]).DeserializeContent<UserBestContent>();
                generator = new(new(content.AccountInfo), new(content.Record));
            }

            return args[0] switch
                   {
                       "1" => generator.Version1(),
                       "2" => generator.Version2(),
                       "3" => generator.Version3(),
                       _   => throw new ArgumentOutOfRangeException()
                   };
        }

        Task<BackGround> GetBest30()
        {
            var content = Deserialize<ResponseRoot>(args[2]).DeserializeContent<UserBestsContent>();
            var b30data = new Best30Data(content);
            var playerInfo = new PlayerInfo(content.AccountInfo);
            return new ArcBest30ImageGenerator(b30data, playerInfo).Generate();
        }

        Task<BackGround> GetBest40()
        {
            var content = Deserialize<ResponseRoot>(args[2]).DeserializeContent<UserBestsContent>();
            var b30data = new Best40Data(content);
            var playerInfo = new PlayerInfo(content.AccountInfo);
            return new ArcBest40ImageGenerator(b30data, playerInfo).Generate();
        }

        Task<BackGround> GetAlaBest30()
        {
            var playerInfo = new PlayerInfo(Deserialize<UserinfoData>(args[3]).Data, int.Parse(args[4]));
            var b30data = new LimitedBest30Data(Deserialize<Best30>(args[2]), playerInfo.Potential);
            return new ArcBest30ImageGenerator(b30data, playerInfo).Generate();
        }

        return 0;
    }
}
