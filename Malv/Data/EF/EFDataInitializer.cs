using Malv.Data.EF.Repositories;
using Malv.Data.Repository;

namespace Malv.Data.EF;

public static class EFDataInitializer
{
    public static void AddEntityFrameworkData(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IAdRepository, AdRepository>();
        serviceCollection.AddScoped<IAuthRepository, AuthRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ICountryRepository, CountryRepository>();
        serviceCollection.AddScoped<IChatRepository, ChatRepository>();
        serviceCollection.AddScoped<IMailTokenRepository, MailTokenRepository>();
        serviceCollection.AddScoped<IAdWatchRepository, AdWatchRepository>();
    }
}