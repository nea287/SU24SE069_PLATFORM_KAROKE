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
        public virtual DbSet<AccountInventoryItem> AccountInventoryItems { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<ExternalSong> ExternalSongs { get; set; } = null!;
        public virtual DbSet<FavouriteSong> FavouriteSongs { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<InAppTransaction> InAppTransactions { get; set; } = null!;
        public virtual DbSet<InstrumentSheet> InstrumentSheets { get; set; } = null!;
        public virtual DbSet<InternalSong> InternalSongs { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<KaraokeRoom> KaraokeRooms { get; set; } = null!;
        public virtual DbSet<LoginActivity> LoginActivities { get; set; } = null!;
        public virtual DbSet<Lyric> Lyrics { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MoneyTransaction> MoneyTransactions { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<Performance> Performances { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PostShare> PostShares { get; set; } = null!;
        public virtual DbSet<PostVote> PostVotes { get; set; } = null!;
        public virtual DbSet<PurchasedSong> PurchasedSongs { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportParticipant> ReportParticipants { get; set; } = null!;
        public virtual DbSet<SongHistory> SongHistories { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<VoiceRecording> VoiceRecordings { get; set; } = null!;

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

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("account_id");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .HasColumnName("account_name");

                entity.Property(e => e.CharacterId).HasColumnName("character_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(150)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdentityCardNumber)
                    .HasMaxLength(50)
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

                entity.Property(e => e.Yob)
                    .HasMaxLength(150)
                    .HasColumnName("yob");
            });

            modelBuilder.Entity<AccountInventoryItem>(entity =>
            {
                entity.ToTable("AccountInventoryItem");

                entity.Property(e => e.AccountInventoryItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("account_inventory_item_id");

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

                entity.Property(e => e.ConversationId)
                    .ValueGeneratedNever()
                    .HasColumnName("conversation_id");

                entity.Property(e => e.ConversationType).HasColumnName("conversation_type");

                entity.Property(e => e.MemberId1).HasColumnName("member_id_1");

                entity.Property(e => e.MemberId2).HasColumnName("member_id_2");

                entity.Property(e => e.SupportRequestId).HasColumnName("support_request_id");
            });

            modelBuilder.Entity<ExternalSong>(entity =>
            {
                entity.HasKey(e => e.SongId);

                entity.ToTable("ExternalSong");

                entity.Property(e => e.SongId)
                    .ValueGeneratedNever()
                    .HasColumnName("song_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

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

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<FavouriteSong>(entity =>
            {
                entity.HasKey(e => e.FavourteId);

                entity.ToTable("FavouriteSong");

                entity.Property(e => e.FavourteId)
                    .ValueGeneratedNever()
                    .HasColumnName("favourte_id");

                entity.Property(e => e.ExternalSongId).HasColumnName("external_song_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.SenderId, e.ReceiverId });

                entity.ToTable("Friend");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<InAppTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("InAppTransaction");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ExternalSongId).HasColumnName("external_song_id");

                entity.Property(e => e.IngameTransactionId).HasColumnName("ingame_transaction_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongType).HasColumnName("song_type");

                entity.Property(e => e.StarAmount).HasColumnName("star_amount");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");
            });

            modelBuilder.Entity<InstrumentSheet>(entity =>
            {
                entity.ToTable("InstrumentSheet");

                entity.Property(e => e.InstrumentSheetId)
                    .ValueGeneratedNever()
                    .HasColumnName("instrument_sheet_id");

                entity.Property(e => e.InstrumentSheetContent)
                    .HasColumnType("text")
                    .HasColumnName("instrument_sheet_content");

                entity.Property(e => e.InstrumentType).HasColumnName("instrument_type");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");
            });

            modelBuilder.Entity<InternalSong>(entity =>
            {
                entity.HasKey(e => e.SongId);

                entity.ToTable("InternalSong");

                entity.Property(e => e.SongId)
                    .ValueGeneratedNever()
                    .HasColumnName("song_id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.PublicDate)
                    .HasColumnType("datetime")
                    .HasColumnName("public_date");

                entity.Property(e => e.SongCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("song_code");

                entity.Property(e => e.SongDescription)
                    .HasMaxLength(500)
                    .HasColumnName("song_description");

                entity.Property(e => e.SongName)
                    .HasMaxLength(250)
                    .HasColumnName("song_name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tempo).HasColumnName("tempo");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("item_id");

                entity.Property(e => e.CanExpire).HasColumnName("can_expire");

                entity.Property(e => e.CanStack).HasColumnName("can_stack");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("item_code")
                    .IsFixedLength();

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(250)
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("item_name")
                    .IsFixedLength();

                entity.Property(e => e.ItemPrice).HasColumnName("item_price");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.ItemType).HasColumnName("item_type");
            });

            modelBuilder.Entity<KaraokeRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("KaraokeRoom");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedNever()
                    .HasColumnName("room_id");

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
                entity.HasKey(e => e.LoginId);

                entity.ToTable("LoginActivity");

                entity.Property(e => e.LoginId)
                    .ValueGeneratedNever()
                    .HasColumnName("login_id");

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
                entity.HasKey(e => e.LyricSheetId);

                entity.ToTable("Lyric");

                entity.Property(e => e.LyricSheetId)
                    .ValueGeneratedNever()
                    .HasColumnName("lyric_sheet_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.LyricSheetContent)
                    .HasColumnType("text")
                    .HasColumnName("lyric_sheet_content");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("message_id");

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

                entity.Property(e => e.MoneyTransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("money_transaction_id");

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

                entity.Property(e => e.PackageId)
                    .ValueGeneratedNever()
                    .HasColumnName("package_id");

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

            modelBuilder.Entity<Performance>(entity =>
            {
                entity.ToTable("Performance");

                entity.Property(e => e.PerformanceId)
                    .ValueGeneratedNever()
                    .HasColumnName("performance_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ExternalSongId).HasColumnName("external_song_id");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.KaraokeRoomId).HasColumnName("karaoke_room_id");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.PerformanceName)
                    .HasMaxLength(150)
                    .HasColumnName("performance_name");

                entity.Property(e => e.PerformanceType).HasColumnName("performance_type");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SongType).HasColumnName("song_type");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId)
                    .ValueGeneratedNever()
                    .HasColumnName("post_id");

                entity.Property(e => e.Caption)
                    .HasColumnType("text")
                    .HasColumnName("caption");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PerformanceId).HasColumnName("performance_id");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("PostComment");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("comment_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.PostId).HasColumnName("post_id");
            });

            modelBuilder.Entity<PostShare>(entity =>
            {
                entity.ToTable("PostShare");

                entity.Property(e => e.PostShareId)
                    .ValueGeneratedNever()
                    .HasColumnName("post_share_id");

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
                entity.HasKey(e => new { e.MemberId, e.PostId });

                entity.ToTable("PostVote");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.VoteType).HasColumnName("vote_type");
            });

            modelBuilder.Entity<PurchasedSong>(entity =>
            {
                entity.ToTable("PurchasedSong");

                entity.Property(e => e.PurchasedSongId)
                    .ValueGeneratedNever()
                    .HasColumnName("purchased_song_id");

                entity.Property(e => e.ExternalSongId).HasColumnName("external_song_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.SongType).HasColumnName("song_type");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId)
                    .ValueGeneratedNever()
                    .HasColumnName("report_id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.ReportCategory).HasColumnName("report_category");

                entity.Property(e => e.ReportType).HasColumnName("report_type");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reported_account_id");

                entity.Property(e => e.ReporterId).HasColumnName("reporter_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ReportParticipant>(entity =>
            {
                entity.HasKey(e => new { e.ReporterId, e.ReportedAccountId, e.RoomId });

                entity.ToTable("ReportParticipant");

                entity.Property(e => e.ReporterId).HasColumnName("reporter_id");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reported_account_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Reason)
                    .HasMaxLength(150)
                    .HasColumnName("reason");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<SongHistory>(entity =>
            {
                entity.ToTable("SongHistory");

                entity.Property(e => e.SongHistoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("song_history_id");

                entity.Property(e => e.ExternalSongId).HasColumnName("external_song_id");

                entity.Property(e => e.InternalSongId).HasColumnName("internal_song_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("sing_date");

                entity.Property(e => e.SongType).HasColumnName("song_type");
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("SupportRequest");

                entity.Property(e => e.RequestId)
                    .ValueGeneratedNever()
                    .HasColumnName("request_id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Problem).HasColumnName("problem");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<VoiceRecording>(entity =>
            {
                entity.HasKey(e => e.RecordingId);

                entity.ToTable("VoiceRecording");

                entity.Property(e => e.RecordingId)
                    .ValueGeneratedNever()
                    .HasColumnName("recording_id");

                entity.Property(e => e.DurationSecond).HasColumnName("duration_second");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PerformanceId).HasColumnName("performance_id");

                entity.Property(e => e.Pitch).HasColumnName("pitch");

                entity.Property(e => e.RecordingUrl)
                    .HasColumnType("text")
                    .HasColumnName("recording_url");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
