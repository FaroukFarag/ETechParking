using ETechParking.Application.AutoMapper.Abstraction;
using ETechParking.Application.AutoMapper.Locations;
using ETechParking.Application.AutoMapper.Locations.Fares;
using ETechParking.Application.AutoMapper.Locations.Roles;
using ETechParking.Application.AutoMapper.Locations.Shifts;
using ETechParking.Application.AutoMapper.Locations.Tickets;
using ETechParking.Application.AutoMapper.Locations.Users;
using ETechParking.Application.AutoMapper.Shared;
using ETechParking.Application.Interfaces.Abstraction;
using ETechParking.Application.Interfaces.Locations;
using ETechParking.Application.Interfaces.Locations.Fares;
using ETechParking.Application.Interfaces.Locations.Roles;
using ETechParking.Application.Interfaces.Locations.Shifts;
using ETechParking.Application.Interfaces.Locations.Tickets;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Application.Services.Locations;
using ETechParking.Application.Services.Locations.Fares;
using ETechParking.Application.Services.Locations.Roles;
using ETechParking.Application.Services.Locations.Shifts;
using ETechParking.Application.Services.Locations.Tickets;
using ETechParking.Application.Services.Locations.Users;
using ETechParking.Application.Validators.Locations;
using ETechParking.Application.Validators.Locations.Fares;
using ETechParking.Application.Validators.Locations.Roles;
using ETechParking.Application.Validators.Locations.Shifts;
using ETechParking.Application.Validators.Locations.Tickets;
using ETechParking.Application.Validators.Locations.Users;
using ETechParking.Common.Tokens.Configurations;
using ETechParking.Common.Tokens.Interfaces;
using ETechParking.Common.Tokens.Services;
using ETechParking.Domain.Constants;
using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations;
using ETechParking.Domain.Interfaces.Repositories.Locations.Fares;
using ETechParking.Domain.Interfaces.Repositories.Locations.Roles;
using ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;
using ETechParking.Domain.Interfaces.Repositories.Locations.Tickets;
using ETechParking.Domain.Interfaces.Repositories.Locations.Users;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Roles;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.Infrastructure.Data.Context;
using ETechParking.Infrastructure.Data.Repositories.Abstraction;
using ETechParking.Infrastructure.Data.Repositories.Locations;
using ETechParking.Infrastructure.Data.Repositories.Locations.Fares;
using ETechParking.Infrastructure.Data.Repositories.Locations.Roles;
using ETechParking.Infrastructure.Data.Repositories.Locations.Shifts;
using ETechParking.Infrastructure.Data.Repositories.Locations.Tickets;
using ETechParking.Infrastructure.Data.Repositories.Locations.Users;
using ETechParking.Infrastructure.Data.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ETechParking.Infrastructure.IoC.DependencyContainer;

public static class DependencyContainer
{
    public static void RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtTokenSettings>(configuration.GetSection(JwtTokenSettings.SectionName));
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>))
            .AddScoped<ITokensService, TokensService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IFareService, FareService>()
            .AddScoped<ITicketService, TicketService>()
            .AddScoped<IShiftService, ShiftService>();
    }

    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ETechParkingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IFareRepository, FareRepository>()
            .AddScoped<ITicketRepository, TicketRepository>()
            .AddScoped<IShiftRepository, ShiftRepository>();
    }

    public static void RegisterUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseModelProfile).Assembly);
        services.AddAutoMapper(typeof(PaginatedModelProfile).Assembly);
        services.AddAutoMapper(typeof(LocationProfile).Assembly);
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        services.AddAutoMapper(typeof(RoleProfile).Assembly);
        services.AddAutoMapper(typeof(FareProfile).Assembly);
        services.AddAutoMapper(typeof(TicketProfile).Assembly);
        services.AddAutoMapper(typeof(ShiftProfile).Assembly);
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LocationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<RoleDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<FareDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TicketDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CalculateTicketTotalDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<PayTicketDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ShiftDtoValidator>();
    }

    public static void RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ETechParkingDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void RegisterJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtTokens");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                ClockSkew = new TimeSpan(0, 2, 0)
            };
        });
    }

    public static void RegisterCORS(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()!;

            options.AddPolicy(AppSettings.AllowedOrigins,
                policy => policy.WithOrigins(allowedOrigins)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials());
        });
    }

    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ETechParkingDbContext>();

        dbContext.Database.Migrate();
    }
}
