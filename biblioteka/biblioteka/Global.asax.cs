using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using biblioteka.Services;
using biblioteka.Services.Interfaces;
using biblioteka.DAL;

namespace biblioteka
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LibraryContext>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
