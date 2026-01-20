using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Helper
{
    public class FileUploadHelper
    {
        //private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        //public static string SaveImage(HttpPostedFile file, string type, string oldFileName = null)
        //{
        //    if (file == null || file.ContentLength == 0)
        //        return oldFileName;

        //    string folderRelativePath = GetFolderPath(type);
        //    if (string.IsNullOrEmpty(folderRelativePath))
        //        throw new InvalidOperationException("نوع الملف غير معروف.");

        //    string extension = Path.GetExtension(file.FileName).ToLower();
        //    if (!AllowedExtensions.Contains(extension))
        //        throw new InvalidOperationException("نوع الملف غير مدعوم. الامتدادات المسموح بها: jpg, jpeg, png, gif,mp4");

        //    string newFileName = Guid.NewGuid() + extension;
        //    string fullFolderPath = HttpContext.Current.Server.MapPath(folderRelativePath);

        //    if (!Directory.Exists(fullFolderPath))
        //        Directory.CreateDirectory(fullFolderPath);

        //    string newFilePath = Path.Combine(fullFolderPath, newFileName);

        //    if (!string.IsNullOrEmpty(oldFileName))
        //    {
        //        string oldFilePath = Path.Combine(fullFolderPath, oldFileName);
        //        if (File.Exists(oldFilePath))
        //        {
        //            File.Delete(oldFilePath);
        //        }
        //    }

        //    file.SaveAs(newFilePath);
        //    return newFileName;
        //}
        //private static string GetFolderPath(string type)
        //{
        //    switch (type)
        //    {
        //        case "PersonProfile": return "~/Uploads/PersonsImage/";
        //        default: return null;
        //    }
        //}
    }
}
