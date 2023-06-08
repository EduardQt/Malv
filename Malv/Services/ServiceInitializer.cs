using Malv.Services.Ad;
using Malv.Services.Auth;
using Malv.Services.Auth.Refresh;
using Malv.Services.Auth.Security;
using Malv.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Malv.Services;

public static class ServiceInitializer
{
    public static void InitMalvServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRefreshService, RefreshService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<AdService>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITwoFactorService, TwoFactorService>();
    }
    
    public static void InitMalvAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var bindJwtSettings = new JwtSettings();
            configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            services.AddSingleton(bindJwtSettings);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }
}