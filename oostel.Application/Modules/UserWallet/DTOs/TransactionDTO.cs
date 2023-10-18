using Oostel.Domain.UserWallet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class TransactionDTO
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string FromLastname { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
