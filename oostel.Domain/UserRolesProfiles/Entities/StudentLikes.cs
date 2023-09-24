using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class StudentLikes : BaseEntity<string>
    {
        public string SourceUserId { get; set; }
        public ApplicationUser SourceUser { get; set; }
        public string LikedStudentId { get; set; }
        public Student LikedStudent { get; set; }

        public StudentLikes()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private StudentLikes(string sourceUserId, string likedStudentId)
        {
            SourceUserId = sourceUserId;
            LikedStudentId = likedStudentId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public static StudentLikes CreateStudentLikesFactory(string sourceUserId, string likedStudentId)
        {
            return new StudentLikes(sourceUserId, likedStudentId);
        }
    }
}
