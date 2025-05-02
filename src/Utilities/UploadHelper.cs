using System;

namespace SmartLife.Utilities;

public static class UploadHelper
{
    public static async Task<string> UploadFile(IFormFile file, string folder)
    {
        Directory.CreateDirectory(folder);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(folder, uniqueFileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return filePath;
    }
}
