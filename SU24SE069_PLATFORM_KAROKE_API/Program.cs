using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SU24SE069_PLATFORM_KAROKE_API.AppStarts;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using SU24SE069_PLATFORM_KAROKE_Service.Validator;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup;
using SU24SE069_PLATFORM_KAROKE_Service.BackgroundServices;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json and appsettings.{Environment}.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

#region FormFile
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // Set file size limit to 500 MB
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // Set file size limit to 500 MB
});
#endregion

#region Momo
builder.Services.ConfigureOptions<MoMoOptionsSetup>();
#endregion

#region FirebaseStorage
builder.Services.ConfigureOptions<FirebaseCredentialOptionSetup>();
#endregion

#region payOS
builder.Services.ConfigureOptions<PayOSCredentialOptionSetup>();
#endregion

#region AppStarts
builder.Services.ConfigDI();
builder.Services.AddAutoMapper(typeof(AutoMapperResolver));
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SwaggerIgnoreFilter>();
    #region JWT
    //Khai bao bearer token trong swagger
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "Enter your token",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        #region Token Bearer
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
        #endregion
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                securityScheme,
                new string[] {"Bearer"} //new string[] {} Như nhau
            }
        });
    #endregion
});


#region AppStarts
builder.Services.AddAutoMapper(typeof(AutoMapperResolver).Assembly);
builder.Services.ConfigDI();
#endregion

#region Authen and Author

var tokenConfig = builder.Configuration.GetSection("Token");
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // clear default behaviour không ảnh hưởng đến jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ClockSkew = TimeSpan.Zero // remove delay of token when expire
    };
})
.AddJwtBearer("RefreshToken", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["RefreshTokenSecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireRole("ADMIN");
    });
    options.AddPolicy("RequireStaffRole", policy =>
    {
        policy.RequireRole("STAFF");
    });
    options.AddPolicy("RequireStaffOrMember", policy =>
    {
        policy.RequireRole("STAFF", "MEMBER");

    });
    options.AddPolicy("RequiredAdminOrStaff", policy =>
    {
        policy.RequireRole("ADMIN", "STAFF");
    });
    options.AddPolicy("RequiredMemberRole", policy =>
    {
        policy.RequireRole("MEMBER");
    });
    options.AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});
#endregion
#region SignalR
builder.Services.AddSignalR();
#endregion
#region CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigins", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion
#region Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheUrl"];
});
#endregion

#region Memory Cache
builder.Services.AddMemoryCache();
#endregion

#region FluentValidator
builder.Services.AddFluentValidator();
#endregion

#region AzureInsight
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});
#endregion

#region BackgroundServices
builder.Services.AddHostedService<PendingMonetaryTransactionCancelService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAnyOrigins");

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHubDAO>("/chatHub");
    endpoints.MapControllers();
});

app.Run();
