using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserWallet.DTOs;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserWallet;
using Oostel.Domain.UserWallet.Enum;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.FlutterwaveIntegration;
using Oostel.Infrastructure.FlutterwaveIntegration.Models;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _userWallet = Oostel.Domain.UserWallet.Wallet;

namespace Oostel.Application.Modules.UserWallet.Services
{
    public class UserWalletService: IUserWalletService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFlutterwaveClient _flutterwaveClient;
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public UserWalletService(UnitOfWork unitOfWork, ApplicationDbContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager,
                            IFlutterwaveClient flutterwaveClient, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _flutterwaveClient = flutterwaveClient;
            _context = dbContext;
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

        public async Task<bool> UpdateUserWalletBalanceWithTransactionHistory(string userId)
        {
            var transactionHistory = await GetUserUnprocessedTransactions(userId);

            if (!transactionHistory.Any())
                return false;

            var totalCredit = transactionHistory.Where(x => x.TransactionType == TransactionType.Credit).Sum(x => x.Amount);
            var totalDebit = transactionHistory.Where(x => x.TransactionType == TransactionType.Debit).Sum(x => x.Amount);
            var difference = totalCredit - totalDebit;

            //get wallet balance
            var wallet = await GetUserWallet(userId);
            var existingBalance = wallet.AvailableBalance;

            //compute new balance
            var newBalance = existingBalance + difference;

            //update transaction status
            foreach (var transaction in transactionHistory)
            {
                transaction.Isprocessed = true;
                await UpdateTransaction(transaction);
            }

            //update wallet balance
            var updatedWallet = new _userWallet
            {
                Id = wallet.Id,
                AvailableBalance = newBalance,
                UserId = userId
            };

            await UpdateUserWallet(updatedWallet);
            return true;
        }

        public async Task<decimal> GetTotalUserWalletBalance()
        {
            var getUserWallet = await _unitOfWork.WalletRepository.GetAll(false);
            decimal totalAvailable = getUserWallet.Sum(wallet => wallet.AvailableBalance);

            return totalAvailable;
        }

        public async Task UpdateUserWallet(_userWallet transaction)
        {
            await _unitOfWork.WalletRepository.UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task<decimal> GetSumOfUserAvailableBalance(string userId)
        {
            var userWallet = await GetUserWallet(userId);
            return userWallet.AvailableBalance;
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Transaction>> GetUserUnprocessedTransactions(string userId)
        {
            var getTransaction = await _context.Transactions
                                .Where(x => x.UserId == userId && x.Isprocessed == false)
                                .OrderBy(x => x.CreatedDate)
                                .ToListAsync();

            return getTransaction;
        }

        public async Task<Wallet> GetUserWallet(string userId)
        {
            var wallet = await _unitOfWork.WalletRepository.Find(x => x.UserId == userId);
            return wallet;
        }


        public async Task<bool> CreateTransaction(string senderId, string title, decimal amount, string lastname, TransactionType type)
        {
            Transaction transaction = new Transaction
            {
                UserId = senderId,
                Title = title,
                TransactionType = type,
                CreatedDate = DateTime.UtcNow,
                Amount = amount,
                FromLastname = lastname,
                LastModifiedDate = DateTime.UtcNow
            };
             await _unitOfWork.TransactionRepository.Add(transaction);

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

        public async Task<PayInAndOutHistory> CreateAndUpdatePayInHistory(PayInAndOutHistory payInHistory)
        {
            if (string.IsNullOrEmpty(payInHistory.Id))
            {
                await _unitOfWork.PayInHistoryRepository.Add(payInHistory);
            }
            else
            {
                await _unitOfWork.PayInHistoryRepository.UpdateAsync(payInHistory);
            }

            await _unitOfWork.SaveAsync();

            return payInHistory;
        }

        public async Task<PayInAndOutHistory> GetPayInHistoryById(string transactionId)
        {
            var payInHistory = await _unitOfWork.PayInHistoryRepository.GetById(transactionId);
            if (payInHistory is null)
                return null;

            return payInHistory;
        }

        public async Task<ResultResponse<PagedList<PayInAndOutHistory>>> GetPayInHistories(int pageNo, int pageSize)
        {
            var payInHistory = _unitOfWork.PayInHistoryRepository.FindByCondition(x => true, false);
            if (payInHistory is null)
                return ResultResponse<PagedList<PayInAndOutHistory>>.Failure(ResponseMessages.NotFound);

            return ResultResponse<PagedList<PayInAndOutHistory>>.Success(await PagedList<PayInAndOutHistory>.CreateAsync(payInHistory, pageNo, pageSize));
        }

        public async Task<List<FLBankModel>> GetNigeriaBanks()
        {
            var allNigeriaBanks = await _flutterwaveClient.GetBanks();
            List<FLBankModel> listOfBanksToReturn = new();
            foreach (var bank in allNigeriaBanks.Banks)
            {
                var banks = new FLBankModel()
                {
                    Id = bank.id.ToString(),
                    BankCode = bank.code,
                    BankName = bank.name
                };

                listOfBanksToReturn.Add(banks);
            }

            return listOfBanksToReturn;
        }

        public async Task<BasePaymentResponse> GeneratePaymentDetails(CustomerPaymentInfo customerPaymentInfo)
        {
            var paymentRedirectUrl = _appSettings.WebhookUrl;

            var payload = new GeneratePaymentRequest
            {
                TransactionReference = customerPaymentInfo.PaymentData.ReferenceNumber,
                Amount = customerPaymentInfo.PaymentData.Amount,
                Currency = customerPaymentInfo.PaymentData.Currency,
                RedirectURL = paymentRedirectUrl,
                Customer = new PaymentRequestCustomer()
                {
                    Email = customerPaymentInfo.Email,
                    PhoneNumber = customerPaymentInfo.PhoneNumber,
                    Name = string.Concat(customerPaymentInfo.FirstName, "", customerPaymentInfo.LastName)

                },
                PaymentOptionsCSV = null,
                PaymentPlan = null
            };

            var result = await _flutterwaveClient.GeneratePaymentLink(payload);
            if (result != null)
            {
                var response = BasePaymentResponse.GetSuccessMessage("Success : Use the link to make payment");
                var dataToReturn = new PaymentGenerationData
                {
                    PaymentLink = result.Data.Link,
                    Id = "nil"
                };
                response.PaymentProvider = "FlutterWave-Rave";
                response.PaymentGenerationData = dataToReturn;
                return response;
            }
            else
            {
                return BasePaymentResponse.GetFailureMessage("Failed : something went wrong");
            }
        }

        public async Task<BasePaymentResponse> VerifyTransactionPayment(VerifyRequestModel verifyRequestModel)
        {
            var payload = new VerifyTransactionRequest
            {
                TransactionId = verifyRequestModel.TransactionId
            };

            var result = await _flutterwaveClient.VerifyTransactionPayment(payload);
            if (result != null)
            {
                var response = BasePaymentResponse.GetSuccessMessage("Verification Successful");

                var verificationDataToReturn = new VerificationResponse
                {
                    Status = result.data.status,
                    Id = result.data.id.ToString(),
                    Amount = result.data.amount,
                    ChargedAmount = result.data.charged_amount,
                    Currency = result.data.currency,
                    ProcessorResponse = result.data.processor_response,
                };
                response.VerificationData = verificationDataToReturn;
                return response;
            }
            else
            {
                return BasePaymentResponse.GetFailureMessage("Failed : something went wrong");
            }
        }

        public string GenerateReferenceNumber()
        {
            var prefix = "Fynda-Ref-";
            var timestamp = DateTime.UtcNow.Ticks;
            var randomAlphabets = RandomCodeGenerator.GenerateAlphabet().Substring(0, 4);
            return string.Concat(prefix, "-", timestamp.ToString(), "-", randomAlphabets);
        }


    }


}
