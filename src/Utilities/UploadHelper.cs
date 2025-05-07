using System;

namespace SmartLife.Utilities;

public static class UploadHelper
{
    internal static IWebHostEnvironment Environment { get; set; }

    public static async Task<string> UploadFile(IFormFile file, string folder)
    {
        string rootedPath = Path.Combine(Environment.WebRootPath, folder);
        Directory.CreateDirectory(rootedPath);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(rootedPath, uniqueFileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return $"/{folder}/{uniqueFileName}";
    }
}
