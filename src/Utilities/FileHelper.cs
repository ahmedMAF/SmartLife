namespace SmartLife.Utilities;

public static class FileHelper
{
    internal static IWebHostEnvironment Environment { get; set; }

    public static void DeleteUploadedFile(string path)
    {
        try
        {
			string rootedPath = Path.Combine(Environment.WebRootPath, path.TrimStart('/'));
            File.Delete(rootedPath);
        }
        catch
        {
        }
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
        catch (Exception)
        {
            return "";
        }
    }
}
