using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Unity;

//allow unit testing project to have visibility into private memebers
[assembly: InternalsVisibleToAttribute("lib.Xunit")]        //Visibility for XUnit
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]  //visibility for Moq

namespace Solution.Services {
    public class Top 
    {
        static void Main(String[] args) 
        {
        // UnityContainer container;
            //unity dependency injection from https://msdn.microsoft.com/en-us/library/dn178463(v=pandp.30).aspx
            using (var container = new UnityContainer())
            using (StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            using (StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput()))
            {
                ContainerBootstrapper.RegisterTypes(container);

                //var solution = container.Resolve<ISolution>();
                var solution = container.Resolve<ISolution>();
                solution.TimeLine(stdin, stdout);
            }
        }
    }
}