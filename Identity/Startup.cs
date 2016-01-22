using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartup(typeof(Identity.Startup))]

namespace Identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Database.SetInitializer<Models.ApplicationDbContext>(new MigrateDatabaseToLatestVersion<Models.ApplicationDbContext, Identity.Migrations.Configuration>());
        }
    }
}
