using AutoMapper;
using Castle.Core.Internal;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Notification;
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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.SupportRequest;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio;
using PostCommentFilter = SU24SE069_PLATFORM_KAROKE_Service.Filters.PostCommentFilter;

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
                .ForMember(x => x.CharaterItemCode, dest =>
                {
                    dest.MapFrom(a => a.CharacterItem != null ? a.CharacterItem.Item.ItemCode : (string?)null);
                })
                .ForMember(x => x.RoomItemCode, dest =>
                {
                    dest.MapFrom(a => a.RoomItem != null ? a.RoomItem.Item.ItemCode : (string?)null);
                });

            CreateMap<Account, AccountPostViewModel>()
                .ForMember(x => x.Role, dest => dest.MapFrom(src => (AccountRole)src.Role))
                .ForMember(x => x.Gender, dest => dest.MapFrom(src => (AccountGender)src.Gender))
                .ForMember(x => x.AccountStatus, dest => dest.MapFrom(src => (AccountStatus)src.AccountStatus))
                .ForMember(x => x.CharaterItemCode, dest =>
                {
                    dest.MapFrom(a => a.CharacterItem != null ? a.CharacterItem.Item.ItemCode : (string?)null);
                })
                .ForMember(x => x.RoomItemCode, dest =>
                {
                    dest.MapFrom(a => a.RoomItem != null ? a.RoomItem.Item.ItemCode : (string?)null);
                })
                ;
            CreateMap<Account, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, CreateAccount1RequestModel>().ReverseMap();
            CreateMap<AccountViewModel, CreateAccount1RequestModel>().ReverseMap();
            CreateMap<AccountViewModel, AccountFilter>().ReverseMap();
            CreateMap<Account, AccountModel>()
                .ForMember(x => x.Role, dest => dest.MapFrom(src => (AccountRole)src.Role))
                .ForMember(x => x.Gender, dest => dest.MapFrom(src => (AccountGender)src.Gender))
                .ForMember(x => x.AccountStatus, dest => dest.MapFrom(src => (AccountStatus)src.AccountStatus))
                .ForMember(x => x.CharaterItemCode, dest =>
                {
                    dest.MapFrom(a => a.CharacterItem != null ? a.CharacterItem.Item.ItemCode : (string?)null);
                })
                .ForMember(x => x.RoomItemCode, dest =>
                {
                    dest.MapFrom(a => a.RoomItem != null ? a.RoomItem.Item.ItemCode : (string?)null);
                })
                .ReverseMap();
            CreateMap<AccountViewModel, AccountModel>()

                // .ForMember(x => x.Role, dest => dest.MapFrom(src => (AccountRole)src.Role))
                //.ForMember(x => x.Gender, dest => dest.MapFrom(src => (AccountGender)src.Gender))
                //.ForMember(x => x.AccountStatus, dest => dest.MapFrom(src => (AccountStatus)src.AccountStatus))
                //.ForMember(x => x.CharaterItemCode, dest =>
                //{
                //    dest.MapFrom(a => a.CharacterItem != null ? a.CharacterItem.Item.ItemCode : (string?)null);
                //})
                //.ForMember(x => x.RoomItemCode, dest =>
                //{
                //    dest.MapFrom(a => a.RoomItem != null ? a.RoomItem.Item.ItemCode : (string?)null);
                //})
                .ReverseMap();
            CreateMap<AccountViewModel, CreateAccountRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountByMailRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountRequestModel>().ReverseMap();
            #endregion

            #region Song
            CreateMap<Song, SongModel>()
                  .ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
                .ReverseMap();
            CreateMap<Song, SongViewModel>()
                .ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
                .ReverseMap();

            CreateMap<Song, SongFilter>()
                .ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
                .ForMember(x => x.SingerName, dest =>
                {
                    dest.MapFrom(a => a.SongSingers.Any() && a.SongSingers != null ? a.SongSingers.Select(t => t.Singer.SingerName).First() : (string?)null);
                })
                .ForMember(x => x.GenreName, dest =>
                {
                    dest.MapFrom(a => a.SongGenres.Any() && a.SongGenres != null ? a.SongGenres.Select(t => t.Genre.GenreName).First() : (string?)null);
                })
                .ForMember(x => x.ArtistName, dest =>
                {
                    dest.MapFrom(a => a.SongArtists.Any() && a.SongArtists != null ? a.SongArtists.Select(t => t.Artist.ArtistName).First() : (string?)null);
                })
                .ReverseMap();
            
            CreateMap<Song, SongInAppViewModel>()
                .ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
                .ForMember(x => x.Singer, dest =>
                {
                    dest.MapFrom(a => a.SongSingers.Any() && a.SongSingers != null ? a.SongSingers.Select(t => t.Singer.SingerName).ToList() : new List<string>());
                })
                .ForMember(x => x.Genre, dest =>
                {
                    dest.MapFrom(a => a.SongGenres.Any() && a.SongGenres != null ? a.SongGenres.Select(t => t.Genre.GenreName).ToList() : new List<string>());
                })
                .ForMember(x => x.Artist, dest =>
                {
                    dest.MapFrom(a => a.SongArtists.Any() && a.SongArtists != null ? a.SongArtists.Select(t => t.Artist.ArtistName).ToList() : new List<string>());
                })
                .ReverseMap();

            CreateMap<SongModel1, UpdateSongRequestModel>().ReverseMap();
            CreateMap<SongModel1, Song>().ReverseMap();

            //CreateMap<Song, SongFilter>()
            //.ForMember(x => x.SongStatus, dest => dest.MapFrom(opt => (SongStatus)opt.SongStatus))
            //.ForMember(x => x.Genre, dest => dest.MapFrom(opt => opt.SongGenres != null ? opt.SongGenres.Select(a => a.Genre.GenreName).ToList() : new List<string>()))
            //.ForMember(x => x.Singer, dest => dest.MapFrom(opt => opt.SongSingers != null ? opt.SongSingers.Select(a => a.Singer.SingerName).ToList() : new List<string>()))
            //.ForMember(x => x.Artist, dest => dest.MapFrom(opt => opt.SongArtists != null ? opt.SongArtists.Select(a => a.Artist.ArtistName).ToList() : new List<string>()))
            //.ForMember(x => x.SingerName, dest => dest.MapFrom(opt => opt.SongSingers != null && opt.SongSingers.Any() ? opt.SongSingers.Select(t => t.Singer.SingerName).First() : (string)null))
            //.ForMember(x => x.GenreName, dest => dest.MapFrom(opt => opt.SongGenres != null && opt.SongGenres.Any() ? opt.SongGenres.Select(t => t.Genre.GenreName).First() : (string)null))
            //.ForMember(x => x.ArtistName, dest => dest.MapFrom(opt => opt.SongArtists != null && opt.SongArtists.Any() ? opt.SongArtists.Select(t => t.Artist.ArtistName).First() : (string)null))
            //.ReverseMap();


            CreateMap<SongFilter, SongViewModel>()
                .ForMember(x => x.Genre, dest => dest.MapFrom(opt => opt.SongGenres.Select(a => a.GenreName)))
                .ForMember(x => x.Singer, dest => dest.MapFrom(opt => opt.SongSingers.Select(a => a.SingerName)))
                .ForMember(x => x.Artist, dest => dest.MapFrom(opt => opt.SongArtists.Select(a => a.ArtistName)))
                .ReverseMap();

            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, CreateSongRequestModel>().ReverseMap();
            CreateMap<Song, UpdateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, CreateSongRequestModel>().ReverseMap();
            CreateMap<SongViewModel, UpdateSongRequestModel>().ReverseMap();

            CreateMap<SongArtist, SongArtistViewModel>()
                .ForMember(x => x.SongName, dest => dest.MapFrom(opt => opt.Song.SongName))
                .ForMember(x => x.ArtistName, dest => dest.MapFrom(opt => opt.Artist.ArtistName))
                .ForMember(x => x.Image, dest => dest.MapFrom(opt => opt.Artist.Image))
                .ReverseMap();
            CreateMap<SongArtist, SongArtistRequestModel>().ReverseMap();

            CreateMap<SongGenre, SongGenreViewModel>()
                .ForMember(x => x.SongName, dest => dest.MapFrom(opt => opt.Song.SongName))
                .ForMember(x => x.GenreName, dest => dest.MapFrom(opt => opt.Genre.GenreName))
                .ForMember(x => x.Image, dest => dest.MapFrom(opt => opt.Genre.Image))
                .ReverseMap();
            CreateMap<SongGenre, SongGenreRequestModel>().ReverseMap();

            CreateMap<SongSinger, SongSingerViewModel>()
                .ForMember(x => x.SongName, dest => dest.MapFrom(opt => opt.Song.SongName))
                .ForMember(x => x.SingerName, dest => dest.MapFrom(opt => opt.Singer.SingerName))
                .ForMember(x => x.Image, dest => dest.MapFrom(opt => opt.Singer.Image))
                .ReverseMap();
            CreateMap<SongSinger, SongSingerRequestModel>().ReverseMap();

            CreateMap<SongViewModel, SongDTO>().ReverseMap();
            CreateMap<Song, SongDTO>()
                .ForMember(d => d.SongStatus, opt => opt.MapFrom(s => (SongStatus)s.SongStatus))
                .ForMember(x => x.Singer, dest =>
                {
                    dest.MapFrom(a => a.SongSingers.Any() && a.SongSingers != null ? a.SongSingers.Select(t => t.Singer.SingerName).ToList() : new List<string>());
                })
                .ForMember(x => x.Genre, dest =>
                {
                    dest.MapFrom(a => a.SongGenres.Any() && a.SongGenres != null ? a.SongGenres.Select(t => t.Genre.GenreName).ToList() : new List<string>());
                })
                .ForMember(x => x.Artist, dest =>
                {
                    dest.MapFrom(a => a.SongArtists.Any() && a.SongArtists != null ? a.SongArtists.Select(t => t.Artist.ArtistName).ToList() : new List<string>());
                })


                .ReverseMap();
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
                .ForMember(x => x.CreatorMail, dest => dest.MapFrom(src => src.Creator.Email))
     
                .ReverseMap();
            
            CreateMap<Item, ItemInAppViewModel>()
                .ForMember(x => x.ItemType, dest => dest.MapFrom(src => (ItemType)src.ItemType))
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(src => (ItemStatus)src.ItemStatus))
                .ForMember(x => x.CreatorMail, dest => dest.MapFrom(src => src.Creator.Email))
                .ReverseMap();

            CreateMap<Item, ItemModel>()
                .ForMember(x => x.ItemType, dest => dest.MapFrom(src => (ItemType)src.ItemType))
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(src => (ItemStatus)src.ItemStatus))
                .ForMember(x => x.CreatorMail, dest => dest.MapFrom(src => src.Creator.Email))
                .ReverseMap();


            CreateMap<Item, ItemFilter>()
                .ForMember(x => x.ItemType, dest => dest.MapFrom(src => (ItemType)src.ItemType))
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(src => (ItemStatus)src.ItemStatus))
                .ForMember(x => x.CreatorMail, dest => dest.MapFrom(src => src.Creator.Email))

                .ForMember(x => x.BuyerId, dest =>
                {
                          dest.MapFrom(a => a.AccountItems.Any() && a.AccountItems != null ? a.AccountItems.Select(t => t.MemberId).First() : (Guid?)null);
                })
                .ReverseMap();

            CreateMap<ItemViewModel, ItemFilter>().ReverseMap();
            CreateMap<ItemShopViewModel, ItemFilter>().ReverseMap();

            //CreateMap<ItemFilter, ItemShopViewModel>()
            //    .ForMember(x => x.IsOwned, dest =>
            //    {
            //        dest.MapFrom(src => src.CanStack == true? false : (src.AccountItems.Any(a => a.MemberId == )));
            //    })
            //    .ReverseMap();

            CreateMap<Item, CreateItemRequestModel>().ReverseMap();
            CreateMap<Item, UpdateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, CreateItemRequestModel>().ReverseMap();
            CreateMap<ItemViewModel, UpdateItemRequestModel>().ReverseMap();
            #endregion

            #region AccountInventoryItem
            CreateMap<AccountItemFilter, AccountItemViewModel>().ReverseMap();
            CreateMap<AccountItem, AccountItemViewModel>()
                .ForMember(x => x.ItemStatus, dest => dest.MapFrom(opt => (ItemStatus)opt.ItemStatus))
                .ForMember(x => x.ItemType, dest =>  dest.MapFrom(a => (ItemType)a.Item.ItemType))
                .ForMember(x => x.Item, dest => dest.MapFrom(a => a.Item
                ))
                .ReverseMap();

            CreateMap<AccountItem, CreateAccountInventoryItemRequestModel>().ReverseMap();
            #endregion

            #region Recording
            CreateMap<RecordingFilter, RecordingViewModel>().ReverseMap();
            CreateMap<Recording, RecordingViewModel>()
                .ForMember(x => x.RecordingType, dest => dest.MapFrom(src => (RecordingType)src.RecordingType))
                .ForMember(x => x.SongUrl, dest => dest.MapFrom(src => src.PurchasedSong.Song.SongUrl))
                .ReverseMap();
            
            CreateMap<Recording, RecordingPostViewModel>()
                .ForMember(x => x.RecordingType, dest => dest.MapFrom(src => (RecordingType)src.RecordingType))
                .ForMember(x => x.SongUrl, dest => dest.MapFrom(src => src.PurchasedSong.Song.SongUrl))
                .ReverseMap();

            CreateMap<Recording, CreateRecordingRequestModel>().ReverseMap();
            CreateMap<Recording, UpdateRecording1RequestModel>()
                .ForMember(x => x.VoiceAudios, dest => dest.MapFrom(opt => opt.VoiceAudios)).ReverseMap();
            #endregion

            #region VoiceAudio
            CreateMap<VoiceAudio, CreateVoiceAudioRequestModel>().ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, CreatePostRequestModel>()
                .ReverseMap();
            #endregion

            #region InAppTransaction
            CreateMap<InAppTransaction, InAppTransactionViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (InAppTransactionStatus)src.Status))
                .ForMember(x => x.TransactionType, dest => dest.MapFrom(src => (InAppTransactionType)src.TransactionType))
                .ForMember(x => x.UserName, dest => dest.MapFrom(src => src.Member.UserName))
                .ReverseMap();

            CreateMap<InAppTransactionFilter, InAppTransactionViewModel>().ReverseMap();

            CreateMap<InAppTransaction, CrreateInAppTransactionRequestModel>().ReverseMap();
            CreateMap<InAppTransaction, UpdateInAppTransactionRequestModel>().ReverseMap();
            #endregion

            #region FavouriteSong
            CreateMap<FavouriteSong, FavouriteSongViewModel>()
                .ForMember(x => x.Artists, dest => dest.MapFrom(opt => opt.Song.SongArtists.Select(a => a.Artist.ArtistName)))
                .ForMember(x => x.Singers, dest => dest.MapFrom(opt => opt.Song.SongSingers.Select(a => a.Singer.SingerName)))  
                .ForMember(x => x.Genres, dest => dest.MapFrom(opt => opt.Song.SongGenres.Select(a => a.Genre.GenreName)))
                .ForMember(x => x.SongName, dest => dest.MapFrom(opt => opt.Song.SongName))
                .ForMember(x => x.SingerName, dest =>
                {
                    dest.MapFrom(a => a.Song.SongSingers.Any() && a.Song.SongSingers != null ? a.Song.SongSingers.Select(t => t.Singer.SingerName).First() : (string?)null);
                })
                .ForMember(x => x.ArtistName, dest =>
                {
                    dest.MapFrom(a => a.Song.SongArtists.Any() && a.Song.SongArtists != null ? a.Song.SongArtists.Select(t => t.Artist.ArtistName).First() : (string?)null);
                })
                .ForMember(x => x.GenreName, dest =>
                {
                    dest.MapFrom(a => a.Song.SongGenres.Any() && a.Song.SongGenres != null ? a.Song.SongGenres.Select(t => t.Genre.GenreName).First() : (string?)null);
                })
                .ForMember(d => d.SongUrl, opt => opt.MapFrom(s => s.Song.SongUrl))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Song.Price))
                .ReverseMap();

            CreateMap<FavouriteSong, CreateFavouriteSongRequestModel>().ReverseMap();
            CreateMap<FavouriteSongViewModel, CreateFavouriteSongRequestModel>().ReverseMap();
            CreateMap<FavouriteSongViewModel, FavoriteSongDTO>().ReverseMap();
            #endregion

            #region Package
            CreateMap<Package, PackageViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PackageStatus)src.Status))
                .ReverseMap();
            CreateMap<Package, PackageRequestModel>().ReverseMap();
            #endregion

            #region MoneyTransaction
            CreateMap<MonetaryTransaction, MonetaryTransactionViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PaymentStatus)src.Status))
                .ForMember(x => x.PaymentType, dest => dest.MapFrom(src => (PaymentType)src.PaymentType))
                .ForMember(x => x.PackageMoneyAmount, dest => dest.MapFrom(src => src.Package.MoneyAmount))
                .ForMember(x => x.PackageName, dest => dest.MapFrom(src => src.Package.PackageName))
                .ForMember(x => x.Username, dest => dest.MapFrom(src => src.Member.UserName))
                .ReverseMap();

            CreateMap<MonetaryTransaction, MonetaryTransactionRequestModel>().ReverseMap();
            #endregion

            #region KaraokeRoom
            CreateMap<KaraokeRoom, KaraokeRoomViewModel>().ReverseMap();
            CreateMap<KaraokeRoom, KaraokeRoomRequestModel>().ReverseMap();
            #endregion

            #region PurchasedSong
            CreateMap<PurchasedSong, PurchasedSongViewModel>()
                    .ForMember(x => x.Price, dest => dest.MapFrom(dest => dest.Song.Price))
                    .ForMember(x => x.SongName, dest => dest.MapFrom(dest => dest.Song.SongName))
                    .ForMember(x => x.Artists, dest => dest.MapFrom(dest => dest.Song.SongArtists.Select(a => a.Artist.ArtistName)))
                    .ForMember(x => x.Singers, dest => dest.MapFrom(dest => dest.Song.SongSingers.Select(a => a.Singer.SingerName)))
                    .ForMember(x => x.Genres, dest => dest.MapFrom(dest => dest.Song.SongGenres.Select(a => a.Genre.GenreName)))
                    .ForMember(d => d.SongUrl, opt => opt.MapFrom(s => s.Song.SongUrl))
            .ReverseMap();

            CreateMap<PurchasedSong, PurchasedSongRequestModel>().ReverseMap();
            CreateMap<PurchasedSongViewModel, PurchasedSongDTO>().ReverseMap();
            #endregion

            #region SupportRequest
            CreateMap<Ticket, SupportRequestViewModel>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (SupportRequestStatus)src.Status))
                .ForMember(x => x.Category, dest => dest.MapFrom(src => (SupportRequestCategory)src.Category))
                .ReverseMap();
            CreateMap<Ticket, SupportRequestRequestModel>().ReverseMap();
            #endregion

            #region Conversation
            CreateMap<Conversation, ConversationViewModel>()
                .ForMember(x => x.ConversationType, dest => dest.MapFrom(src => (ConversationType)src.ConversationType))
                .ReverseMap();

            CreateMap<ConversationFilter, ConversationViewModel>().ReverseMap();

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
            CreateMap<PostComment, PostCommentViewModel>().ReverseMap();
            CreateMap<PostCommentFilter, PostCommentViewModel>().ReverseMap();
            CreateMap<PostCommentViewModel, PostComment>()
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (PostCommentStatus)src.Status))
                .ForMember(x => x.CommentType, dest => dest.MapFrom(src => (PostCommentType)src.CommentType))
                .ForMember(x => x.Member, dest => dest.MapFrom(src => src.Member))
                .ReverseMap();

            CreateMap<PostComment, CreatePostCommentRequestModel>().ReverseMap();
            CreateMap<PostComment, UpdatePostComment>().ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, CreatePostRequestModel>().ReverseMap();
            CreateMap<PostFilter, PostViewModel>().ReverseMap();
            CreateMap<Post, PostFilter>()
                .ForMember(e => e.PostStatus, dest => dest.MapFrom(opt => (PostStatus)opt.Status))
                .ForMember(e => e.PostType, dest => dest.MapFrom(opt => (PostType)opt.PostType))
                .ForMember(x => x.SongUrl, dest => dest.MapFrom(src => src.Recording.PurchasedSong.Song.SongUrl))
                .ReverseMap();
            CreateMap<Post, PostViewModel>()
                .ForMember(e => e.PostStatus, dest => dest.MapFrom(opt => (PostStatus)opt.Status))
                .ForMember(e => e.PostType, dest => dest.MapFrom(opt => (PostType)opt.PostType))
                .ForMember(x => x.SongUrl, dest => dest.MapFrom(src => src.Recording.PurchasedSong.Song.SongUrl))
                .ForMember(e => e.Member, dest => dest.MapFrom(src => src.Member??null))
                .ReverseMap();
            #endregion

            #region PostRating
            CreateMap<PostRating, PostRatingViewModel>().ReverseMap();
            CreateMap<PostRating, PostRatingRequestModel>().ReverseMap();
            CreateMap<PostRating, UpdatePostRatingRequestModel>().ReverseMap();
            CreateMap<PostRatingViewModel, UpdatePostRatingRequestModel>().ReverseMap();
            CreateMap<PostRatingViewModel, PostRatingRequestModel>().ReverseMap();
            #endregion
            #region VoiceAudio
            CreateMap<VoiceAudio, VoiceAudioViewModel>().ReverseMap();
            #endregion

            #region Report
            CreateMap<Report, ReportViewModel>()
                .ForMember(x => x.ReportType, dest => dest.MapFrom(src => (ReportType)src .ReportType))
                .ForMember(x => x.ReportCategory, dest => dest.MapFrom(src => (ReportCatagory)src.ReportCategory))
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (ReportStatus)src.Status))
                .ForMember(x => x.Title, dest => 
         
                    dest.MapFrom(src => (ReportTitle?)src.Title)
                )
                .ForMember(x => x.Comment, dest => dest.MapFrom(src => src.Comment.Comment))
                .ForMember(x => x.RoomLog, dest => dest.MapFrom(src => src.Room.RoomLog))  
                .ForMember(x => x.PostCaption, dest => dest.MapFrom(src => src.Post.Caption))  
                .ReverseMap();

            CreateMap<Report, CreateReportForMemberRequestModel>().ReverseMap();
            CreateMap<Report,  UpdateReportForMemberRequestModel>().ReverseMap();   
            CreateMap<Report, CreateReportRequestModel>().ReverseMap();
            #endregion

            #region Notification 
            CreateMap<Notification, NotificationViewModel>()
               // .ForMember(x => x.AccountEmail, dest => dest.MapFrom(src => src.Account.Email))
                .ForMember(x => x.NotificationType, dest => dest.MapFrom(src => (NotificationType) src.NotificationType))
                .ForMember(x => x.Status, dest => dest.MapFrom(src => (NotificationStatus) src.Status))
                .ReverseMap();
            CreateMap<Notification, CreateNotificationRequestModel>().ReverseMap();
            CreateMap<NotificationFiilter, NotificationViewModel>().ReverseMap();
            CreateMap<Notification, NotificationResponse>()
                .ForMember(d => d.NotificationType, opt => opt.MapFrom(s => (NotificationType)s.NotificationType))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => (NotificationStatus)s.Status));
            #endregion
        }
    }
}
