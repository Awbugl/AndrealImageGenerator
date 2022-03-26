using ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;
using ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

namespace ImageGenerator.Model;

[Serializable]
internal class PlayerInfo
{
    internal PlayerInfo(UserinfoDataItem recentdata, int usercode)
    {
        PlayerId = usercode.ToString("D9");
        PlayerName = recentdata.DisplayName;
        Partner = recentdata.Partner.PartnerId;
        IsAwakened = recentdata.Partner.IsAwakened;
        Potential = recentdata.Potential ?? -1;
    }

    public PlayerInfo(AccountInfo accountInfo)
    {
        PlayerId = accountInfo.Code.ToString("D9");
        PlayerName = accountInfo.Name;
        Partner = accountInfo.Character;
        IsAwakened = accountInfo.IsCharUncapped && !accountInfo.IsCharUncappedOverride;
        Potential = accountInfo.Rating;
    }

    internal string PlayerName { get; init; }

    internal string PlayerId { get; init; }

    internal int Partner { get; }

    internal bool IsAwakened { get; }

    internal short Potential { get; }
}
