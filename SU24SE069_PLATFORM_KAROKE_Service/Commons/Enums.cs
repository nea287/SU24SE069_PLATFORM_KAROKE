using System;
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
        Role,
        Star,
        IsOnline,
        CharacterId,
        Fullname,
        Yob,
        IdentityCardNumber,
        PhoneNumber,
        CreatedTime,
        AccountStatus,
    }
    public enum SongOrderFilter
    {
        SongId,
        SongName,
        SongDescription,
        SongUrl,
        SongStatus,
        CreatedDate,
        UpdatedDate,
        SongCode,
        PublicDate,
        CreatorId,
        Price,
    }

    public enum FriendOrderFilter
    {
        SenderId,
        ReceiverId,
        Status,
    }

    public enum ItemOrderFilter
    {
        ItemId,
        ItemCode,
        ItemName,
        ItemDescription,
        ItemType,
        ItemPrice,
        ItemStatus,
        CanExpire,
        CanStack,
        CreatedDate,
        CreatorId,

    }

    public enum RecordingOrderFilter
    {
        RecordingId,
        RecordingName,
        RecordingType,
        CreatedDate,
        UpdatedDate,
        Score,
        SongType,
        SongId,
        HostId,
        OwnerId,
        KaraokeRoomId,

    }

    public enum AccountInventoryItemOrderFilter
    {
        AccountInventoryItemId,
        ItemStatus,
        ActivateDate,
        ExpirationDate,
        Quantity,
        ItemId,
        MemberId
    }

    public enum InAppTransactionOrderFilter
    {

        InAppTransactionId,
        StarAmount,
        Status,
        CreatedDate,
        TransactionType,
        MemberId,
        ItemId,
        SongType,
        SongId,
    }
    public enum FavouriteSongOrderFilter
    {
        MemberId,
        SongId
    }

    public enum PackageOrderFilter
    {
        PackageId,
        PackageName,
        Description,
        MoneyAmount,
        StarNumber,
        Status,
        CreatedDate,
        CreatorId,
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

    public enum AccountStatus
    {
        NOT_VERIFY = 0,
        ACTIVE = 1,
        INACTIVE = 2,
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
    public enum SongCategory
    {
        VPOP = 0,
        POP = 1,
        KPOP = 2,
        ROCK = 3,
    }

    public enum FriendStatus
    {
        OFFLINE = 0,
        ONLINE = 1,
    }
    public enum ItemStatus
    {
        DISABLE = 0,
        ENABLE = 1,
        PENDING = 2,
    }
    public enum ItemType
    {
        CHARACTER,
        ROOM,
        DEFAULT,
    }

    public enum InAppTransactionType
    {
        BUY_ITEM = 1,
    }

    public enum InAppTransactionStatus
    {
        PENDING,
        COMPLETE,
        CANCELED,
    }

    public enum RecordingType
    {
        SINGLE,
        MULTIPLE
    }

    public enum PackageStatus
    {
        INACTIVE = 0,
        ACTIVE = 1,

    }




}