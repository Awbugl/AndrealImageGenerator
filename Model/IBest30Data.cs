namespace ImageGenerator.Model;

internal interface IBest30Data
{
    internal string Best30Avg { get; }
    internal string Recent10Avg { get; }
    internal List<RecordInfo> Best30List { get; }
    internal string Best30TextResult { get; }
    internal string Best5TextResult { get; }
    internal string Floor5TextResult { get; }
}
