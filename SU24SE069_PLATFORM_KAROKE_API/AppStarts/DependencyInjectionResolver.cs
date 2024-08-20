using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Services;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;
using SU24SE069_PLATFORM_KAROKE_Service.Services;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts
{
    public static class DependencyInjectionResolver
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IBaseDAO<>), typeof(BaseDAO<>));
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddHttpClient();

            #region Account
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion

            #region Song
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<ISongService, SongService>();
            #endregion

            #region Friend
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IFriendService, FriendService>();
            #endregion

            #region Item
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();
            #endregion

            #region AccountInventoryItem
            services.AddScoped<IAccountItemRepository, AccountItemRepository>();
            services.AddScoped<IAccountItemService, AccountItemService>();
            #endregion

            #region Recording
            services.AddScoped<IRecordingRepository, RecordingRepository>();
            services.AddScoped<IRecordingService, RecordingService>();
            #endregion

            #region InAppTransaction
            services.AddScoped<IInAppTransactionRepository, InAppTransactionRepository>();
            services.AddScoped<IInAppTransactionService, InAppTransactionService>();
            #endregion

            #region FavouriteSong
            services.AddScoped<IFavouriteSongRepository, FavouriteSongRepository>();
            services.AddScoped<IFavouriteSongSerivce, FavouriteSongService>();
            #endregion

            #region Package
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageService, PackageService>();
            #endregion

            #region Money Transaction
            services.AddScoped<IMonetaryTransactionRepository, MonetaryTransactionRepository>();
            services.AddScoped<IMonetaryTransactionService, MonetaryTransactionService>();
            #endregion

            #region Money Transaction
            services.AddScoped<IMomoService, MomoService>();
            #endregion

            #region KaraokeRoom
            services.AddScoped<IKaraokeRoomRepository, KaraokeRoomRepository>();
            services.AddScoped<IKaraokeRoomService, KaraokeRoomService>();
            #endregion

            #region PurchasedSong
            services.AddScoped<IPurchasedSongRepository, PurchasedSongRepository>();
            services.AddScoped<IPurchasedSongService, PurchasedSongService>();
            #endregion

            #region SupportRequest
            services.AddScoped<ISupportRequestRepository, SupportRequestRepository>();
            services.AddScoped<ISupportRequestService, SupportRequestService>();
            #endregion

            #region Conversation
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IConversationService, ConversationService>();
            #endregion

            #region LoginActivity
            services.AddScoped<ILoginActivityRepository, LoginActivityRepository>();
            services.AddScoped<ILoginActivityService, LoginActivityService>();
            #endregion

            #region Message
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            #endregion

            #region Singer
            services.AddScoped<ISingerRepository, SingerRepository>();
            services.AddScoped<ISingerService, SingerService>();
            #endregion

            #region Artist
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IArtistService, ArtistService>();
            #endregion

            #region Genre
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            #endregion

            #region LiveChat
            services.AddScoped<IChatHubRepository, ChatHubRepository>();
            #endregion

            #region PostComment
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();  
            services.AddScoped<IPostCommentService, PostCommentService>();
            #endregion

            #region Post
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            #endregion

            #region PostRating
            services.AddScoped<IPostRatingRepository, PostRatingRepository>();
            services.AddScoped<IPostRatingService, PostRatingService>();
            #endregion

            #region Dashboard
            services.AddScoped<IDashboardGameRepository, DashboardGameRepository>();
            services.AddScoped<IDashboardMonetaryRepository, DashboardMonetaryRepository>();
            services.AddScoped<IDashboardService, DashboardService>();
            #endregion

            #region AudioVoice
            services.AddScoped<IVoiceAudioRepository, VoiceAudioRepository>();
            #endregion

            #region FirebaseStorage
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
            #endregion

            #region Report
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportService>();
            #endregion

            #region Notification
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            #endregion

            #region payOS
            services.AddScoped<IPayOSService, PayOSService>();
            #endregion
        }

    }
}
