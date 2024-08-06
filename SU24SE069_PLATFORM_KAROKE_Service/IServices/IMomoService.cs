using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Momo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoMo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MonetaryTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IMomoService
    {
        public Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MonetaryTransactionViewModel model);
        MonetaryTransactionViewModel PaymentExecuteAsync(IQueryCollection collection);
        //public Task<ResponseResult<MonetaryTransactionViewModel>> PaymentExecuteAsync(IQueryCollection collection);
        public Task<ResponseResult<MonetaryTransactionViewModel>> PaymentNotifyAsync(MoMoIpnRequest moMoIpnRequest);
    }
}
