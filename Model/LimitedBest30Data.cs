using ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;

namespace ImageGenerator.Model;

[Serializable]
internal class LimitedBest30Data : IBest30Data
{
    private double _b30Avg, _r10Avg;
    private List<RecordInfo> _best30List;

    private short _potential;

    internal LimitedBest30Data(Best30 b30data, short potential)
    {
        B30data = b30data;
        _potential = potential;
        _best30List = B30data.Data.Select(i => new RecordInfo(i)).ToList();
        _b30Avg = B30data.Data.Average(i => i.PotentialValue);
        _r10Avg = _potential > 0
            ? (double)_potential / 25 - 3 * _b30Avg
            : -1;
    }

    private Best30 B30data { get; }

    private string Best30Avg => _b30Avg.ToString("0.0000");

    private string Recent10Avg =>
        _r10Avg > 0
            ? _r10Avg.ToString("0.0000")
            : "--";

    private string Best30TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nB30列表：";

            for (var i = 0; i < _best30List.Count; ++i)
                result
                    += $"\n\n{_best30List[i].SongName(60)} [{_best30List[i].DifficultyInfo.ShortStr}]  #{i + 1}\n Score:{_best30List[i].Score}  PTT:{_best30List[i].Rating}\nPure:{_best30List[i].Pure} (+{_best30List[i].MaxPure})  Far:{_best30List[i].Far}  Lost:{_best30List[i].Lost}";

            return result;
        }
    }

    private string Best5TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nB5列表：";

            for (var i = 0; i < Math.Min(5, _best30List.Count); ++i)
                result
                    += $"\n\n{_best30List[i].SongName(60)} [{_best30List[i].DifficultyInfo.ShortStr}]  #{i + 1}\n Score:{_best30List[i].Score}  PTT:{_best30List[i].Rating}\nPure:{_best30List[i].Pure} (+{_best30List[i].MaxPure})  Far:{_best30List[i].Far}  Lost:{_best30List[i].Lost}";

            return result;
        }
    }

    private string Floor5TextResult
    {
        get
        {
            var result = $"您的B30为 {Best30Avg}\n您的R10为 {Recent10Avg}\nF5列表：";

            for (var i = 0; i < Math.Min(5, _best30List.Count); ++i)
            {
                var record = _best30List[i + Math.Max(0, _best30List.Count - 5)];
                result
                    += $"\n\n{record.SongName(60)} [{record.DifficultyInfo.ShortStr}]  #{i + Math.Max(0, _best30List.Count - 5) + 1}\n Score:{record.Score}  PTT:{record.Rating}\nPure:{record.Pure} (+{record.MaxPure})  Far:{record.Far}  Lost:{record.Lost}";
            }

            return result;
        }
    }

    string IBest30Data.Best30Avg => Best30Avg;
    string IBest30Data.Recent10Avg => Recent10Avg;
    List<RecordInfo> IBest30Data.Best30List => _best30List;
    string IBest30Data.Best30TextResult => Best30TextResult;
    string IBest30Data.Best5TextResult => Best5TextResult;
    string IBest30Data.Floor5TextResult => Floor5TextResult;
}
