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
        if (string.IsNullOrEmpty(url))
            return "";

        if (url.Contains("iframe"))
        {
            int srcIndex = url.IndexOf("src=\"", StringComparison.OrdinalIgnoreCase);
            string srcUrl = "";

            if (srcIndex != -1)
            {
                srcIndex += 5;
                int endIndex = url.IndexOf('"', srcIndex);

                if (endIndex != -1)
                    return url[srcIndex..endIndex];
            }
        }

        if (url.Contains("embed"))
            return url;

        if (!url.Contains("google.com/maps/"))
            return "";

        string query;

        if (url.Contains("maps/place/"))
        {
            int placeIndex = url.IndexOf("place/");
            int atIndex = url.IndexOf("/@", placeIndex);

            int startIndex = placeIndex + 6;
            
            if (placeIndex == -1 || atIndex == -1 || atIndex <= startIndex)
                return "";

            query = url[startIndex..atIndex];
        }
        else if (url.Contains("maps/@"))
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
