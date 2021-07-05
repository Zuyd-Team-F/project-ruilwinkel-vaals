using Microsoft.AspNetCore.Hosting;
using RuilwinkelVaals.WebApp.Classes.Services;
using RuilwinkelVaals.WebApp.Data.Models.Interfaces;
using RuilwinkelVaals.WebApp.ViewModels.Interfaces;
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

        public string UploadedFile(IImageViewModel model, Constants.ImageModels type)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                var folder = _storage.FirstOrDefault(f => 
                f.Name.Equals(
                    type.ToString()
                        .ToLower()
                    )
                ).FullName;

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public void RemoveFile(IImageModel model, Constants.ImageModels type)
        {            
            if(model.Image != "default.png")
            {
                var folder = _storage.FirstOrDefault(f =>
                f.Name.Equals(
                    type.ToString()
                        .ToLower()
                    )
                );

                try
                {
                    folder.GetFiles().FirstOrDefault(f => f.Name == model.Image).Delete();
                }
                catch
                {
                    // Log that the image hasn't been found,
                    // does not impact deletion.
                }
            }
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
    }
}
