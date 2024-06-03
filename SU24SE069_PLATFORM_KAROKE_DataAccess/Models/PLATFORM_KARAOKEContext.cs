using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PLATFORM_KARAOKEContext : DbContext
    {
        public PLATFORM_KARAOKEContext()
        {
        }

        public PLATFORM_KARAOKEContext(DbContextOptions<PLATFORM_KARAOKEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountCharacter> AccountCharacters { get; set; } = null!;
        public virtual DbSet<AccountInventoryItem> AccountInventoryItems { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<FavouriteSong> FavouriteSongs { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<InAppTransaction> InAppTransactions { get; set; } = null!;
        public virtual DbSet<InstrumentSheet> InstrumentSheets { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<KaraokeRoom> KaraokeRooms { get; set; } = null!;
        public virtual DbSet<LoginActivity> LoginActivities { get; set; } = null!;
        public virtual DbSet<Lyric> Lyrics { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MoneyTransaction> MoneyTransactions { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PostShare> PostShares { get; set; } = null!;
        public virtual DbSet<PostVote> PostVotes { get; set; } = null!;
        public virtual DbSet<PurchasedSong> PurchasedSongs { get; set; } = null!;
        public virtual DbSet<Recording> Recordings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<VoiceAudio> VoiceAudios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=database.monoinfinity.net;database=PLATFORM_KARAOKE;Uid=sa;Pwd=1234567890Aa;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.AccountId, "UQ__Account__46A222CC0372D537")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Account__7C9273C40D371C5F")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Account__AB6E616440B88E80")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .HasColumnName("account_name");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("created_time");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(150)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdentityCardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("identity_card_number");

                entity.Property(e => e.IsOnline).HasColumnName("is_online");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.Yob).HasColumnName("yob");
            });

            modelBuilder.Entity<AccountCharacter>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CharacterId })
                    .HasName("PK__AccountC__B7BF549FEC939403");

                entity.ToTable("AccountCharacter");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CharacterId).HasColumnName("character_id");
            });

            modelBuilder.Entity<AccountInventoryItem>(entity =>
            {
                entity.ToTable("AccountInventoryItem");

                entity.HasIndex(e => e.AccountInventoryItemId, "UQ__AccountI__3C30841E34E39FEC")
                    .IsUnique();

                entity.Property(e => e.AccountInventoryItemId)
                    .HasColumnName("account_inventory_item_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActivateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("activate_date");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.HasIndex(e => e.ConversationId, "UQ__Conversa__311E7E9BEE3C2821")
                    .IsUnique();

                entity.Property(e => e.ConversationId)
                    .HasColumnName("conversation_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConversationType).HasColumnName("conversation_type");

                entity.Property(e => e.MemberId1).HasColumnName("member_id_1");

                entity.Property(e => e.MemberId2).HasColumnName("member_id_2");

                entity.Property(e => e.SupportRequestId).HasColumnName("support_request_id");
            });

            modelBuilder.Entity<FavouriteSong>(entity =>
            {
                entity.HasKey(e => e.FavouriteId)
                    .HasName("PK__Favourit__B3E742CE9F2BE9DE");

                entity.ToTable("FavouriteSong");

                entity.HasIndex(e => e.FavouriteId, "UQ__Favourit__B3E742CF1BFE1543")
                    .IsUnique();

                entity.Property(e => e.FavouriteId)
                    .HasColumnName("favourite_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.SenderId, e.ReceiverId })
                    .HasName("PK__Friend__39A74E2FDEEB87AE");

                entity.ToTable("Friend");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<InAppTransaction>(entity =>
            {
                entity.ToTable("InAppTransaction");

                entity.HasIndex(e => e.InAppTransactionId, "UQ__InAppTra__783D788F38A01030")
                    .IsUnique();

                entity.Property(e => e.InAppTransactionId)
                    .HasColumnName("in_app_transaction_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");

                entity.Property(e => e.StarAmount)
                    .HasColumnType("money")
                    .HasColumnName("star_amount");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
            });

            modelBuilder.Entity<InstrumentSheet>(entity =>
            {
                entity.ToTable("InstrumentSheet");

                entity.HasIndex(e => e.InstrumentSheetId, "UQ__Instrume__B44A38C35BF41994")
                    .IsUnique();

                entity.Property(e => e.InstrumentSheetId)
                    .HasColumnName("instrument_sheet_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.InstrumentSheetContent)
                    .HasColumnType("text")
                    .HasColumnName("instrument_sheet_content");

                entity.Property(e => e.InstrumentType).HasColumnName("instrument_type");

                entity.Property(e => e.SongId).HasColumnName("song_id");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasIndex(e => e.ItemCode, "UQ__Item__4A67201ECF647929")
                    .IsUnique();

                entity.HasIndex(e => e.ItemId, "UQ__Item__52020FDC5D2EF0E0")
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CanExpire).HasColumnName("can_expire");

                entity.Property(e => e.CanStack).HasColumnName("can_stack");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("item_code");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(250)
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemPrice)
                    .HasColumnType("money")
                    .HasColumnName("item_price");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.ItemType).HasColumnName("item_type");
            });

            modelBuilder.Entity<KaraokeRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__KaraokeR__19675A8A1B3185AD");

                entity.ToTable("KaraokeRoom");

                entity.HasIndex(e => e.RoomId, "UQ__KaraokeR__19675A8B84146B38")
                    .IsUnique();

                entity.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.RoomLog)
                    .HasColumnType("text")
                    .HasColumnName("room_log");
            });

            modelBuilder.Entity<LoginActivity>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__LoginAct__C2C971DB4FD13A29");

                entity.ToTable("LoginActivity");

                entity.HasIndex(e => e.LoginId, "UQ__LoginAct__C2C971DA48A055B2")
                    .IsUnique();

                entity.Property(e => e.LoginId)
                    .HasColumnName("login_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LoginDevice)
                    .HasMaxLength(150)
                    .HasColumnName("login_device");

                entity.Property(e => e.LoginTime)
                    .HasColumnType("datetime")
                    .HasColumnName("login_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");
            });

            modelBuilder.Entity<Lyric>(entity =>
            {
                entity.HasKey(e => e.LyricSheetId)
                    .HasName("PK__Lyric__AAAC3784A109C8A6");

                entity.ToTable("Lyric");

                entity.HasIndex(e => e.LyricSheetId, "UQ__Lyric__AAAC3785FD89FE5F")
                    .IsUnique();

                entity.Property(e => e.LyricSheetId)
                    .HasColumnName("lyric_sheet_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LyricSheetContent)
                    .HasColumnType("text")
                    .HasColumnName("lyric_sheet_content");

                entity.Property(e => e.SongId).HasColumnName("song_id");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.MessageId, "UQ__Message__0BBF6EE79EC144F5")
                    .IsUnique();

                entity.Property(e => e.MessageId)
                    .HasColumnName("message_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.ConversationId).HasColumnName("conversation_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("time_stamp");
            });

            modelBuilder.Entity<MoneyTransaction>(entity =>
            {
                entity.ToTable("MoneyTransaction");

                entity.HasIndex(e => e.MoneyTransactionId, "UQ__MoneyTra__EC443D7DD1B1FB64")
                    .IsUnique();

                entity.Property(e => e.MoneyTransactionId)
                    .HasColumnName("money_transaction_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Currency)
                    .HasColumnType("text")
                    .HasColumnName("currency");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("money")
                    .HasColumnName("money_amount");

                entity.Property(e => e.PackageId).HasColumnName("package_id");

                entity.Property(e => e.PaymentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("payment_code");

                entity.Property(e => e.PaymentType).HasColumnName("payment_type");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.HasIndex(e => e.PackageId, "UQ__Package__63846AE905A50AF7")
                    .IsUnique();

                entity.Property(e => e.PackageId)
                    .HasColumnName("package_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("money")
                    .HasColumnName("money_amount");

                entity.Property(e => e.PackageName)
                    .HasMaxLength(250)
                    .HasColumnName("package_name");

                entity.Property(e => e.StarNumber).HasColumnName("star_number");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.PostId, "UQ__Post__3ED7876790C0F2DB")
                    .IsUnique();

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caption)
                    .HasColumnType("text")
                    .HasColumnName("caption");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.RecordingId).HasColumnName("recording_id");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__PostComm__E7957687BDC979F7");

                entity.ToTable("PostComment");

                entity.HasIndex(e => e.CommentId, "UQ__PostComm__E7957686B3AEB33E")
                    .IsUnique();

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");
            });

            modelBuilder.Entity<PostShare>(entity =>
            {
                entity.ToTable("PostShare");

                entity.HasIndex(e => e.PostShareId, "UQ__PostShar__6F03FC2032916E39")
                    .IsUnique();

                entity.Property(e => e.PostShareId)
                    .HasColumnName("post_share_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caption)
                    .HasColumnType("text")
                    .HasColumnName("caption");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.ShareTime)
                    .HasColumnType("datetime")
                    .HasColumnName("share_time");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");
            });

            modelBuilder.Entity<PostVote>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.PostId })
                    .HasName("PK__PostVote__C176FD421A1EEAD3");

                entity.ToTable("PostVote");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.VoteType).HasColumnName("vote_type");
            });

            modelBuilder.Entity<PurchasedSong>(entity =>
            {
                entity.ToTable("PurchasedSong");

                entity.HasIndex(e => e.PurchasedSongId, "UQ__Purchase__12FEA5F36BE187C3")
                    .IsUnique();

                entity.Property(e => e.PurchasedSongId)
                    .HasColumnName("purchased_song_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");
            });

            modelBuilder.Entity<Recording>(entity =>
            {
                entity.ToTable("Recording");

                entity.HasIndex(e => e.RecordingId, "UQ__Recordin__0C5B24E48F564281")
                    .IsUnique();

                entity.Property(e => e.RecordingId)
                    .HasColumnName("recording_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.KaraokeRoomId).HasColumnName("karaoke_room_id");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.RecordingName)
                    .HasMaxLength(150)
                    .HasColumnName("recording_name");

                entity.Property(e => e.RecordingType).HasColumnName("recording_type");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.HasIndex(e => e.ReportId, "UQ__Report__779B7C59DFAEEF57")
                    .IsUnique();

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportCategory).HasColumnName("report_category");

                entity.Property(e => e.ReportType).HasColumnName("report_type");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reported_account_id");

                entity.Property(e => e.ReporterId).HasColumnName("reporter_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.HasIndex(e => e.SongId, "UQ__Song__A535AE1D841F900F")
                    .IsUnique();

                entity.Property(e => e.SongId)
                    .HasColumnName("song_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SongType).HasColumnName("song_type");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.PublicDate)
                    .HasColumnType("datetime")
                    .HasColumnName("public_date");

                entity.Property(e => e.SongCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("song_code");

                entity.Property(e => e.SongDescription)
                    .HasMaxLength(500)
                    .HasColumnName("song_description");

                entity.Property(e => e.SongName)
                    .HasMaxLength(250)
                    .HasColumnName("song_name");

                entity.Property(e => e.SongStatus).HasColumnName("song_status");

                entity.Property(e => e.SongUrl)
                    .HasColumnType("text")
                    .HasColumnName("song_url");

                entity.Property(e => e.Source).HasColumnName("source");

                entity.Property(e => e.Tempo).HasColumnName("tempo");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SupportR__18D3B90FA499A25E");

                entity.ToTable("SupportRequest");

                entity.HasIndex(e => e.RequestId, "UQ__SupportR__18D3B90EEABB5AF2")
                    .IsUnique();

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Problem)
                    .HasColumnType("text")
                    .HasColumnName("problem");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<VoiceAudio>(entity =>
            {
                entity.HasKey(e => e.VoiceId)
                    .HasName("PK__VoiceAud__128AF3816B244F5F");

                entity.ToTable("VoiceAudio");

                entity.HasIndex(e => e.VoiceId, "UQ__VoiceAud__128AF3806DDA3CFA")
                    .IsUnique();

                entity.Property(e => e.VoiceId)
                    .HasColumnName("voice_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DurationSecond).HasColumnName("duration_second");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Pitch).HasColumnName("pitch");

                entity.Property(e => e.RecordingId).HasColumnName("recording_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");

                entity.Property(e => e.VoiceUrl)
                    .HasColumnType("text")
                    .HasColumnName("voice_url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
