﻿using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts
{
    public class AutoMapperResolver : Profile
    {
        public AutoMapperResolver()
        {
            #region Account
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Account, CreateAccountRequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountByMailRequestModel>().ReverseMap();
            #endregion

            #region Song
            CreateMap<Song, SongViewModel>().ReverseMap();
            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, UpdateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, CreateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, UpdateSongRequestModel>().ReverseMap();
            #endregion

            #region Friend
            CreateMap<Friend, FriendViewModel>().ReverseMap();
            CreateMap<Friend, FriendRequestModel>().ReverseMap();
            #endregion

            #region Item
            CreateMap<Item, ItemViewModel>().ReverseMap();
            CreateMap<Item, CreateItemRequestModel>().ReverseMap();
            CreateMap<Item, UpdateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, CreateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, UpdateItemRequestModel>().ReverseMap();
            #endregion

            #region AccountInventoryItem
            CreateMap<AccountInventoryItem, AccountInventoryItemViewModel>().ReverseMap();
            CreateMap<AccountInventoryItem, CreateAccountInventoryItemRequestModel>().ReverseMap();
            #endregion

            #region Recording
            CreateMap<Recording, RecordingViewModel>().ReverseMap();
            CreateMap<Recording, CreateRecordingRequestModel>().ReverseMap();
            #endregion
        }
    }
}
