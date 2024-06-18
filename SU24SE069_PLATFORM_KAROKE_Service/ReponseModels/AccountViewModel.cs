using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels
{
    public class AccountViewModel
    {
        //public AccountViewModel()
        //{
        //    AccountInventoryItems = new HashSet<AccountInventoryItem>();
        //    ConversationMemberId1Navigations = new HashSet<Conversation>();
        //    ConversationMemberId2Navigations = new HashSet<Conversation>();
        //    FavouriteSongs = new HashSet<FavouriteSong>();
        //    FriendReceivers = new HashSet<Friend>();
        //    FriendSenders = new HashSet<Friend>();
        //    InAppTransactions = new HashSet<InAppTransaction>();
        //    Items = new HashSet<Item>();
        //    KaraokeRooms = new HashSet<KaraokeRoom>();
        //    LoginActivities = new HashSet<LoginActivity>();
        //    Messages = new HashSet<Message>();
        //    MoneyTransactions = new HashSet<MoneyTransaction>();
        //    Packages = new HashSet<Package>();
        //    PostComments = new HashSet<PostComment>();
        //    PostShares = new HashSet<PostShare>();
        //    PostVotes = new HashSet<PostVote>();
        //    Posts = new HashSet<Post>();
        //    PurchasedSongs = new HashSet<PurchasedSong>();
        //    RecordingHosts = new HashSet<Recording>();
        //    RecordingOwners = new HashSet<Recording>();
        //    ReportReportedAccounts = new HashSet<Report>();
        //    ReportReporters = new HashSet<Report>();
        //    Songs = new HashSet<Song>();
        //    SupportRequests = new HashSet<SupportRequest>();
        //    VoiceAudios = new HashSet<VoiceAudio>();
        //}

        public Guid? AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; } 
        public AccountGender? Gender { get; set; }
        //public string? AccountName { get; set; } 
        public AccountRole? Role { get; set; }
        public int? Star { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus? AccountStatus { get; set; }

        //public ICollection<AccountInventoryItem>? AccountInventoryItems { get; set; }
        //public ICollection<Conversation>? ConversationMemberId1Navigations { get; set; }
        //public ICollection<Conversation>? ConversationMemberId2Navigations { get; set; }
        //public ICollection<FavouriteSong>? FavouriteSongs { get; set; }
        //public ICollection<Friend>? FriendReceivers { get; set; }
        //public ICollection<Friend>? FriendSenders { get; set; }
        //public ICollection<InAppTransaction>? InAppTransactions { get; set; }
        //public ICollection<Item>? Items { get; set; }
        //public ICollection<KaraokeRoom>? KaraokeRooms { get; set; }
        //public ICollection<LoginActivity>? LoginActivities { get; set; }
        //public ICollection<Message>? Messages { get; set; }
        //public ICollection<MoneyTransaction>? MoneyTransactions { get; set; }
        //public ICollection<Package>? Packages { get; set; }
        //public ICollection<PostComment>? PostComments { get; set; }
        //public ICollection<PostShare>? PostShares { get; set; }
        //public ICollection<PostVote>? PostVotes { get; set; }
        //public ICollection<Post>? Posts { get; set; }
        //public ICollection<PurchasedSong>? PurchasedSongs { get; set; }
        //public ICollection<Recording>? RecordingHosts { get; set; }
        //public ICollection<Recording>? RecordingOwners { get; set; }
        //public ICollection<Report>? ReportReportedAccounts { get; set; }
        //public ICollection<Report>? ReportReporters { get; set; }
        //public ICollection<Song>? Songs { get; set; }
        //public ICollection<SupportRequest>? SupportRequests { get; set; }
        //public ICollection<VoiceAudio>? VoiceAudios { get; set; }
    }
}
