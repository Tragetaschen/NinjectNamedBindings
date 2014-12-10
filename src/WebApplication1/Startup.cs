using System;
using Microsoft.AspNet.Builder;
using Ninject;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Ninject;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection.NestedProviders;

namespace WebApplication1
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseServices(services =>
            {
                services.AddMvc();

                var kernel = new StandardKernel();
                kernel.Populate(services, app.ApplicationServices);
                // TODO: remove the workaround. cf. https://github.com/aspnet/DependencyInjection/issues/134
                kernel.Bind<INestedProviderManager<FilterProviderContext>>().To<NestedProviderManager<FilterProviderContext>>();

                kernel.Bind<Class>().ToConstant(new Class { Name = "Foo was inserted" }).Named("Foo");
                kernel.Bind<Class>().ToConstant(new Class { Name = "Bar was inserted" }).Named("Bar");

                // This is how Ninject resolves the name:
                Console.WriteLine("Ninject says: {0}", kernel.Get<Class>("Foo").Name);

                return kernel.Get<IServiceProvider>();
            });

            app.UseMvc();
        }
    }
}