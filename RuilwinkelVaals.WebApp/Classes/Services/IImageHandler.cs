﻿using RuilwinkelVaals.WebApp.Data.Models.Interfaces;
using RuilwinkelVaals.WebApp.ViewModels;
using RuilwinkelVaals.WebApp.ViewModels.Interfaces;
using System.IO;

namespace RuilwinkelVaals.WebApp.Classes.Services
{
    public interface IImageHandler
    {
        // Summary:
        //     Uploads the image file contained in the model.
        //
        // Parameters:
        //   model:
        //     A model that's contracted with the IImageViewModel interface.
        //
        // Returns:
        //     A unique image id, linked to the stored image.
        string UploadedFile(IImageViewModel model, Constants.ImageModels type);
        // Summary:
        //     Removes the image file from storage.
        //
        // Parameters:
        //   model:
        //     A model that's contracted with the IImageModel interface, containing
        //     the image id.
        void RemoveFile(IImageModel model, Constants.ImageModels type);
        // Summary:
        //     Removes the image file from storage.
        //
        // Parameters:
        //   folders:
        //     An enumerable collection of DirectoryInfo objects, containing the path
        //     to the img storage.
        void DisposeImages(DirectoryInfo[] folders);
    }
}
