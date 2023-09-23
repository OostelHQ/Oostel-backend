using Oostel.Common.Types;
using Oostel.Domain.UserRoleProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class OpenToRoomate : BaseEntity<string>
    {
        public string StudentId { get; set; }
        public string HostelName { get; set; }
        public decimal HostelPrice { get; set; }
        public string HostelAddress { get; set; }
        public Student Student { get; set; }

        public OpenToRoomate()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private OpenToRoomate(string studentId, string hostelName, decimal hostelPrice, string hostelAddress)
        {
            StudentId = studentId;
            HostelName = hostelName;
            HostelPrice = hostelPrice;
            HostelAddress = hostelAddress;
            Id = Guid.NewGuid().ToString();
        }

        public static OpenToRoomate CreateOpenToRoomateFactory(string studentId, string hostelName, decimal hostelPrice, string hostelAddress)
        {
            return new OpenToRoomate(studentId, hostelName, hostelPrice, hostelAddress);
        }
    }
}
