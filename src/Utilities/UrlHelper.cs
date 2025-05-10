namespace SmartLife.Utilities;

public static class UrlHelper
{
    public static string GetYouTubeVideoId(string url)
    {
        if (url.Contains("youtube.com"))
            return url.Split("v=")[1];

        return "";
    }
}
