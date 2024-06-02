using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts
{
    public class AutoMapperResolver : Profile
    {
        public AutoMapperResolver()
        {
            #region Account
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<CreateAccountRequestModel, Account>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountByMailRequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            #endregion
        }
    }
}
