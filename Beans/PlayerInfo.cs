using AndrealImageGenerator.Beans.Json;

namespace AndrealImageGenerator.Beans;

[Serializable]
internal class PlayerInfo
{
    public PlayerInfo(AccountInfo accountInfo)
    {
        PlayerCode = accountInfo.Code.ToString("D9");
        PlayerName = accountInfo.Name;
        Partner = accountInfo.Character;
        IsAwakened = accountInfo is { IsCharUncapped: true, IsCharUncappedOverride: false };
        Potential = accountInfo.Rating;
    }

    internal string PlayerName { get; init; }
    internal string PlayerCode { get; init; }
    internal int Partner { get; }
    internal bool IsAwakened { get; }
    internal short Potential { get; }
}
