using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Momo;
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
        private readonly IOptions<MomoOptionModel> _options;
        private readonly IMonetaryTransactionService _monetaryTransactionService;

        public MomoService(IOptions<MomoOptionModel> options, IMonetaryTransactionService monetaryTransactionService)
        {
            _options = options;
            _monetaryTransactionService = monetaryTransactionService;
        }

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MonetaryTransaction model)
        {
            //model.MonetaryTransactionId = new Guid();
            //model.OrderInfo = "Khách hàng: " + model.FullName + ". Nội dung: " + model.OrderInfo;
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.MonetaryTransactionId}&amount={model.MoneyAmount}&orderId={model.MonetaryTransactionId}&orderInfo={model.MonetaryTransactionId}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            /*var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.MonetaryTransactionId, 
                amount = model.MoneyAmount.ToString(),
                //orderInfo = model.OrderInfo, //modify rawdata
                requestId = model.MonetaryTransactionId,
                extraData = "",
                packageId = model.PackageId,
                memberId = model.MemberId,
                signature = signature
            };*/

            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.MonetaryTransactionId,
                amount = model.MoneyAmount.ToString(),
                orderInfo = model.MonetaryTransactionId,
                requestId = model.MonetaryTransactionId,
                extraData = "",
                signature = signature
            };           

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        }

        public MonetaryTransactionViewModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            //var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var localMessage = collection.First(s => s.Key == "localMessage").Value;
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
            if(errorCode != 0)
            {
                // transaction failed
            }

            /*            var packageId = collection.First(s => s.Key == "packageId").Value;
                        var memberId = collection.First(s => s.Key == "memberId").Value;*/
            return null;

            /*return new MonetaryTransactionViewModel()
            {
                MoneyAmount = decimal.Parse(amount),
                PaymentType = PaymentType.MOMO,

                Currency = "VND",
                Status = PaymentStatus.COMPLETE,
                CreatedDate = DateTime.Now,
                PackageId = Guid.Parse(packageId),
                MemberId = Guid.Parse(memberId)
            };*/

        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public async Task<MonetaryTransactionViewModel> PaymentNotifyAsync(IQueryCollection collection)
        {
            var orderId = Guid.Parse(collection.First(s => s.Key == "orderId").Value);
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
            if (errorCode != 0)
            {
                // transaction failed
                var update = await _monetaryTransactionService.UpdateStatusTransaction(orderId, PaymentStatus.CANCELLED);
            }
            else
            {
                // transaction success
                var update = await _monetaryTransactionService.UpdateStatusTransaction(orderId, PaymentStatus.COMPLETE);
            }
            return null; 
        }
    }
}
