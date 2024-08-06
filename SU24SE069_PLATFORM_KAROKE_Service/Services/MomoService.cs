using AutoMapper.Execution;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Momo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoMo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MonetaryTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class MomoService : IMomoService
    {
        private readonly MomoOptionModel _moMoOptions;
        private readonly IMonetaryTransactionService _monetaryTransactionService;
        private readonly IPackageRepository _packageRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInAppTransactionRepository _inAppTransactionRepository;
        private readonly IMonetaryTransactionRepository _monetaryTransactionRepository;

        public MomoService(IOptions<MomoOptionModel> options,
            IMonetaryTransactionService monetaryTransactionService,
            IPackageRepository packageRepository,
            IAccountRepository accountRepository,
            IInAppTransactionRepository inAppTransactionRepository,
            IMonetaryTransactionRepository monetaryTransactionRepository)
        {
            _moMoOptions = options.Value;
            _monetaryTransactionService = monetaryTransactionService;
            _packageRepository = packageRepository;
            _accountRepository = accountRepository;
            _inAppTransactionRepository = inAppTransactionRepository;
            _monetaryTransactionRepository = monetaryTransactionRepository;
        }

        public async Task<MoMoCreatePaymentResponse?> CreatePaymentAsync(MonetaryTransactionRequestModel transactionRequest)
        {
            // Create monetary transaction
            var transactionResult = await _monetaryTransactionService.CreateTransaction(transactionRequest);
            if (transactionResult == null || transactionResult.Value == null)
            {
                return null;
            }
            var transaction = transactionResult.Value;

            // Create MoMo payment request
            string orderInfo = $"Người dùng '{transaction.MemberId}' nạp UP vào tài khoản.";
            CreateMoMoPaymentRequest paymentRequest = new CreateMoMoPaymentRequest();
            paymentRequest.SetMoMoOptions(_moMoOptions);

            MoMoExtraData extraData = new MoMoExtraData()
            {
                PackageId = transaction.PackageId.ToString(),
                AccountId = transaction.MemberId.ToString(),
            };

            var extraDataString = HashHelper.EncodeToBase64(JsonConvert.SerializeObject(extraData));

            paymentRequest.SetTransactionData(transaction.MonetaryTransactionId.ToString(), (long)transaction.PackageMoneyAmount, transaction.MonetaryTransactionId.ToString(), orderInfo, extraDataString);
            paymentRequest.MakeSignature(_moMoOptions.AccessKey, _moMoOptions.SecretKey);
            (bool result, string? paymentData) = paymentRequest.GetPaymentMethod(_moMoOptions.PaymentUrl);
            if (!result)
            {
                return null;
                //throw new Exception($"Failed to retrieve MoMo payment method for transaction '{transaction.MonetaryTransactionId.ToString()}'");
            }
            return JsonConvert.DeserializeObject<MoMoCreatePaymentResponse>(paymentData!);
        }

        public async Task ProcessMoMoIpnRequest(MoMoIpnRequest ipnRequest)
        {
            // Transaction canceled by user
            if (ipnRequest.resultCode == 1006)
            {
                await _monetaryTransactionService.UpdateStatusTransaction(Guid.Parse(ipnRequest.orderId), PaymentStatus.CANCELLED);
                return;
            }

            // Transaction failed because of insufficient user's balance
            if (ipnRequest.resultCode == 1001)
            {
                await _monetaryTransactionService.UpdateStatusTransaction(Guid.Parse(ipnRequest.orderId), PaymentStatus.CANCELLED);
                return;
            }

            // Transaction process successful
            if (ipnRequest.resultCode == 0 || ipnRequest.resultCode == 9000)
            {
                // Change monetary transaction status to success and set transId
                var monetaryTransaction = await _monetaryTransactionRepository.GetByIdGuid(Guid.Parse(ipnRequest.orderId));
                monetaryTransaction.Status = (int)PaymentStatus.COMPLETE;
                monetaryTransaction.PaymentCode = ipnRequest.transId.ToString();
                await _monetaryTransactionRepository.SaveChagesAsync();
                //await _monetaryTransactionService.UpdateStatusTransaction(Guid.Parse(ipnRequest.orderId), PaymentStatus.COMPLETE);

                // Get package data
                var extraData = JsonConvert.DeserializeObject<MoMoExtraData>(HashHelper.DecodeFromBase64(ipnRequest.extraData));
                var upPackage = await _packageRepository.GetByIdGuid(Guid.Parse(extraData.PackageId));

                // Fetch user information
                var account = await _accountRepository.GetByIdGuid(Guid.Parse(extraData.AccountId));

                // Create in-app transaction
                var inAppTransaction = new InAppTransaction()
                {
                    CreatedDate = DateTime.Now,
                    MemberId = account.AccountId,
                    SongId = null,
                    ItemId = null,
                    MonetaryTransactionId = Guid.Parse(ipnRequest.orderId),
                    Status = (int)PaymentStatus.COMPLETE,
                    UpAmountBefore = account.UpBalance,
                    UpTotalAmount = upPackage.StarNumber,
                    TransactionType = (int)InAppTransactionType.RECHARGE_UP_BALANCE,
                };

                var addInAppTransaction = await _inAppTransactionRepository.CreateInAppTransaction(inAppTransaction);

                if (!addInAppTransaction)
                {
                    // Failed to add in-app transaction
                    return;
                }
                // Update user's UP balance
                account.UpBalance += upPackage.StarNumber;
                await _accountRepository.SaveChagesAsync();
            }
        }
    }
}
