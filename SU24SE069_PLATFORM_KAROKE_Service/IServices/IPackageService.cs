using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPackageService
    {
        public Task<ResponseResult<PackageViewModel>> CreatePackage(PackageRequestModel request);
        public Task<ResponseResult<PackageViewModel>> UpdatePackage(PackageRequestModel request, Guid id);
        public Task<ResponseResult<PackageViewModel>> DeletePackage(Guid id);
        public Task<ResponseResult<PackageViewModel>> EnablePackage(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<PackageViewModel>> GetPackages(PackageViewModel filter, PagingRequest paging, PackageOrderFilter orderFilter);
    }
}
