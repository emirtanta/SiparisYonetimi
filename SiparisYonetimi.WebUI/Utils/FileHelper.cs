namespace SiparisYonetimi.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile,string filePath="/wwwroot/Img/")
        {
            var fileName = "";

            fileName = formFile.FileName;
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;

            using var stream = new FileStream(directory, FileMode.Create);

            await formFile.CopyToAsync(stream);

            return fileName;
        }

        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Img/")
        {
            string directory=Directory.GetCurrentDirectory() + filePath+fileName;

            //Exist metodu verilen adreste dosy olup olmadığını kontrol eder
            if (File.Exists(directory))
            {
                File.Delete(directory);//dosyayı sunucudan siler

                return true;
            }

            return false;
        }

    }
}
