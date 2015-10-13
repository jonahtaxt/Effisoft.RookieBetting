using System;
using Autofac;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;
using Effisoft.RookieBetting.SqlDataAccess;
using Effisoft.RookieBetting.SqlDataAccess.Core;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Effisoft.RookieBetting.Security.Helpers;
using Effisoft.RookieBetting.Security.Infrastructure;
using Effisoft.RookieBetting.SqlDataAccess.Security;
using Owin;

namespace Effisoft.RookieBetting.Services
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
                .WithParameter("connStringName", ConfigurationManager.ConnectionStrings["RookieBettingDbConnString"].ConnectionString);
            builder.RegisterType<ConferenceRepository>().As<IConferenceRepository>().InstancePerRequest();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>().InstancePerRequest();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerRequest();
            builder.RegisterType<GameRepository>().As<IGameRepository>().InstancePerRequest();
            builder.RegisterType<GameBetRepository>().As<IGameBetRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            var container = builder.Build();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
            app.UseAutofacMiddleware(container);
        }
    }
}