using Ninject;
using System;

namespace WebApplication1.Controllers
{
    public class HomeController
    {
        private readonly Class c;

        public HomeController([Named("Foo")] Class c)
        {
            this.c = c;
            // This is how Ninject through M.F.DI.Ninject resolves the name:
            Console.WriteLine("Controller says: {0}", c.Name);
        }

        public string Index() => c.Name;
    }
}