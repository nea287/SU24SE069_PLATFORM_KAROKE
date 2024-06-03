﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons
{

    #region filter order
    public enum SortOrder
    {
        Descending,
        Ascending,

    }

    public enum AccountOrderFilter
    {
        AccountId,
        UserName,
        Password,
        Email,
        Gender,
        AccountName,
        IsVerified,
        Role,
        Star,
        IsOnline,
        CharacterId,
        Fullname,
        Yob,
        IdentityCardNumber,
        PhoneNumber,
        CreatedTime,
    }
    #endregion
    public enum AccountRole
    {
        ADMIN = 1,
        STAFF = 2,
        MEMBER = 3,
    }
    public enum AccountGender
    {
        MALE = 1,
        FEMALE = 2,
        OTHERS = 3,
    }

    public enum ItemStatus
    {

    }

    public enum SongStatus
    {
        DISABLE = 0,
        ENABLE = 1,
    }

    public enum SongType
    {
        INTERNAL = 1,
        EXTERNAL = 2,
    }
    public enum SongOrderFilter
    {
        SongId,
        SongName,
        SongDescription,
        SongUrl,
        Source,
        SongStatus,
        CreatedDate,
        UpdatedDate,
        SongCode,
        PublicDate,
        SongType,
        Tempo,
        CreatorId,
        Price,
    }

}