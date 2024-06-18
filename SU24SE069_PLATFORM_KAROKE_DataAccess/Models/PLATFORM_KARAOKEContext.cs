using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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
        public virtual DbSet<FavouriteSong> FavouriteSongs { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<InAppTransaction> InAppTransactions { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<KaraokeRoom> KaraokeRooms { get; set; } = null!;
        public virtual DbSet<LoginActivity> LoginActivities { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MoneyTransaction> MoneyTransactions { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostShare> PostShares { get; set; } = null!;
        public virtual DbSet<PostRate> PostRates { get; set; } = null!;
        public virtual DbSet<PurchasedSong> PurchasedSongs { get; set; } = null!;
        public virtual DbSet<Recording> Recordings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<VoiceAudio> VoiceAudios { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS01;Initial Catalog=Kok-DB;Uid=sa;Pwd=1234;TrustServerCertificate=true");
        //        //optionsBuilder.UseSqlServer("Server=gible-db.database.windows.net;Initial Catalog=Kok-DB;Uid=gible-db-sa;Pwd=G!ble87654321;TrustServerCertificate=true");
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {


                optionsBuilder.UseSqlServer(GetConnectionString());
                optionsBuilder.UseLazyLoadingProxies();

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    // Đóng kết nối hiện tại nếu đang mở
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();


                }


            }

        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config.GetConnectionString("Database");
            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.AccountId, "UQ__Account__46A222CCB95F54B5")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Account__7C9273C4BE4FEFD8")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Account__AB6E6164AF554E58")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountStatus).HasColumnName("account_status");

                entity.Property(e => e.CharacterItemId).HasColumnName("character_item_id");

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

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.RoomItemId).HasColumnName("room_item_id");

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.Yob).HasColumnName("yob");

                entity.HasOne(d => d.CharacterItem)
                    .WithMany(p => p.AccountCharacterItems)
                    .HasForeignKey(d => d.CharacterItemId)
                    .HasConstraintName("FK__Account__charact__7C4F7684");

                entity.HasOne(d => d.RoomItem)
                    .WithMany(p => p.AccountRoomItems)
                    .HasForeignKey(d => d.RoomItemId)
                    .HasConstraintName("FK__Account__room_it__7D439ABD");
            });

            modelBuilder.Entity<AccountInventoryItem>(entity =>
            {
                entity.ToTable("AccountInventoryItem");

                entity.HasIndex(e => e.AccountInventoryItemId, "UQ__AccountI__3C30841ED7833689")
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

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.AccountInventoryItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountIn__item___7E37BEF6");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.AccountInventoryItems)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountIn__membe__7F2BE32F");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.HasIndex(e => e.ConversationId, "UQ__Conversa__311E7E9B9619D04A")
                    .IsUnique();

                entity.Property(e => e.ConversationId)
                    .HasColumnName("conversation_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConversationType).HasColumnName("conversation_type");

                entity.Property(e => e.MemberId1).HasColumnName("member_id_1");

                entity.Property(e => e.MemberId2).HasColumnName("member_id_2");

                entity.Property(e => e.SupportRequestId).HasColumnName("support_request_id");

                entity.HasOne(d => d.MemberId1Navigation)
                    .WithMany(p => p.ConversationMemberId1Navigations)
                    .HasForeignKey(d => d.MemberId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__membe__00200768");

                entity.HasOne(d => d.MemberId2Navigation)
                    .WithMany(p => p.ConversationMemberId2Navigations)
                    .HasForeignKey(d => d.MemberId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__membe__01142BA1");

                entity.HasOne(d => d.SupportRequest)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.SupportRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__suppo__02084FDA");
            });

            modelBuilder.Entity<FavouriteSong>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.SongId })
                    .HasName("PK__Favourit__68C8DFD514CDDEC7");

                entity.ToTable("FavouriteSong");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");


                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FavouriteSongs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__membe__02FC7413");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.FavouriteSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__song___03F0984C");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.SenderId, e.ReceiverId })
                    .HasName("PK__Friend__39A74E2FEA2EB197");

                entity.ToTable("Friend");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.FriendReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Friend__receiver__04E4BC85");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.FriendSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Friend__sender_i__05D8E0BE");
            });

            modelBuilder.Entity<InAppTransaction>(entity =>
            {
                entity.ToTable("InAppTransaction");

                entity.HasIndex(e => e.InAppTransactionId, "UQ__InAppTra__783D788F3120721B")
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

                entity.Property(e => e.StarAmount)
                    .HasColumnType("money")
                    .HasColumnName("star_amount");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__item___06CD04F7");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__membe__07C12930");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__song___08B54D69");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasIndex(e => e.ItemCode, "UQ__Item__4A67201E86BAC7DD")
                    .IsUnique();

                entity.HasIndex(e => e.ItemId, "UQ__Item__52020FDCCC7458B7")
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
                
                entity.Property(e => e.PrefabCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("prefab_code");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(250)
                    .HasColumnName("item_description");

                entity.Property(e => e.PrefabCode)
                      .HasMaxLength(20)
                      .HasColumnName("prefab_code");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemPrice)
                    .HasColumnType("money")
                    .HasColumnName("item_price");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.ItemType).HasColumnName("item_type");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK__Item__creator_id__0A9D95DB");
            });

            modelBuilder.Entity<KaraokeRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__KaraokeR__19675A8A6BA327E4");

                entity.ToTable("KaraokeRoom");

                entity.HasIndex(e => e.RoomId, "UQ__KaraokeR__19675A8BC5A9ED1B")
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

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.KaraokeRooms)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KaraokeRo__creat__0B91BA14");
            });

            modelBuilder.Entity<LoginActivity>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__LoginAct__C2C971DB811BF7A3");

                entity.ToTable("LoginActivity");

                entity.HasIndex(e => e.LoginId, "UQ__LoginAct__C2C971DA5DBB7ACA")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.LoginActivities)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LoginActi__membe__0C85DE4D");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.MessageId, "UQ__Message__0BBF6EE78703A3BB")
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

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__convers__0E6E26BF");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__sender___0F624AF8");
            });

            modelBuilder.Entity<MoneyTransaction>(entity =>
            {
                entity.ToTable("MoneyTransaction");

                entity.HasIndex(e => e.MoneyTransactionId, "UQ__MoneyTra__EC443D7DCAA4141D")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MoneyTransactions)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MoneyTran__membe__10566F31");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.MoneyTransactions)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MoneyTran__packa__114A936A");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.HasIndex(e => e.PackageId, "UQ__Package__63846AE9D3373BE0")
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

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Package__creator__123EB7A3");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.PostId, "UQ__Post__3ED78767E3F1F3DF")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__member_id__1332DBDC");

                entity.HasOne(d => d.Recording)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.RecordingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__recording___14270015");
            });

            modelBuilder.Entity<PostShare>(entity =>
            {
                entity.ToTable("PostShare");

                entity.HasIndex(e => e.PostShareId, "UQ__PostShar__6F03FC20AE6B0180")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostShares)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostShare__membe__17036CC0");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostShares)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostShare__post___17F790F9");
            });

            modelBuilder.Entity<PostRate>(entity =>
            {
                entity.HasKey(e => e.RateId )
                    .HasName("PK__PostRate__C176FD426D6D26E0");

                entity.ToTable("PostRate");

                entity.HasIndex(e => e.RateId, "UQ__PostRate__E79576863EE120FB")
                    .IsUnique();

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.VoteType).HasColumnName("vote_type");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostRates)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostVote__member__18EBB532");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostRates)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostVote__post_i__19DFD96B");


            });



            modelBuilder.Entity<PurchasedSong>(entity =>
            {
                entity.ToTable("PurchasedSong");

                entity.HasIndex(e => e.PurchasedSongId, "UQ__Purchase__12FEA5F379BFEF7C")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PurchasedSongs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchased__membe__1AD3FDA4");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.PurchasedSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchased__song___1BC821DD");
            });

            modelBuilder.Entity<Recording>(entity =>
            {
                entity.ToTable("Recording");

                entity.HasIndex(e => e.RecordingId, "UQ__Recordin__0C5B24E46D5CE3E3")
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


                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.RecordingHosts)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__host___1CBC4616");

                entity.HasOne(d => d.KaraokeRoom)
                    .WithMany(p => p.Recordings)
                    .HasForeignKey(d => d.KaraokeRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__karao__1DB06A4F");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.RecordingOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__owner__1EA48E88");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Recordings)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__song___1F98B2C1");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.HasIndex(e => e.ReportId, "UQ__Report__779B7C5917900A24")
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

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__comment___208CD6FA");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__post_id__2180FB33");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__22751F6C");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__236943A5");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__room_id__245D67DE");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.HasIndex(e => e.SongCode, "UQ__Song__43F33A39E8877F76")
                    .IsUnique();

                entity.HasIndex(e => e.SongId, "UQ__Song__A535AE1D1351FFFF")
                    .IsUnique();

                entity.Property(e => e.SongId)
                    .HasColumnName("song_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Author)
                    .HasMaxLength(150)
                    .HasColumnName("author");

                entity.Property(e => e.Category)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("category");

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

                entity.Property(e => e.Singer)
                    .HasMaxLength(150)
                    .HasColumnName("singer");

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

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Song__creator_id__25518C17");
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SupportR__18D3B90FC2899572");

                entity.ToTable("SupportRequest");

                entity.HasIndex(e => e.RequestId, "UQ__SupportR__18D3B90EB565E032")
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

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportRe__sende__2645B050");
            });

            modelBuilder.Entity<VoiceAudio>(entity =>
            {
                entity.HasKey(e => e.VoiceId)
                    .HasName("PK__VoiceAud__128AF381A07F9D92");

                entity.ToTable("VoiceAudio");

                entity.HasIndex(e => e.VoiceId, "UQ__VoiceAud__128AF3808BA35F61")
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

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.VoiceAudios)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceAudi__membe__2739D489");

                entity.HasOne(d => d.Recording)
                    .WithMany(p => p.VoiceAudios)
                    .HasForeignKey(d => d.RecordingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceAudi__recor__282DF8C2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
