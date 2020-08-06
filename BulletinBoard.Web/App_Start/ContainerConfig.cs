using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BulletinBoard.Api.Accounts;
using BulletinBoard.Api.Adverts;
using BulletinBoard.Api.Comments;
using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Repositories;
using BulletinBoard.DAL;
using BulletinBoard.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BulletinBoard.Web.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<BulletinBoardDbContext>().AsSelf();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType(typeof(RepositoryBase<User>))
                .As(typeof(IRepository<User>))
                .SingleInstance();

            builder.RegisterType(typeof(RepositoryBase<Advert>))
                .As(typeof(IRepository<Advert>))
                .SingleInstance();

            builder.RegisterType(typeof(UserStore<User>))
                .As(typeof(UserStore<User>))
                .WithParameter("context", new BulletinBoardDbContext());

            builder.RegisterType(typeof(RoleStore<Role>))
                .As(typeof(RoleStore<Role>))
                .WithParameter("context", new BulletinBoardDbContext());

            builder.RegisterType<AccountsService>().As<IAccountsService>().InstancePerRequest();

            builder.RegisterType<AdvertsService>().As<IAdvertsService>().InstancePerRequest();

            builder.RegisterType<CommentsService>().As<ICommentsService>().InstancePerRequest();

            builder.RegisterType(typeof(RepositoryBase<Role>))
                .As(typeof(IRepository<Role>))
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}