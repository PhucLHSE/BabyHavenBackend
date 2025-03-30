using BabyHaven.Common.Enum.Converters;
using Azure;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Repositories;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenAI;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using VNPAY.NET;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using BabyHaven.Repositories.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = Environments.Development;

// Đăng ký dịch vụ trong DI Container
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IMembershipPackageService, MembershipPackageService>();
builder.Services.AddScoped<IPackageFeatureService, PackageFeatureService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPackagePromotionService, PackagePromotionService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IDiseaseService, DiseaseService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberMembershipService, MemberMembershipService>();
builder.Services.AddScoped<IGrowthRecordService, GrowthRecordService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IChildrenService, ChildrenService>();
builder.Services.AddScoped<IGrowthAnalysisService, GrowthRecordAnalysisService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorSpecializationService, DoctorSpecializationService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IMilestoneService, MilestoneService>();
builder.Services.AddScoped<IChildMilestoneService, ChildMilestoneService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();
builder.Services.AddSingleton<IVnpay, Vnpay>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IConsultationRequestService, ConsultationRequestService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IConsultationResponseService, ConsultationResponseService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRatingFeedbackService, RatingFeedbackService>();
builder.Services.AddScoped<IBmiPercentileService, BmiPercentileService>();


// Đăng ký UnitOfWork và Repository
builder.Services.AddScoped<UnitOfWork>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});


//ODATA
static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Alert>("Alert");
    odataBuilder.EntitySet<Blog>("Blog");
    odataBuilder.EntitySet<BlogCategory>("BlogCategory");
    odataBuilder.EntitySet<Child>("Child");
    odataBuilder.EntitySet<ChildMilestone>("ChildMilestone");
    odataBuilder.EntitySet<ConsultationRequest>("ConsultationRequest");
    odataBuilder.EntitySet<ConsultationResponse>("ConsultationResponse");
    odataBuilder.EntitySet<Disease>("Disease");
    odataBuilder.EntitySet<Doctor>("Doctor");
    odataBuilder.EntitySet<DoctorSpecialization>("DoctorSpecialization");
    odataBuilder.EntitySet<Feature>("Feature");
    odataBuilder.EntitySet<GrowthRecord>("GrowthRecord");
    odataBuilder.EntitySet<Member>("Member");
    odataBuilder.EntitySet<MemberMembership>("MemberMembership");
    odataBuilder.EntitySet<MembershipPackage>("MembershipPackage");
    odataBuilder.EntitySet<Milestone>("Milestone");
    odataBuilder.EntitySet<PackageFeature>("PackageFeature");
    odataBuilder.EntitySet<PackagePromotion>("PackagePromotion");
    odataBuilder.EntitySet<Promotion>("Promotion");
    odataBuilder.EntitySet<RatingFeedback>("RatingFeedback");
    odataBuilder.EntitySet<Role>("Role");
    odataBuilder.EntitySet<Specialization>("Specialization");
    odataBuilder.EntitySet<Transaction>("Transaction");
    odataBuilder.EntitySet<UserAccount>("UserAccount");
    return odataBuilder.GetEdmModel();
}
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
    options.AddRouteComponents("odata", GetEdmModel());
});

// Swagger UI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

//Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Nếu dùng HTTPS
    options.Cookie.HttpOnly = true;
});

//Cấu hình Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Mặc định JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Khi cần xác thực, ưu tiên JWT
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Cho Google
})
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set cookie lifespan to 60 minutes
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = "/signin-google";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GoogleAuth", policy =>
        policy.RequireAuthenticatedUser().AddAuthenticationSchemes(GoogleDefaults.AuthenticationScheme));

    options.AddPolicy("JwtAuth", policy =>
        policy.RequireAuthenticatedUser().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
});

var app = builder.Build();

//Cấu hình Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BabyHaven API V1");
        c.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
