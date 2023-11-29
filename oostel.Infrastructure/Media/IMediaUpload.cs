using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Media
{
    public interface IMediaUpload
    {
        Task<PhotoUploadResult> UploadPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
        Task<List<PhotoUploadResult>> UploadPhotos(List<IFormFile> files);
        Task<PhotoUploadResult> UploadVideo(IFormFile file);
    }
}
