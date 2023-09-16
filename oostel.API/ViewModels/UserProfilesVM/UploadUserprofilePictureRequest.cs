namespace Oostel.API.ViewModels.UserProfilesVM
{
    public class UploadUserprofilePictureRequest
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }
    }
}
