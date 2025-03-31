
using Microsoft.AspNetCore.Http;


namespace Scenario.Application.Extensions.Extension;

public static class Extension
{
    public static bool CheckContentType(this string file, string fileType)
    {
        if (string.IsNullOrEmpty(file))
            return false;
        var prefix = $"data:{fileType}/";
        return file.StartsWith(prefix);
    }
    public static bool CheckSize(this string file, int size)
    {
        //return file.Length / 1024 > size;
        if (string.IsNullOrEmpty(file))
            return false;

        // Remove the prefix "data:image/{fileType};base64,"
        var base64Data = file.Substring(file.IndexOf(',') + 1);

        // Convert the base64 string to byte array
        byte[] imageBytes = Convert.FromBase64String(base64Data);

        // Check if the size exceeds the specified limit
        return (imageBytes.Length / 1024) > size;
    }
    public static async Task<string> SaveFile(this string file, string folderName, IHttpContextAccessor _httpContextAccessor)
    {
        var uriBuilder = new UriBuilder(
            _httpContextAccessor.HttpContext.Request.Scheme,
            _httpContextAccessor.HttpContext.Request.Host.Host,
            _httpContextAccessor.HttpContext.Request.Host.Port.GetValueOrDefault());
        var uri = uriBuilder.Uri.AbsoluteUri;

        if (file.Contains(","))
        {
            file = file.Substring(file.IndexOf(',') + 1);
        }

        byte[] imageBytes = Convert.FromBase64String(file);

        string fileName = Guid.NewGuid() + GetExtensionFromMime(GetMimeType(imageBytes));
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folderName, fileName);

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folderName)))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folderName));
        }

        // Save the file
        await File.WriteAllBytesAsync(path, imageBytes);

        return $"{uri}img/{folderName}/{fileName}";
    }
    public static string GetExtensionFromMime(string mimeType)
    {
        return mimeType switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            _ => ".bin" // Default for unknown types
        };
    }
    public static string GetMimeType(byte[] byteArray)
    {
        if (byteArray == null || byteArray.Length == 0)
        {
            throw new ArgumentException("The byte array is null or empty.");
        }

        try
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
                //using (var image = Image.FromStream(stream)) // Ensure this uses System.Drawing.Image
                //{
                //    //var codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == image.RawFormat.Guid);
                //    //return codec?.MimeType ?? "application/octet-stream"; // Fallback if codec is not found
                //    return "";
                //}
                return "";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get MIME type from byte array.", ex);
        }
    }




}
