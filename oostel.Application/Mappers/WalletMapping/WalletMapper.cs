using Mapster;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Domain.UserRoleProfiles.Entities;
using Oostel.Domain.UserWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Mappers.WalletMapping
{
    public class WalletMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Transaction, TransactionDTO>();

        }
    }
}
