using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Oostel.Common.Exceptions;
using Oostel.Infrastructure.FlutterwaveIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.FlutterwaveIntegration
{
    public class FlutterwaveClient: IFlutterwaveClient
    {
        private readonly HttpClient _httpClient;
        private AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public FlutterwaveClient(HttpClient httpClient, IOptions<AppSettings> options, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _appSettings = options.Value;
            _configuration = configuration;
        }


        public async Task<GetBanksResponse> GetBanks()
        {
            string secretKey = _configuration.GetValue<string>("SecretKey");
            string url = _appSettings.BaseUrl + "banks/NG";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadHttpRequestException("The Request Payload isInvalid !");
                }
                throw new PaymentGatewayException("Endpoint Operation Failed", response.StatusCode);
            }

            var result = await response.Content.ReadAsStringAsync();
            var IncomingDataResponse = JsonConvert.DeserializeObject<GetBanksResponse>(result);
            return IncomingDataResponse;
        }

        public async Task<BankTransferResponseData> ProcessTransfer(BankTransferRequest transferRequest)
        {
            string secretKey = _configuration.GetValue<string>("SecretKey");
            string url = _appSettings.BaseUrl + "transfers";

            var requestData = new BankTransferRequest()
            {
                AccountBankCode = transferRequest.AccountBankCode,
                AccountNumber = transferRequest.AccountNumber,
                Amount = transferRequest.Amount,
                DebitCurrency = transferRequest.DebitCurrency,
                CallbackUrl = transferRequest.CallbackUrl,
                Currency = transferRequest.Currency,
                Narration = transferRequest.Narration,
                Reference = transferRequest.Reference
            };

            var json = JsonConvert.SerializeObject(requestData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            var response = await _httpClient.PostAsync(url, data);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadHttpRequestException("The Request Payload isInvalid !");
                }
                throw new PaymentGatewayException("Endpoint Operation Failed", response.StatusCode);
            }

            var result = await response.Content.ReadAsStringAsync();
            var IncomingDataResponse = JsonConvert.DeserializeObject<BankTransferResponseData>(result);
            return IncomingDataResponse;
        }


        public async Task<GeneratePaymentResponse> GeneratePaymentLink(GeneratePaymentRequest generatePaymentRequest)
        {
            string secretKey = _configuration.GetValue<string>("SecretKey");
            string url = _appSettings.BaseUrl + "payments";

            var requestData = new GeneratePaymentRequest()
            {
                Amount = generatePaymentRequest.Amount,
                Currency= generatePaymentRequest.Currency,
                Customer =generatePaymentRequest.Customer, //issue may come up here.
                PaymentOptionsCSV = generatePaymentRequest?.PaymentOptionsCSV,
                PaymentPlan= generatePaymentRequest?.PaymentPlan,
                RedirectURL = generatePaymentRequest.RedirectURL,
                TransactionReference = generatePaymentRequest.TransactionReference
            };

            var json = JsonConvert.SerializeObject(requestData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            var response = await _httpClient.PostAsync(url, data);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadHttpRequestException("The Request Payload isInvalid !");
                }
                throw new PaymentGatewayException("Endpoint Operation Failed", response.StatusCode);
            }

            var result = await response.Content.ReadAsStringAsync();
            var IncomingDataResponse = JsonConvert.DeserializeObject<GeneratePaymentResponse>(result);
            return IncomingDataResponse;
        }

        public async Task<VerifyTransactionResponse> VerifyTransactionPayment(VerifyTransactionRequest verifyTransactionRequest)
        {
            string secretKey = _configuration.GetValue<string>("SecretKey");
            string url = _appSettings.BaseUrl + $"transactions/{verifyTransactionRequest.TransactionId}/verify";

            //  var json = JsonConvert.SerializeObject(countryIso2Code);
            //  var data = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadHttpRequestException("The Request Payload isInvalid !");
                }
                throw new PaymentGatewayException("Endpoint Operation Failed", response.StatusCode);
            }

            var result = await response.Content.ReadAsStringAsync();
            var IncomingDataResponse = JsonConvert.DeserializeObject<VerifyTransactionResponse>(result);
            return IncomingDataResponse;
        }
    }
}
