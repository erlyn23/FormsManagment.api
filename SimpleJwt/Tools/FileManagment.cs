using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Tools
{
    public class FileManagment
    {
        public async Task<string> UploadImageAsync(string userFullName, string photo64)
        {
            string photoPath = Path.Combine(Environment.CurrentDirectory, $"Photos");
            string[] imageSplitted = photo64.Split(',');
            byte[] imageInBytes = Convert.FromBase64String(imageSplitted[1]);

            Random random = new Random();

            string fileName = $"{userFullName}_{random.Next(1, 3000)}.jpg";
            string path = Path.Combine(photoPath, fileName);

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(imageInBytes);
                await stream.FlushAsync();
            }
            return path;
        }
    }
}
