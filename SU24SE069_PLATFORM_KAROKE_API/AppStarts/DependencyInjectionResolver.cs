using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Services;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
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
            services.AddScoped<IAccountInventoryItemRepository, AccountInventoryItemRepository>();
            services.AddScoped<IAccountInventoryItemService, AccountInventoryItemService>();
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
            services.AddScoped<IMoneyTransactionRepository, MoneyTransactionRepository>();
            services.AddScoped<IMoneyTransactionService, MoneyTransactionService>();
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

        }
    }
}
