using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserRolesProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserWallet
{
    public class Wallet : BaseEntity<string>
    {
        public string? UserId { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastTransactionDate { get; set; } = DateTime.Now;
        public Student? Student { get; set; } = null;

        public Landlord? Landlord { get; set; } = null;
       // public ApplicationUser User { get; set; }

        private Wallet(string userId, decimal availableBalance, Student student, Landlord landlord) : base(userId)
        {
            UserId = userId;
            AvailableBalance = availableBalance;
            LastTransactionDate = DateTime.UtcNow;
            Student = student;
            Landlord= landlord;
        }

        private Wallet(string userId, decimal availableBalance) : base(userId)
        {
            UserId = userId;
            AvailableBalance = availableBalance;
            LastTransactionDate = DateTime.UtcNow;
        }

        public Wallet()
        {

        }

        public static Wallet CreateWalletFactory(string UserId)
        {
            var wallet = new Wallet(UserId, 0.0M);
            return wallet;
        }

        public static Wallet CreateWalletFactoryForStudent(string UserId, Student student, Landlord landlord)
        {
            var wallet = new Wallet(UserId, 0.0M, student, null);
            return wallet;
        }

        public static Wallet CreateWalletFactoryForLandlord(string UserId, Student student, Landlord landlord)
        {
            var wallet = new Wallet(UserId, 0.0M, null, landlord);
            return wallet;
        }
    }
}
