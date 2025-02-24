//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyModel;

//namespace xSdk.Extensions.Links.Polyfills
//{
//    internal class DefaultAssemblyLoader : IAssemblyLoader
//    {
//        public IEnumerable<Assembly> GetAssemblies()
//        {
//            var thisAssembly = GetType().GetTypeInfo().Assembly.GetName().Name;
//            var libraries =
//                DependencyContext.Default
//                    .CompileLibraries
//                    .Where(l => l.Dependencies.Any(d => d.Name.Equals(thisAssembly)));

//            var names = libraries.Select(l => l.Name).Distinct();
//            var assemblies = names.Select(a => Assembly.Load(new AssemblyName(a)));
//            return assemblies;
//        }
//    }
//}
