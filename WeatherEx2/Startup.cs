using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherEx2.Startup))]
namespace WeatherEx2
{
    public partial class Startup
    {
        //public void ConfigureServices(IServiceCollection services)
        //{

        //}

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
