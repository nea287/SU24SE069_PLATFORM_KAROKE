﻿using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Artist;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Conversation;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.FavouriteSong;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Genre;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.KaraokeRoom;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LoginActivity;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Message;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.SupportRequest;
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
            CreateMap<Account, CreateAccount1RequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccount1RequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountByMailRequestModel>().ReverseMap();
            #endregion

            #region Song
            CreateMap<Song, SongViewModel>()
                .ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
                .ReverseMap();

            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, UpdateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, CreateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, UpdateSongRequestModel>().ReverseMap();

            CreateMap<SongArtist, SongArtistViewModel>().ReverseMap();
            CreateMap<SongArtist, SongArtistRequestModel>().ReverseMap();

            CreateMap<SongGenre, SongGenreViewModel>().ReverseMap();
            CreateMap<SongGenre, SongGenreRequestModel>().ReverseMap();

            CreateMap<SongSinger, SongSingerViewModel>().ReverseMap();
            CreateMap<SongSinger, SongSingerRequestModel>().ReverseMap();
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
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(opt => (ItemStatus)opt.ItemStatus))
                .ReverseMap();

            CreateMap<AccountInventoryItem, CreateAccountInventoryItemRequestModel>().ReverseMap();
            #endregion

            #region Recording
            CreateMap<Recording, RecordingViewModel>()
                .ForMember(x => x.RecordingType, dest => dest.MapFrom(src => (RecordingType)src.RecordingType))
                .ReverseMap();
            CreateMap<Recording, CreateRecordingRequestModel>().ReverseMap();
            #endregion

            #region VoiceAudio
            CreateMap<VoiceAudio, CreateVoiceAudioRequestModel>().ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, CreatePostRequestModel>().ReverseMap();
            #endregion

            #region InAppTransaction
            CreateMap<InAppTransaction, InAppTransactionViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (InAppTransactionStatus)src.Status))
                .ForMember(x => x.TransactionType, dest => dest.MapFrom(src => (InAppTransactionType)src.TransactionType))
                .ReverseMap();

            CreateMap<InAppTransaction, CrreateInAppTransactionRequestModel>().ReverseMap();
            CreateMap<InAppTransaction, UpdateInAppTransactionRequestModel>().ReverseMap();
            #endregion

            #region FavouriteSong
            CreateMap<FavouriteSong, FavouriteSongViewModel>().ReverseMap();
            CreateMap<FavouriteSong, CreateFavouriteSongRequestModel>().ReverseMap();
            CreateMap<FavouriteSongViewModel, CreateFavouriteSongRequestModel>().ReverseMap();
            #endregion

            #region Package
            CreateMap<Package, PackageViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PackageStatus)src.Status))
                .ReverseMap();
            CreateMap<Package, PackageRequestModel>().ReverseMap();
            #endregion

            #region MoneyTransaction
            CreateMap<MoneyTransaction, MoneyTransactionViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PaymentStatus)src.Status))
                .ForMember(x => x.PaymentType, dest => dest.MapFrom(src => (PaymentType)src.PaymentType))
                .ReverseMap();

            CreateMap<MoneyTransaction, MoneyTransactionRequestModel>().ReverseMap();
            #endregion

            #region KaraokeRoom
            CreateMap<KaraokeRoom, KaraokeRoomViewModel>().ReverseMap();
            CreateMap<KaraokeRoom, KaraokeRoomRequestModel>().ReverseMap();
            #endregion

            #region PurchasedSong
            CreateMap<PurchasedSong, PurchasedSongViewModel>().ReverseMap();
            CreateMap<PurchasedSong, PurchasedSongRequestModel>().ReverseMap();
            #endregion

            #region SupportRequest
            CreateMap<SupportRequest, SupportRequestViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (SupportRequestStatus)src.Status))
                .ForMember(x => x.Category, dest => dest.MapFrom(src => (SupportRequestCategory)src.Category))
                .ReverseMap();
            CreateMap<SupportRequest, SupportRequestRequestModel>().ReverseMap();
            #endregion

            #region Conversation
            CreateMap<Conversation, ConversationViewModel>()
                .ForMember(x => x.ConversationType, dest => dest.MapFrom(src => (ConversationType)src.ConversationType))
                .ReverseMap();

            CreateMap<Conversation, ConversationRequestModel>().ReverseMap();
            CreateMap<Conversation, ChatConversationRequestModel>()
                .ForMember(x => x.Message, dest => dest.Ignore())
                .ReverseMap();
            #endregion

            #region LoginActivity
            CreateMap<LoginActivity, LoginActivityViewModel>().ReverseMap();
            CreateMap<LoginActivity, LoginActivityRequestModel>().ReverseMap();
            #endregion

            #region Message
            CreateMap<Message, MessageViewModel>().ReverseMap();
            CreateMap<Message, MessageRequestModel>().ReverseMap();
            #endregion

            #region Singer
            CreateMap<Singer, SingerViewModel>().ReverseMap();
            CreateMap<Singer, SingerRequestModel>().ReverseMap();
            #endregion

            #region Artist
            CreateMap<Artist, ArtistViewModel>().ReverseMap();
            CreateMap<Artist, ArtistRequestModel>().ReverseMap();
            #endregion

            #region Genre
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<Genre, GenreRequestModel>().ReverseMap();
            #endregion

            #region PostComment
            CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PostCommentStatus)src.Status))
                .ForMember(x => x.CommentType, dest => dest.MapFrom(src => (PostCommentType)src.CommentType))
                .ReverseMap();

            CreateMap<PostComment, CreatePostCommentRequestModel>().ReverseMap();
            CreateMap<PostComment, UpdatePostComment>().ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, CreatePostRequestModel>().ReverseMap();
            CreateMap<Post, PostViewModel>()
                .ForMember(x => x.SongUrl, dest => dest.MapFrom(src => src.Recording.Song.SongUrl))
                .ReverseMap();
            #endregion

            #region PostRating
            CreateMap<PostRating, PostRatingViewModel>().ReverseMap();
            CreateMap<PostRating, PostRatingRequestModel>().ReverseMap();
            CreateMap<PostRating, UpdatePostRatingRequestModel>().ReverseMap();
            CreateMap<PostRatingViewModel, UpdatePostRatingRequestModel>().ReverseMap();
            CreateMap<PostRatingViewModel, PostRatingRequestModel>().ReverseMap();
            #endregion
        }
    }
}
