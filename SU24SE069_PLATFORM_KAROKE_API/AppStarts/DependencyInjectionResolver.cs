﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Services;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
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


        }
    }
}
