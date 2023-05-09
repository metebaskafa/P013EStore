﻿namespace P013EStore.MVCUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath="/Img/")
        {
            var fileName = " ";
            fileName = formFile.FileName;
            string directory =Directory.GetCurrentDirectory()+"/wwwroot"+filePath+fileName;
            using var stream = new FileStream(directory, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return fileName;
        }
        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Img")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;
            if(File.Exists(directory)) // (File.Exists metodu .net içinde var olan ve kendisine verilen dizinde dosya var mı yok mu kontrol eden metot
            {
                File.Delete(directory); //File.Delete metodu bir dizinde dosya siler
                return true;// dosya silindikten sonra metot geriye true döner
            }
            return false;// yukarıdaki silme kodu çalışmazsa geriye false döner böylece işlem sonucundan haberdar olabiliriz.
        }

    }
}
