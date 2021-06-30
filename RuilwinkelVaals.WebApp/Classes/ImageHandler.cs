using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RuilwinkelVaals.WebApp.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace RuilwinkelVaals.WebApp.Classes
{
    public class ImageHandler : IImageHandler
    {
        private readonly DirectoryInfo[] _storage;
        private readonly IWebHostEnvironment _env;

        public ImageHandler(IWebHostEnvironment environment) 
        {
            _env = environment;

            var location = Path.Combine(_env.WebRootPath, "img/storage");
            var folders = new DirectoryInfo(location).GetDirectories();

            _storage = new DirectoryInfo[folders.Length];

            for(int i = 0; i < folders.Length; i++)
            {
                _storage[i] = folders[i];
            }
        }

        public string UploadedFile(ImageViewModel model)
        {
            string uniqueFileName = null;

            // Fetches the namespace of the entity
            // which always yields the correct
            // name of the entity in question
            var nameSpace = model.GetType().Namespace.Split('.');
            var entityName = nameSpace[nameSpace.Length - 1].ToLower();

            if (model.Image != null)
            {
                var folder = _storage.Where(s => s.Name.Contains(entityName)).FirstOrDefault().FullName;

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
