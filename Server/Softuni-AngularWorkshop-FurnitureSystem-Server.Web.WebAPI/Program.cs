using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MoravianStar.Dao;
using MoravianStar.Settings;
using MoravianStar.WebAPI.Attributes;
using MoravianStar.WebAPI.Extensions;
using MoravianStar.WebAPI.JsonConverters;
using MoravianStar.WebAPI.ModelBinders;
using MoravianStar.WebAPI.Swagger;
using MoravianStar.WebAPI.Transformers;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Common;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Infrastructure.Constants;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Infrastructure.Handlers;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Middlewares;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.DbContexts;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Stores;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Services.Furniture;
using System;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        options.Filters.Add<ValidateModelStateAttribute>();
        options.AddCustomSimpleTypeModelBinderProvider();
    })
    .AddControllersAsServices()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new CustomStringTypeJsonConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // This options is set to "true", because the logic in ValidateModelStateAttribute 
    // won't be executed for controllers marked with ApiControllerAttribute
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var signingConfigurationService = new SigningConfigurationService();
builder.Services.AddSingleton(signingConfigurationService);

var jwtEventHandlers = new JwtEventHandlers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyConstants.Default,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200") // TODO: Add urls, that are defined in appsettings.json
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "http://localhost:5000",
                ValidAudience = "SoftuniFurnitureWorkshopAudience",
                IssuerSigningKey = signingConfigurationService.Key,
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = jwtEventHandlers.OnTokenValidated,
                OnAuthenticationFailed = jwtEventHandlers.OnAuthenticationFailed
            };
        });

builder.Services
        .AddDbContext<AppDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(connectionString, x =>
            {
                x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
            })
            .UseAsyncSeeding(async (appDbContext, storeOperationPerformed, ct) =>
            {
                await DbSeeder.SeedAppDbAsync((AppDbContext)appDbContext);
            });
        }, ServiceLifetime.Scoped)
        .AddIdentity<UserEntity, RoleEntity>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddUserStore<UserEntityStore>()
        .AddDefaultTokenProviders();

builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
builder.Services.AddScoped<IDbTransaction<AppDbContext>, DbTransaction<AppDbContext>>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IDbUpdater, DbUpdater>();
builder.Services.AddTransient<IModelsMappingService<FurnitureModel, FurnitureEntity>, FurnitureModelsMappingService>();
builder.Services.AddTransient<IEntityDeleting<FurnitureEntity>, FurnitureDeleting>();
builder.Services.AddTransient<IEntitySaving<FurnitureEntity>, FurnitureSaving>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<HideInDocsFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMoravianStar(app.Environment, () =>
{
    Settings.DefaultDbContextType = typeof(AppDbContext);
});

app.UseHttpsRedirection();
app.UseCors(CorsPolicyConstants.Default);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

InitDb();

void InitDb()
{
    using (var scope = app.Services.CreateAsyncScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var dbUpdaterService = serviceProvider.GetRequiredService<IDbUpdater>();
        dbUpdaterService.CreateAndUpdateAsync().GetAwaiter().GetResult();
    }
}

app.Run();