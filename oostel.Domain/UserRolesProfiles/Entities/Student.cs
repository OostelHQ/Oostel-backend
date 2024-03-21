using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using Oostel.Domain.UserWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserRoleProfiles.Entities
{
    public class Student : BaseEntity<string>
    {
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public string SchoolLevel { get; set; }
        public string Country { get; set; }
        public string Religion { get; set; }
        public bool IsAvailable { get; set; } = false;
        public string Age { get; set; }
        public string? Area { get; set; }
        public string Denomination { get; set; }
        public string Hobby { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public virtual OpenToRoommate OpenToRoomate { get; set; }
        public ICollection<StudentLikes> LikedUsers { get; set; }
        public virtual ApplicationUser User { get; set; }
      //  public virtual Wallet Wallet { get; set; }

        public Student(string userId, string stateOfOrigin, string gender, string schoolLevel, string country, string religion,
            string age, string area, string donomination, string hobby, string guardianPhoneNumber) : this()
        {
            Id = userId;
            StateOfOrigin = stateOfOrigin;
            Gender = gender;
            SchoolLevel = schoolLevel;
            Country = country;
            Religion = religion;
            Age = age;
            Area = area;
            Denomination = donomination;
            Hobby = hobby;
            GuardianPhoneNumber = guardianPhoneNumber;
          //  SetDefaultWallet();
        }

        public Student()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            IsAvailable = false;
        }

       public static Student CreateStudentProfileFactory(string userId, string stateOfOrigin, string gender, string schoolLevel, string country, string religion,
            string age, string area, string donomination, string hobby, string guardianPhoneNumber)
        {
            return new Student(userId, stateOfOrigin, gender, schoolLevel, country, religion, age, area, donomination, hobby, guardianPhoneNumber);
        }

       /* public void SetDefaultWallet()
        {
            var wallet = Wallet.CreateWalletFactory(Id);
            this.Wallet = wallet;
        }*/
    }
}

