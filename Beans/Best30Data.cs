using AndrealImageGenerator.Beans.Json;

namespace AndrealImageGenerator.Beans;

internal class Best30Data
{
    internal Best30Data(UserBestsContent b30data)
    {
        B30data = b30data;
    }

    private UserBestsContent B30data { get; }

    internal string Best30Avg => B30data.Best30Avg.ToString("0.0000");

    internal string Recent10Avg => B30data.Recent10Avg > 0 ? B30data.Recent10Avg.ToString("0.0000") : "--";

    internal List<RecordInfo> Best30List => B30data.Best30List.Select(i => new RecordInfo(i)).ToList();
}
