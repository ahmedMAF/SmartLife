namespace SmartLife.Utilities;

public static class FileHelper
{
    internal static IWebHostEnvironment Environment { get; set; }

    public static async Task<string> UploadReplaceFile(string oldFile, IFormFile file, string folder)
    {
        string newFile = await UploadFile(file, folder);

        try
        {
            if (!string.IsNullOrEmpty(newFile))
            {
                string rootedPath = Path.Combine(Environment.WebRootPath, oldFile.TrimStart('/'));
                File.Delete(rootedPath);

                return newFile;
            }
        }
        catch
        {
        }

        return oldFile;
    }

    public static async Task<string> UploadFile(IFormFile file, string folder)
    {
        try
        {
            string rootedPath = Path.Combine(Environment.WebRootPath, folder.TrimStart('/'));
            Directory.CreateDirectory(rootedPath);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(rootedPath, uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return $"/{folder}/{uniqueFileName}";
        }
        catch
        {
            return "";
        }
    }
}
