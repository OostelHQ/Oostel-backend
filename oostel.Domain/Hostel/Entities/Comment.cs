using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class Comment : BaseEntity<string>
    {
        public string UserComment { get; set; }
        public int rating { get; set; }
        public Hostel Hostel { get; set; }
        public ApplicationUser Author { get; set; }

        public Comment(string id):base(id)
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
