using ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

namespace ImageGenerator.Model;

internal class Best30Data : IBest30Data
{
    internal Best30Data(UserBest30Content b30data) { B30data = b30data; }

    private UserBest30Content B30data { get; }


    private string Best30Avg => B30data.Best30Avg.ToString("0.0000");


    private string Recent10Avg =>
        B30data.Recent10Avg > 0
            ? B30data.Recent10Avg.ToString("0.0000")
            : "--";


    private List<RecordInfo> Best30List => B30data.Best30List.Select(i => new RecordInfo(i)).ToList();

    private string Best30TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nB30列表：";

            for (var i = 0; i < Best30List.Count; ++i)
                result
                    += $"\n\n{Best30List[i].SongName(60)} [{Best30List[i].DifficultyInfo.ShortStr}]  #{i + 1}\n Score:{Best30List[i].Score}  PTT:{Best30List[i].Rating}\nPure:{Best30List[i].Pure} (+{Best30List[i].MaxPure})  Far:{Best30List[i].Far}  Lost:{Best30List[i].Lost}";

            return result;
        }
    }

    private string Best5TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nB5列表：";

            for (var i = 0; i < Math.Min(5, Best30List.Count); ++i)
                result
                    += $"\n\n{Best30List[i].SongName(60)} [{Best30List[i].DifficultyInfo.ShortStr}]  #{i + 1}\n Score:{Best30List[i].Score}  PTT:{Best30List[i].Rating}\nPure:{Best30List[i].Pure} (+{Best30List[i].MaxPure})  Far:{Best30List[i].Far}  Lost:{Best30List[i].Lost}";

            return result;
        }
    }

    private string Floor5TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nF5列表：";

            for (var i = 0; i < Math.Min(5, Best30List.Count); ++i)
            {
                var record = Best30List[i + Math.Max(0, Best30List.Count - 5)];
                result
                    += $"\n\n{record.SongName(60)} [{record.DifficultyInfo.ShortStr}]  #{i + Math.Max(0, Best30List.Count - 5) + 1}\n Score:{record.Score}  PTT:{record.Rating}\nPure:{record.Pure} (+{record.MaxPure})  Far:{record.Far}  Lost:{record.Lost}";
            }

            return result;
        }
    }

    string IBest30Data.Best30Avg => Best30Avg;
    string IBest30Data.Recent10Avg => Recent10Avg;
    List<RecordInfo> IBest30Data.Best30List => Best30List;
    string IBest30Data.Best30TextResult => Best30TextResult;
    string IBest30Data.Best5TextResult => Best5TextResult;
    string IBest30Data.Floor5TextResult => Floor5TextResult;
}
