using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkoutPlanner.Startup))]
namespace WorkoutPlanner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
