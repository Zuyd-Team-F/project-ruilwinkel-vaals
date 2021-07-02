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

        public ImageHandler(IWebHostEnvironment environment) 
        {
            var location = Path.Combine(environment.WebRootPath, "img/storage");
            var folders = new DirectoryInfo(location).GetDirectories();

            _storage = folders;
        }

        public string UploadedFile(IImageViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                var folder = GetEntityStorageFolderPath(model);

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public void RemoveFile(IImageModel model)
        {
            DirectoryInfo folder = new(GetEntityStorageFolderPath(model));
            folder.GetFiles().FirstOrDefault(f => f.Name == model.Image).Delete();
        }

        public void DisposeImages(DirectoryInfo[] folders)
        {
            foreach (var folder in folders)
            {
                foreach (var file in folder.GetFiles())
                {
                    if (file.Name != "default.png")
                    {
                        file.Delete();
                    }
                }
            }
        }

        public string GetEntityStorageFolderPath(object model)
        {
            // Fetches the namespace of the entity
            // which always yields the correct
            // name of the entity in question
            var nameSpace = model.GetType().Namespace.Split('.');
            var entityName = nameSpace[nameSpace.Length - 1].ToLower();
            return _storage.FirstOrDefault(s => s.Name.Contains(entityName)).FullName;
        }
    }
}
