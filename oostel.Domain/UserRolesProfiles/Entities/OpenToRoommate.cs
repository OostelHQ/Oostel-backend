using Oostel.Common.Types;
using Oostel.Domain.UserRoleProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRolesProfiles.Entities
{
    public class OpenToRoommate : BaseEntity<string>
    {
        public string StudentId { get; set; }
        public string HostelName { get; set; }
        public decimal RoomBudgetAmount { get; set; }
        public string HostelAddress { get; set; }
        public Student Student { get; set; }

        public OpenToRoommate()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        private OpenToRoommate(string studentId, string hostelName, decimal hostelPrice, string roomBudgetAmount)
        {
            StudentId = studentId;
            HostelName = hostelName;
            RoomBudgetAmount = hostelPrice;
            HostelAddress = roomBudgetAmount;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public static OpenToRoommate CreateOpenToRoomateFactory(string studentId, string hostelName, decimal roomBudgetAmount, string hostelAddress)
        {
            return new OpenToRoommate(studentId, hostelName, roomBudgetAmount, hostelAddress);
        }
    }
}
