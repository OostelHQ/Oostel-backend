using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Hostel.Entities
{
    public class HostelPictures : BaseEntity<string>
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
        public string HostelId { get; set; }
        public Hostel Hostel { get; set; }

        public HostelPictures()
        {
            
        }

        public HostelPictures(string url, string publicId, string hostelId) : base(Guid.NewGuid().ToString())
        {
            Url = url;
            PublicId = publicId;
            HostelId = hostelId;
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public static HostelPictures CreateHostelOrRoomPicturesFactory(string url, string publicId, string hostelId)
        {
            return new HostelPictures(url, publicId, hostelId);
        }
    }
}
