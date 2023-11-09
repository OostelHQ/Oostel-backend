using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserWallet.DTOs
{
    public class CustomerPaymentInfo
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryIso2Code { get; set; }

        public string? Platform { get; set; }
        public PaymentInfo? PaymentData { get; set; }
        public MobileMoneyInfo? MobileMoneyData { get; set; }
        public IdentityInfo? IdentityData { get; set; }
        public BankInfo? CustomerBankData { get; set; }
    }

    public class PaymentInfo
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        // To identity the transaction from our own Side
        public string ReferenceNumber { get; set; }
    }

    public class MobileMoneyInfo
    {
        public string PhoneNumber { get; set; }
        public string OperatorCode { get; set; }
    }

    public class BankInfo
    {
        public string? BankName { get; set; }
        public string? BankCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountCurrency { get; set; }

        public string? RoutingNumber { get; set; }
        public string SwiftCode { get; set; }
    }

    public class IdentityInfo
    {
        public string? IdCardNumber { get; set; }
        public string? IdCardType { get; set; }
        public string? IdCardExpiry { get; set; }
        public string? IdCardIssuer { get; set; }
    }
}
