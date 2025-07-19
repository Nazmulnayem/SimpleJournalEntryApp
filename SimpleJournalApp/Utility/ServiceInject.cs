using SimpleJournalApp.Application.Interface;
using SimpleJournalApp.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace SimpleJournalApp.WebAPI.Utility
{
    public static class ServiceInject
    {
        public static void InjectService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IJournalEntryService, JournalEntryService>();
            services.AddScoped<UserService>();


        }
    }
}
