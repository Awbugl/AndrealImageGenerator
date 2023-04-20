namespace AndrealImageGenerator.Common;

internal static class ExceptionLogger
{
    private static readonly object SyncObj = new();

    internal static void Write(Exception ex)
    {
        lock (SyncObj)
        {
            File.AppendAllText(Path.ExceptionReport, $"{DateTime.Now}\n{ex}\n\n");
        }
    }
}
