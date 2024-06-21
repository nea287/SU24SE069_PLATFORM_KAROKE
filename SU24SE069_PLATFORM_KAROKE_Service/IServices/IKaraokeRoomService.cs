﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.KaraokeRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IKaraokeRoomService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<KaraokeRoomViewModel>> GetRooms(KaraokeRoomViewModel filter, PagingRequest paging, KaraokeRoomOrderFilter orderFilter);
        public Task<ResponseResult<KaraokeRoomViewModel>> CreateRoom(KaraokeRoomRequestModel request);
        public Task<ResponseResult<KaraokeRoomViewModel>> UpdateRoom(KaraokeRoomRequestModel request, Guid id);
    }
}
