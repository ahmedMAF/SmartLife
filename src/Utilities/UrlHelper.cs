namespace SmartLife.Utilities;

public static class UrlHelper
{
    public static string GetYouTubeVideoId(string url)
    {
        if (url.Contains("youtube.com"))
            return url.Split("v=")[1].Split("&")[0];

        return "";
    }

    public static string GetGoogleMapsEmbedUrl(string url)
    {
        string query;

        if (string.IsNullOrEmpty(url))
            return "";

        if (!url.Contains("google.com/maps/"))
            return "";

        if (url.Contains("place/"))
        {
            int placeIndex = url.IndexOf("place/");
            int atIndex = url.IndexOf("/@", placeIndex);

            if (placeIndex == -1 || atIndex == -1 || atIndex <= placeIndex + 6)
                return "";

            query = url[(placeIndex + 6)..atIndex];
        }
        else if (url.Contains('@'))
        {
            int atIndex = url.IndexOf("/@");
            int start = atIndex + 2; // skip "/@"
            int end = url.IndexOf('z', start); // end just before 'z' (zoom level)

            if (atIndex == -1 || end == -1)
                return "";

            string[] parts = url[start..end].Split(',');

            if (parts.Length < 2)
                return "";

            query = $"{parts[0]},{parts[1]}"; // lat,lng
        }
        else
        {
            return "";
        }

        return $"https://www.google.com/maps?q={query}&output=embed";
    }
}
