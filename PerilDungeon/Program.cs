using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PerilDungeon.Data;
using System.Threading.Tasks;

namespace PerilDungeon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddSingleton(typeof(IPartyProvider), typeof(PartyProvider));
            builder.Services.AddSingleton(typeof(IEncounterProvider), typeof(EncounterProvider));
            builder.Services.AddSingleton(typeof(IMessageProvider), typeof(MessageProvider));
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
