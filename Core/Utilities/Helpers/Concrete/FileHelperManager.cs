using Core.Utilities.Business.Concrete;
using Core.Utilities.Helpers.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.Concrete
{
    public class FileHelperManager : IFileHelper
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG",".jpg",
                                                                                 ".jpeg",".jpe",".bmp",".gif",".png" };
        public IResult Upload(IFormFile file, string root)
        {
            IResult result = BusinessRules.Run(CheckIfAFileSent(file),
                CheckIfFileIsAnImage(file));
            if (result != null)
            {
                return result;
            }

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string fileName = CreateFile(root, file);
            return new SuccessResult(fileName);
        }

        public IResult Update(IFormFile file, string filePath, string root)
        {
            var result = DeleteFile(filePath);
            if (result.Success)
            {
                return Upload(file, root);
            }
            return result;
        }

        public IResult Delete(string filePath)
        {
            return DeleteFile(filePath);
        }

        private string CreateFile(string root, IFormFile file)
        {
            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(file.FileName);
            var fileName = guid + extension;

            using (FileStream fileStream = File.Create(root + fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return fileName;
        }

        private IResult DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return new SuccessResult("Dosya silindi");
            }
            return new ErrorResult("Belirtilen dosya bulunamadı");
        }

        private static IResult CheckIfFileIsAnImage(IFormFile file)
        {
            if (ImageExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                return new SuccessResult();
            }
            return new ErrorResult("Hatalı dosya uzantısı");
        }

        private static IResult CheckIfAFileSent(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Bozuk dosya");
        }
    }
}
