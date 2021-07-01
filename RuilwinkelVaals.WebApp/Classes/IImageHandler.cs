using RuilwinkelVaals.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Classes
{
    public interface IImageHandler
    {
        public string UploadedFile(ImageViewModel model);
    }
}
