using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Migrations
{
    public partial class migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    user_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    star = table.Column<int>(type: "money", nullable: false),
                    is_online = table.Column<bool>(type: "bit", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    yob = table.Column<int>(type: "int", nullable: true),
                    identity_card_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    character_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    room_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    account_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    sender_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    receiver_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Friend__39A74E2FEA2EB197", x => new { x.sender_id, x.receiver_id });
                    table.ForeignKey(
                        name: "FK__Friend__receiver__04E4BC85",
                        column: x => x.receiver_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Friend__sender_i__05D8E0BE",
                        column: x => x.sender_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    item_code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    item_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    item_description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    item_type = table.Column<int>(type: "int", nullable: false),
                    item_price = table.Column<decimal>(type: "money", nullable: false),
                    item_status = table.Column<int>(type: "int", nullable: false),
                    can_expire = table.Column<bool>(type: "bit", nullable: true),
                    can_stack = table.Column<bool>(type: "bit", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    prefab_code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.item_id);
                    table.ForeignKey(
                        name: "FK__Item__creator_id__0A9D95DB",
                        column: x => x.creator_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "KaraokeRoom",
                columns: table => new
                {
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    room_log = table.Column<string>(type: "text", nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KaraokeR__19675A8A6BA327E4", x => x.room_id);
                    table.ForeignKey(
                        name: "FK__KaraokeRo__creat__0B91BA14",
                        column: x => x.creator_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "LoginActivity",
                columns: table => new
                {
                    login_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    login_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    login_device = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoginAct__C2C971DB811BF7A3", x => x.login_id);
                    table.ForeignKey(
                        name: "FK__LoginActi__membe__0C85DE4D",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    package_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    money_amount = table.Column<decimal>(type: "money", nullable: true),
                    star_number = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.package_id);
                    table.ForeignKey(
                        name: "FK__Package__creator__123EB7A3",
                        column: x => x.creator_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    song_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    song_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    song_url = table.Column<string>(type: "text", nullable: true),
                    song_status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    song_code = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    public_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    category = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    author = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    singer = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.song_id);
                    table.ForeignKey(
                        name: "FK__Song__creator_id__25518C17",
                        column: x => x.creator_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "SupportRequest",
                columns: table => new
                {
                    request_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    problem = table.Column<string>(type: "text", nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    sender_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SupportR__18D3B90FC2899572", x => x.request_id);
                    table.ForeignKey(
                        name: "FK__SupportRe__sende__2645B050",
                        column: x => x.sender_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "AccountInventoryItem",
                columns: table => new
                {
                    account_inventory_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    item_status = table.Column<int>(type: "int", nullable: false),
                    activate_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInventoryItem", x => x.account_inventory_item_id);
                    table.ForeignKey(
                        name: "FK__AccountIn__item___7E37BEF6",
                        column: x => x.item_id,
                        principalTable: "Item",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "FK__AccountIn__membe__7F2BE32F",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "MoneyTransaction",
                columns: table => new
                {
                    money_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    payment_type = table.Column<int>(type: "int", nullable: false),
                    payment_code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    money_amount = table.Column<decimal>(type: "money", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransaction", x => x.money_transaction_id);
                    table.ForeignKey(
                        name: "FK__MoneyTran__membe__10566F31",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__MoneyTran__packa__114A936A",
                        column: x => x.package_id,
                        principalTable: "Package",
                        principalColumn: "package_id");
                });

            migrationBuilder.CreateTable(
                name: "FavouriteSong",
                columns: table => new
                {
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favourit__68C8DFD514CDDEC7", x => new { x.member_id, x.song_id });
                    table.ForeignKey(
                        name: "FK__Favourite__membe__02FC7413",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Favourite__song___03F0984C",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "InAppTransaction",
                columns: table => new
                {
                    in_app_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    star_amount = table.Column<decimal>(type: "money", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    transaction_type = table.Column<int>(type: "int", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InAppTransaction", x => x.in_app_transaction_id);
                    table.ForeignKey(
                        name: "FK__InAppTran__item___06CD04F7",
                        column: x => x.item_id,
                        principalTable: "Item",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "FK__InAppTran__membe__07C12930",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__InAppTran__song___08B54D69",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasedSong",
                columns: table => new
                {
                    purchased_song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    purchase_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    song_type = table.Column<int>(type: "int", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedSong", x => x.purchased_song_id);
                    table.ForeignKey(
                        name: "FK__Purchased__membe__1AD3FDA4",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Purchased__song___1BC821DD",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "Recording",
                columns: table => new
                {
                    recording_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    recording_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    recording_type = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    host_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    owner_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    karaoke_room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recording", x => x.recording_id);
                    table.ForeignKey(
                        name: "FK__Recording__host___1CBC4616",
                        column: x => x.host_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Recording__karao__1DB06A4F",
                        column: x => x.karaoke_room_id,
                        principalTable: "KaraokeRoom",
                        principalColumn: "room_id");
                    table.ForeignKey(
                        name: "FK__Recording__owner__1EA48E88",
                        column: x => x.owner_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Recording__song___1F98B2C1",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    conversation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    member_id_1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id_2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    conversation_type = table.Column<int>(type: "int", nullable: false),
                    support_request_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.conversation_id);
                    table.ForeignKey(
                        name: "FK__Conversat__membe__00200768",
                        column: x => x.member_id_1,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Conversat__membe__01142BA1",
                        column: x => x.member_id_2,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Conversat__suppo__02084FDA",
                        column: x => x.support_request_id,
                        principalTable: "SupportRequest",
                        principalColumn: "request_id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    caption = table.Column<string>(type: "text", nullable: true),
                    upload_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recording_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.post_id);
                    table.ForeignKey(
                        name: "FK__Post__member_id__1332DBDC",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Post__recording___14270015",
                        column: x => x.recording_id,
                        principalTable: "Recording",
                        principalColumn: "recording_id");
                });

            migrationBuilder.CreateTable(
                name: "VoiceAudio",
                columns: table => new
                {
                    voice_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    voice_url = table.Column<string>(type: "text", nullable: false),
                    duration_second = table.Column<double>(type: "float", nullable: false),
                    upload_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    pitch = table.Column<int>(type: "int", nullable: false),
                    recording_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoiceAud__128AF381A07F9D92", x => x.voice_id);
                    table.ForeignKey(
                        name: "FK__VoiceAudi__membe__2739D489",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__VoiceAudi__recor__282DF8C2",
                        column: x => x.recording_id,
                        principalTable: "Recording",
                        principalColumn: "recording_id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    content = table.Column<string>(type: "text", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    sender_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__Message__convers__0E6E26BF",
                        column: x => x.conversation_id,
                        principalTable: "Conversation",
                        principalColumn: "conversation_id");
                    table.ForeignKey(
                        name: "FK__Message__sender___0F624AF8",
                        column: x => x.sender_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "PostRate",
                columns: table => new
                {
                    RateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vote_type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PostRate__C176FD426D6D26E0", x => x.RateId);
                    table.ForeignKey(
                        name: "FK__PostVote__member__18EBB532",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__PostVote__post_i__19DFD96B",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "post_id");
                });

            migrationBuilder.CreateTable(
                name: "PostShare",
                columns: table => new
                {
                    post_share_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    caption = table.Column<string>(type: "text", nullable: true),
                    share_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostShare", x => x.post_share_id);
                    table.ForeignKey(
                        name: "FK__PostShare__membe__17036CC0",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__PostShare__post___17F790F9",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "post_id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    report_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    reporter_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reported_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    report_category = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    report_type = table.Column<int>(type: "int", nullable: false),
                    comment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.report_id);
                    table.ForeignKey(
                        name: "FK__Report__comment___208CD6FA",
                        column: x => x.comment_id,
                        principalTable: "PostRate",
                        principalColumn: "RateId");
                    table.ForeignKey(
                        name: "FK__Report__post_id__2180FB33",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "post_id");
                    table.ForeignKey(
                        name: "FK__Report__reported__22751F6C",
                        column: x => x.reported_account_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Report__reporter__236943A5",
                        column: x => x.reporter_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK__Report__room_id__245D67DE",
                        column: x => x.room_id,
                        principalTable: "KaraokeRoom",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_character_item_id",
                table: "Account",
                column: "character_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_room_item_id",
                table: "Account",
                column: "room_item_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Account__46A222CCB95F54B5",
                table: "Account",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__7C9273C4BE4FEFD8",
                table: "Account",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__AB6E6164AF554E58",
                table: "Account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountInventoryItem_item_id",
                table: "AccountInventoryItem",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountInventoryItem_member_id",
                table: "AccountInventoryItem",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "UQ__AccountI__3C30841ED7833689",
                table: "AccountInventoryItem",
                column: "account_inventory_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_member_id_1",
                table: "Conversation",
                column: "member_id_1");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_member_id_2",
                table: "Conversation",
                column: "member_id_2");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_support_request_id",
                table: "Conversation",
                column: "support_request_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Conversa__311E7E9B9619D04A",
                table: "Conversation",
                column: "conversation_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteSong_song_id",
                table: "FavouriteSong",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_receiver_id",
                table: "Friend",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_InAppTransaction_item_id",
                table: "InAppTransaction",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_InAppTransaction_member_id",
                table: "InAppTransaction",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_InAppTransaction_song_id",
                table: "InAppTransaction",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "UQ__InAppTra__783D788F3120721B",
                table: "InAppTransaction",
                column: "in_app_transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_creator_id",
                table: "Item",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Item__4A67201E86BAC7DD",
                table: "Item",
                column: "item_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Item__52020FDCCC7458B7",
                table: "Item",
                column: "item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeRoom_creator_id",
                table: "KaraokeRoom",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__KaraokeR__19675A8BC5A9ED1B",
                table: "KaraokeRoom",
                column: "room_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginActivity_member_id",
                table: "LoginActivity",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "UQ__LoginAct__C2C971DA5DBB7ACA",
                table: "LoginActivity",
                column: "login_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_conversation_id",
                table: "Message",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_sender_id",
                table: "Message",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Message__0BBF6EE78703A3BB",
                table: "Message",
                column: "message_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransaction_member_id",
                table: "MoneyTransaction",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransaction_package_id",
                table: "MoneyTransaction",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "UQ__MoneyTra__EC443D7DCAA4141D",
                table: "MoneyTransaction",
                column: "money_transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_creator_id",
                table: "Package",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Package__63846AE9D3373BE0",
                table: "Package",
                column: "package_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_member_id",
                table: "Post",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_recording_id",
                table: "Post",
                column: "recording_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Post__3ED78767E3F1F3DF",
                table: "Post",
                column: "post_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostRate_member_id",
                table: "PostRate",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostRate_post_id",
                table: "PostRate",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "UQ__PostRate__E79576863EE120FB",
                table: "PostRate",
                column: "RateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostShare_member_id",
                table: "PostShare",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostShare_post_id",
                table: "PostShare",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "UQ__PostShar__6F03FC20AE6B0180",
                table: "PostShare",
                column: "post_share_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSong_member_id",
                table: "PurchasedSong",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSong_song_id",
                table: "PurchasedSong",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Purchase__12FEA5F379BFEF7C",
                table: "PurchasedSong",
                column: "purchased_song_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recording_host_id",
                table: "Recording",
                column: "host_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recording_karaoke_room_id",
                table: "Recording",
                column: "karaoke_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recording_owner_id",
                table: "Recording",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recording_song_id",
                table: "Recording",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Recordin__0C5B24E46D5CE3E3",
                table: "Recording",
                column: "recording_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_comment_id",
                table: "Report",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_post_id",
                table: "Report",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reported_account_id",
                table: "Report",
                column: "reported_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reporter_id",
                table: "Report",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_room_id",
                table: "Report",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Report__779B7C5917900A24",
                table: "Report",
                column: "report_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_creator_id",
                table: "Song",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Song__43F33A39E8877F76",
                table: "Song",
                column: "song_code",
                unique: true,
                filter: "[song_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Song__A535AE1D1351FFFF",
                table: "Song",
                column: "song_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportRequest_sender_id",
                table: "SupportRequest",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "UQ__SupportR__18D3B90EB565E032",
                table: "SupportRequest",
                column: "request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoiceAudio_member_id",
                table: "VoiceAudio",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceAudio_recording_id",
                table: "VoiceAudio",
                column: "recording_id");

            migrationBuilder.CreateIndex(
                name: "UQ__VoiceAud__128AF3808BA35F61",
                table: "VoiceAudio",
                column: "voice_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Account__charact__7C4F7684",
                table: "Account",
                column: "character_item_id",
                principalTable: "AccountInventoryItem",
                principalColumn: "account_inventory_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Account__room_it__7D439ABD",
                table: "Account",
                column: "room_item_id",
                principalTable: "AccountInventoryItem",
                principalColumn: "account_inventory_item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Account__charact__7C4F7684",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK__Account__room_it__7D439ABD",
                table: "Account");

            migrationBuilder.DropTable(
                name: "FavouriteSong");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "InAppTransaction");

            migrationBuilder.DropTable(
                name: "LoginActivity");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MoneyTransaction");

            migrationBuilder.DropTable(
                name: "PostShare");

            migrationBuilder.DropTable(
                name: "PurchasedSong");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "VoiceAudio");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "PostRate");

            migrationBuilder.DropTable(
                name: "SupportRequest");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Recording");

            migrationBuilder.DropTable(
                name: "KaraokeRoom");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "AccountInventoryItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
