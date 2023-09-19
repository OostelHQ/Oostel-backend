using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Npgsql.BackendMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Media
{
    public class MediaUpload : IMediaUpload
    {
        private readonly CloudinarySettings cloudinarySettings;
        private readonly Account account;
        private readonly Cloudinary cloudinary;

        public MediaUpload(IOptions<CloudinarySettings> _cloudinarySettings)
        {
            this.cloudinarySettings = _cloudinarySettings.Value;

            account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
                );

            cloudinary = new Cloudinary(account);

        }

        public async Task<PhotoUploadResult> UploadPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return new PhotoUploadResult
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString()
                };
            }

            return null;
        }

        public async Task<List<PhotoUploadResult>> UploadPhotos(List<IFormFile> files)
        {
            var results = new List<PhotoUploadResult>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        throw new Exception(uploadResult.Error.Message);
                    }

                    results.Add(new PhotoUploadResult
                    {
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.SecureUrl.ToString()
                    });
                }
            }

            return results;
        }



        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok" ? result.Result : null;
        }
    }
}
