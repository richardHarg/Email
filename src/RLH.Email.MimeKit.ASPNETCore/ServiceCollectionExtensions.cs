using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RLH.Email;

namespace RLH.Email.MimeKit.ASPNETCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMimeEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailService, OptionsMimeKitEmailService>();
            return services;
        }
    }
}
