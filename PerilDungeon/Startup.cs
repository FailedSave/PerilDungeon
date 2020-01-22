using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using PerilDungeon.Data;

namespace PerilDungeon
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IPartyProvider), typeof(PartyProvider));
            services.AddSingleton(typeof(IEncounterProvider), typeof(EncounterProvider));
            services.AddSingleton(typeof(IMessageProvider), typeof(MessageProvider));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
