using Autofac;
using Autofac.Integration.WebApi;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;
using Effisoft.RookieBetting.SqlDataAccess;
using Effisoft.RookieBetting.SqlDataAccess.Core;
using Owin;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace Effisoft.RookieBetting.Services
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
                .WithParameter("connStringName", ConfigurationManager.ConnectionStrings["RookieBettingDbConnString"].ConnectionString);
            builder.RegisterType<ConferenceRepository>().As<IConferenceRepository>();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();

            var container = builder.Build();

            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
        }
    }
}
