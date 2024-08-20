﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons
{

    #region filter order
    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }
    public enum SortOrder
    {
        Descending,
        Ascending,

    }

    public enum PostOrderFilter
    {
        PostId,
        Caption,
        UploadTime,
        UpdateTime,
        MemberId,
        RecordingId,
    }


    public enum ReportOrderFilter
    {
        ReportId, ReporterId, ReportedAccountId, ReportCategory, Status, Reason, CreateTime, ReportType, CommentId, PostId, RoomId
    }

    public enum PostRateOrderFilter
    {
        RateId,
        MemberId,
        PostId,
        VoteType,
        Category,
        Comment,
    }

    public enum PostShareOrderFilter
    {
        PostShareId,
        Caption,
        ShareTime,
        UpdateTime,
        MemberId,
        PostId,
    }

    //public enum ReportOrderFilter
    //{
    //    ReportId,
    //    ReporterId,
    //    ReportedAccountId,
    //    ReportCategory,
    //    Status,
    //    Reason,
    //    CreateTime,
    //    ReportType,
    //    CommentId,
    //    PostId,
    //    RoomId,
    //}

    public enum NoticationFilter
    {
        NotificationId,
        Description,
        NotificationType,
        Status,
        CreateDate,
        AccountId,

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
        Description,
        CharacterId,
        Fullname,
        Yob,
        IdentityCardNumber,
        PhoneNumber,
        CreatedTime,
        AccountStatus,
        UpBalance,
        Image,
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
        ItemStatus,
        CanExpire,
        CanStack,
        CreatedDate,
        CreatorId,
        ItemBuyPrice,
        ItemSetPrice,
        PrefabCode,


    }

    public enum RecordingOrderFilter
    {
        RecordingId,
        RecordingName,
        RecordingType,
        CreatedDate,
        UpdatedDate,
        Volume,
        Score,
        PurchasedSongId,
        HostId,
        OwnerId,
        KaraokeRoomId,

    }

    public enum RatingOrderFilter
    {
        MemberId,
        PostId,
        Score,
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
        UpAmountBefore,
        UpTotalAmount,
        Status,
        CreatedDate,
        TransactionType,
        ItemId,
        SongId,
        MonetaryTransactionId,
        ItemQuantity,
        ItemPrice,
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

    public enum MonetaryTransactionOrderFilter
    {
        MonetaryTransactionId,
        PaymentType,
        PaymentCode,
        MoneyAmount,
        Currency,
        Status,
        CreatedDate,
        PackageId,
        MemberId,
    }

    public enum KaraokeRoomOrderFilter
    {
        RoomId,
        RoomLog,
        CreateTime,
        CreatorId,
    }

    public enum PurchasedSongOrderFilter
    {
        PurchasedSongId,
        PurchaseDate,
        MemberId,
        SongId,
    }

    public enum SupportRequestOrderFilter
    {
        TicketId,
        Problem,
        CreateTime,
        Category,
        Status,
        SenderId,
    }

    public enum ConversationOrderFilter
    {
        ConversationId,
        MemberId1,
        MemberId2,
        ConversationType,
        SupportRequestId,
    }

    public enum LoginActivityOrderFilter
    {
        LoginId,
        LoginTime,
        LoginDevice,
        MemberId,
    }

    public enum MessageOrderFilter
    {
        MessageId,
        Content,
        TimeStamp,
        SenderId,
        ConversationId,
    }

    public enum SingerOrderFilter
    {
        SingerId,
        SingerName,
        Image
    }

    public enum ArtistOrderFilter
    {
        ArtistId,
        ArtistName,
        Image
    }

    public enum GenreOrderFilter
    {
        GenreId,
        GenreName,
        Image

    }

    public enum PostCommentFilter
    {
        CommentId,
        Comment,
        CommentType,
        Status,
        ParentCommentId,
        MemberId,
        PostId,
        UploadTime
    }
    #endregion

    #region Account
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



    #endregion

    #region Song
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

    #endregion

    #region Friend
    public enum FriendStatus
    {
        OFFLINE = 0,
        ONLINE = 1,
    }
    #endregion
    #region Item
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

    #endregion

    #region InAppTransaction
    public enum InAppTransactionType
    {
        BUY_ITEM = 1,
        BUY_SONG = 2,
        RECHARGE_UP_BALANCE = 3,
    }

    public enum InAppTransactionStatus
    {
        PENDING,
        COMPLETE,
        CANCELED,
    }

    #endregion

    #region Recording

    public enum RecordingType
    {
        SINGLE,
        MULTIPLE
    }
    #endregion

    #region Package

    public enum PackageStatus
    {
        INACTIVE = 0,
        ACTIVE = 1,

    }
    #endregion

    #region Payment

    public enum PaymentType
    {
        MOMO = 1,
        PAYOS = 2,
    }

    public enum PaymentStatus
    {
        PENDING,
        COMPLETE,
        CANCELLED,
    }
    #endregion

    #region SupportRequest
    public enum SupportRequestCategory
    {
        TECHNICAL,
        PROBLEM,

    }

    public enum SupportRequestStatus
    {
        CANCELED,
        PROCESSING,
        PROCESSED,

    }

    #endregion

    #region Conversation
    public enum ConversationType
    {
        DEFAULT,
        SUPPORT,
    }
    #endregion
    #region Post

    public enum PostStatus
    {
        ACTIVE,
        DEACTIVE,
    }
    public enum PostType
    {
        POST,
        SHARE,
    }

    public enum PostCommentType
    {
        PARENT,
        CHILD,
    }

    public enum PostCommentStatus
    {
        DEACTIVE,
        ACTIVE,
    }
    #endregion

    #region Report
    public enum ReportStatus
    {
        PROCCESSING,
        COMPLETE,
        CANCELED,
    }
    public enum ReportType
    {
        INAPPROPRIATE_CONTENT,
        SPAM,
        HARASSMENT,
        VIOLENCE,
        HATE_SPEECH,
        SELF_HARM,
        TERRORISM,
        NUDITY,
        GRAPHIC_CONTENT,
        MISINFORMATION
    }
    public enum ReportCatagory
    {
        POST,
        COMMENT,
        ROOM
    }
    #endregion

    #region Notification 
    public enum NotificationType
    {
        FRIEND_REQUEST,
        MESSAGE_COMMING,
        TRANSACTION_NOTI,
    }

    public enum NotificationStatus
    {
        READ,
        UNREAD,
        DELETE,
    }
    #endregion
}