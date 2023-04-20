using AndrealImageGenerator.Beans.Json;

namespace AndrealImageGenerator.Beans;

internal class Best40Data
{
    internal Best40Data(UserBestsContent b40data)
    {
        B40data = b40data;
    }

    private UserBestsContent B40data { get; }

    internal string Best30Avg => B40data.Best30Avg.ToString("0.0000");

    internal string Recent10Avg => B40data.Recent10Avg > 0 ? B40data.Recent10Avg.ToString("0.0000") : "--";

    internal List<RecordInfo> Best30List => B40data.Best30List.Select(i => new RecordInfo(i)).ToList();

    internal List<RecordInfo>? OverflowList => B40data.OverflowList?.Select(i => new RecordInfo(i)).ToList();
}
