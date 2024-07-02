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
                name: "Artist",
                columns: table => new
                {
                    artist_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    artist_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.artist_id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    genre_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "Singer",
                columns: table => new
                {
                    singer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    singer_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singer", x => x.singer_id);
                });

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
                    is_online = table.Column<bool>(type: "bit", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    yob = table.Column<int>(type: "int", nullable: true),
                    identity_card_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    character_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    room_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    account_status = table.Column<int>(type: "int", nullable: true),
                    up_balance = table.Column<decimal>(type: "money", nullable: false)
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
                    item_status = table.Column<int>(type: "int", nullable: false),
                    can_expire = table.Column<bool>(type: "bit", nullable: true),
                    can_stack = table.Column<bool>(type: "bit", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    prefab_code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    item_price = table.Column<decimal>(type: "money", nullable: false),
                    item_sell_price = table.Column<decimal>(type: "money", nullable: false)
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
                    price = table.Column<decimal>(type: "money", nullable: false)
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
                name: "AccountItem",
                columns: table => new
                {
                    account_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    item_status = table.Column<int>(type: "int", nullable: false),
                    activate_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    obtain_method = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountItem", x => x.account_item_id);
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
                name: "MonetaryTransaction",
                columns: table => new
                {
                    monetary_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
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
                    table.PrimaryKey("PK_MonetaryTransaction", x => x.monetary_transaction_id);
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
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "SongArtist",
                columns: table => new
                {
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    artist_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongArtist", x => new { x.song_id, x.artist_id });
                    table.ForeignKey(
                        name: "FK_SongArtist_Artist",
                        column: x => x.artist_id,
                        principalTable: "Artist",
                        principalColumn: "artist_id");
                    table.ForeignKey(
                        name: "FK_SongArtist_Song",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "SongGenre",
                columns: table => new
                {
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    genre_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongGenre", x => new { x.song_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_SongGenre_Genre",
                        column: x => x.genre_id,
                        principalTable: "Genre",
                        principalColumn: "genre_id");
                    table.ForeignKey(
                        name: "FK_SongGenre_Song",
                        column: x => x.song_id,
                        principalTable: "Song",
                        principalColumn: "song_id");
                });

            migrationBuilder.CreateTable(
                name: "SongSinger",
                columns: table => new
                {
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    singer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongSinger", x => new { x.song_id, x.singer_id });
                    table.ForeignKey(
                        name: "FK_SongSinger_Singer",
                        column: x => x.singer_id,
                        principalTable: "Singer",
                        principalColumn: "singer_id");
                    table.ForeignKey(
                        name: "FK_SongSinger_Song",
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
                        principalColumn: "request_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InAppTransaction",
                columns: table => new
                {
                    in_app_transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    transaction_type = table.Column<int>(type: "int", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    up_amount_before = table.Column<decimal>(type: "money", nullable: false),
                    MonetaryTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    up_total_amount = table.Column<decimal>(type: "money", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_InAppTransaction_MonetaryTransaction_MonetaryTransactionId",
                        column: x => x.MonetaryTransactionId,
                        principalTable: "MonetaryTransaction",
                        principalColumn: "monetary_transaction_id",
                        onDelete: ReferentialAction.Cascade);
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
                    recording_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    post_type = table.Column<int>(type: "int", nullable: false),
                    origin_post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Post_Post",
                        column: x => x.origin_post_id,
                        principalTable: "Post",
                        principalColumn: "post_id");
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
                name: "PurchasedSong",
                columns: table => new
                {
                    purchased_song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    purchase_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    song_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InAppTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_PurchasedSong_InAppTransaction_InAppTransactionId",
                        column: x => x.InAppTransactionId,
                        principalTable: "InAppTransaction",
                        principalColumn: "in_app_transaction_id");
                });

            migrationBuilder.CreateTable(
                name: "PostComment",
                columns: table => new
                {
                    comment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comment_type = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    parent_comment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DF_post_comment_id", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_PostComment_Account",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_PostComment_Post_post_id",
                        column: x => x.post_id,
                        principalTable: "Post",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComment_PostComment_parent_comment_id",
                        column: x => x.parent_comment_id,
                        principalTable: "PostComment",
                        principalColumn: "comment_id");
                });

            migrationBuilder.CreateTable(
                name: "PostRating",
                columns: table => new
                {
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRating", x => new { x.member_id, x.post_id });
                    table.ForeignKey(
                        name: "FK_PostRating_Account",
                        column: x => x.member_id,
                        principalTable: "Account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_PostRating_Post",
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
                name: "IX_AccountItem_item_id",
                table: "AccountItem",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountItem_member_id",
                table: "AccountItem",
                column: "member_id");

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
                name: "IX_InAppTransaction_MonetaryTransactionId",
                table: "InAppTransaction",
                column: "MonetaryTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_InAppTransaction_song_id",
                table: "InAppTransaction",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_creator_id",
                table: "Item",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeRoom_creator_id",
                table: "KaraokeRoom",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_LoginActivity_member_id",
                table: "LoginActivity",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_conversation_id",
                table: "Message",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_sender_id",
                table: "Message",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryTransaction_member_id",
                table: "MonetaryTransaction",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryTransaction_package_id",
                table: "MonetaryTransaction",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Package_creator_id",
                table: "Package",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_member_id",
                table: "Post",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_origin_post_id",
                table: "Post",
                column: "origin_post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_recording_id",
                table: "Post",
                column: "recording_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostComment_member_id",
                table: "PostComment",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostComment_parent_comment_id",
                table: "PostComment",
                column: "parent_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostComment_post_id",
                table: "PostComment",
                column: "post_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostRating_post_id",
                table: "PostRating",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostShare_member_id",
                table: "PostShare",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostShare_post_id",
                table: "PostShare",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSong_InAppTransactionId",
                table: "PurchasedSong",
                column: "InAppTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSong_member_id",
                table: "PurchasedSong",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedSong_song_id",
                table: "PurchasedSong",
                column: "song_id");

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
                name: "IX_Song_creator_id",
                table: "Song",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Song__43F33A39E8877F76",
                table: "Song",
                column: "song_code",
                unique: true,
                filter: "([song_code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_SongArtist_artist_id",
                table: "SongArtist",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "IX_SongGenre_genre_id",
                table: "SongGenre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_SongSinger_singer_id",
                table: "SongSinger",
                column: "singer_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupportRequest_sender_id",
                table: "SupportRequest",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceAudio_member_id",
                table: "VoiceAudio",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceAudio_recording_id",
                table: "VoiceAudio",
                column: "recording_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Account__charact__7C4F7684",
                table: "Account",
                column: "character_item_id",
                principalTable: "AccountItem",
                principalColumn: "account_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Account__room_it__7D439ABD",
                table: "Account",
                column: "room_item_id",
                principalTable: "AccountItem",
                principalColumn: "account_item_id");
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
                name: "LoginActivity");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "PostComment");

            migrationBuilder.DropTable(
                name: "PostRating");

            migrationBuilder.DropTable(
                name: "PostShare");

            migrationBuilder.DropTable(
                name: "PurchasedSong");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "SongArtist");

            migrationBuilder.DropTable(
                name: "SongGenre");

            migrationBuilder.DropTable(
                name: "SongSinger");

            migrationBuilder.DropTable(
                name: "VoiceAudio");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "InAppTransaction");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Singer");

            migrationBuilder.DropTable(
                name: "SupportRequest");

            migrationBuilder.DropTable(
                name: "MonetaryTransaction");

            migrationBuilder.DropTable(
                name: "Recording");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "KaraokeRoom");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "AccountItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
