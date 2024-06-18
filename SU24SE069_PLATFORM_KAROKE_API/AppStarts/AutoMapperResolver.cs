using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts
{
    public class AutoMapperResolver : Profile
    {
        public AutoMapperResolver()
        {
            #region Account
            CreateMap<Account, AccountViewModel>()
                .ForMember(x => x.Role, dest => dest.MapFrom(src => (AccountRole)src.Role))
                .ForMember(x => x.Gender, dest => dest.MapFrom(src => (AccountGender)src.Gender))
                .ForMember(x => x.AccountStatus, dest => dest.MapFrom(src => (AccountStatus)src.AccountStatus))
                .ReverseMap();
            CreateMap<Account, CreateAccountRequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountByMailRequestModel>().ReverseMap();
            #endregion

            #region Song
            CreateMap<Song, SongViewModel>()
                //.ForMember(x => x.Category, dest => dest.MapFrom(opt => (SongCategory)opt.Category))
                .ReverseMap();

            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, UpdateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, CreateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, UpdateSongRequestModel>().ReverseMap();
            #endregion

            #region Friend
            CreateMap<Friend, FriendViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (FriendStatus)src.Status)).ReverseMap();

            CreateMap<Friend, FriendRequestModel>().ReverseMap();
            #endregion

            #region Item
            CreateMap<Item, ItemViewModel>()
                .ForMember(x => x.ItemType, dest => dest.MapFrom(src => (ItemType)src.ItemType))
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(src => (ItemStatus)src.ItemStatus))
                .ReverseMap();

            CreateMap<Item, CreateItemRequestModel>().ReverseMap();
            CreateMap<Item, UpdateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, CreateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, UpdateItemRequestModel>().ReverseMap();
            #endregion

            #region AccountInventoryItem
            CreateMap<AccountInventoryItem, AccountInventoryItemViewModel>()
                .ForMember(x => x.ItemStatus, dest =>dest.MapFrom(opt => (ItemStatus)opt.ItemStatus))
                .ReverseMap();

            CreateMap<AccountInventoryItem, CreateAccountInventoryItemRequestModel>().ReverseMap();
            #endregion

            #region Recording
            CreateMap<Recording, RecordingViewModel>().ReverseMap();
            CreateMap<Recording, CreateRecordingRequestModel>().ReverseMap();
            #endregion

            #region VoiceAudio
            CreateMap<VoiceAudio, CreateVoiceAudioRequestModel>().ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, CreatePostRequestModel>().ReverseMap();
            #endregion
        }
    }
}
