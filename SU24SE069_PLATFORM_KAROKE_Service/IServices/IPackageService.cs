using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.PayOS;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPackageService
    {
        public Task<ResponseResult<PackageViewModel>> CreatePackage(PackageRequestModel request);
        public Task<ResponseResult<PackageViewModel>> UpdatePackage(PackageRequestModel request, Guid id);
        public Task<ResponseResult<PackageViewModel>> DeletePackage(Guid id);
        public Task<ResponseResult<PackageViewModel>> EnablePackage(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<PackageViewModel>> GetPackages(PackageViewModel filter, PagingRequest paging, PackageOrderFilter orderFilter);
        public Task<DynamicModelResponse.DynamicModelsResponse<PackageViewModel>> GetPackagesForAdmin(string? filter, PagingRequest paging, PackageOrderFilter orderFilter);
        Task<ResponseResult<PayOSPackagePaymentResponse>> CreatePayOSPackagePurchasePayment(MonetaryTransactionRequestModel transactionRequest);
    }
}
