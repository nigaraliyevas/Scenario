namespace Scenario.Application.Helpers.Helper;

public class Helper
{
    public static void DeleteImageFromFolder(string fileName, string folderName)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folderName, fileName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}
