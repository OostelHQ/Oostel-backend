using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserWallet;
using Oostel.Domain.UserWallet.Enum;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.Services
{
    public class UserWalletService: IUserWalletService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserWalletService(UnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task UpdateWalletBalance(decimal amount, string userId, TransactionType transactionType)
        {
            var wallet = await _unitOfWork.WalletRepository.Find(x => x.UserId == userId);
            if (wallet is null)
            {
                 wallet = await _unitOfWork.WalletRepository.Add(Wallet.CreateWalletFactory(userId));
                await _unitOfWork.SaveAsync();
            }

            if (transactionType == TransactionType.Credit)
                wallet.AvailableBalance += amount;

            else if (transactionType == TransactionType.Debit)
                wallet.AvailableBalance -= amount;

             await _unitOfWork.WalletRepository.UpdateAsync(wallet);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Wallet> GetUserWallet(string userId)
        {
            var wallet = await _unitOfWork.WalletRepository.Find(x => x.UserId == userId);
            return wallet;
        }


        public async Task<bool> CreateTransaction(TransactionDTO transactionDTO)
        {
            var mapper = _mapper.Map<Transaction>(transactionDTO);
            var transaction = await _unitOfWork.TransactionRepository.Add(mapper);

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ResultResponse<PagedList<Transaction>>> GetTransaction(string userId, TransactionType transactionType, int pageSize, int pageNo)
        {
            var transactionQuery = _unitOfWork.TransactionRepository.FindByCondition(x => x.UserId == userId && x.TransactionType == transactionType, true);

            if (transactionQuery is null)
            {
                return ResultResponse<PagedList<Transaction>>.Failure(ResponseMessages.NotFound);
            }

            return ResultResponse<PagedList<Transaction>>.Success(await PagedList<Transaction>.CreateAsync(transactionQuery, pageNo, pageSize));
        }
      
    }


}
