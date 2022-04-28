using ImageGenerator.Json.ArcaeaLimited;
using ImageGenerator.Json.ArcaeaUnlimited;

namespace ImageGenerator.Model;

[Serializable]
internal class PlayerInfo
{
    internal PlayerInfo(UserinfoDataItem recentdata, int usercode)
    {
        PlayerCode = usercode.ToString("D9");
        PlayerName = recentdata.DisplayName;
        Partner = recentdata.Partner.PartnerID;
        IsAwakened = recentdata.Partner.IsAwakened;
        Potential = recentdata.Potential ?? -1;
    }

    public PlayerInfo(AccountInfo accountInfo)
    {
        PlayerCode = accountInfo.Code.ToString("D9");
        PlayerName = accountInfo.Name;
        Partner = accountInfo.Character;
        IsAwakened = accountInfo.IsCharUncapped && !accountInfo.IsCharUncappedOverride;
        Potential = accountInfo.Rating;
    }

    internal string PlayerName { get; init; }
    internal string PlayerCode { get; init; }
    internal int Partner { get; }
    internal bool IsAwakened { get; }
    internal short Potential { get; }
}
