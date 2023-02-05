using EcommerceCleanArchitecture.ApplicationDomain.InputPorts;
using EcommerceCleanArchitecture.ApplicationDomain.Output;

namespace EcommerceCleanArchitecture.WebClient
{
    internal static class ContainerRegistration
    {
        public static void RegisterDependencies(IServiceCollection serviceCollection)
        {
            var assembliesToLoad = new List<string>
            {
                "EcommerceCleanArchitecture.ApplicationDomain"
            };

            LoadAssemblies(assembliesToLoad);
            RegisterViewModels(serviceCollection);
            RegisterUseCases(serviceCollection);
        }

        public static void LoadAssemblies(List<string> assembliesToRegister)
        {
            foreach (var assembly in assembliesToRegister)
            {
                System.Reflection.Assembly.Load(assembly);
            }
        }

        public static void RegisterUseCases(IServiceCollection serviceCollection)
        {
            var classImplementations = GetClassImplementations("UseCase", "EcommerceCleanArchitecture.ApplicationDomain.UseCases");

            foreach (var type in classImplementations)
            {
                serviceCollection.AddTransient(typeof(IUseCaseInputPort<ProductListViewModel>), type);
            }
        }
        public static void RegisterViewModels(IServiceCollection serviceCollection)
        {
            var classImplementations = GetClassImplementations("EcommerceCleanArchitecture.ApplicationDomain.Output");

            foreach (var type in classImplementations)
            {
                serviceCollection.AddTransient(type);
            }
        }


        public static void Register(List<string> conventions, List<string> namespaces, IServiceCollection serviceCollection)
        {
            var interfaces = GetInterfaces(conventions, namespaces);
            var classImplementations = GetClassImplementations(conventions, namespaces);

            foreach (var type in interfaces)
            {
                var classImplementation = classImplementations.First(x => x.Name == type.Name.Substring(1));
                serviceCollection.AddSingleton(type, classImplementation);
            }

            foreach (var type in classImplementations)
            {
                serviceCollection.AddSingleton(type);
            }
        }

        public static bool EndsWith(this string value, IEnumerable<String> values)
        {
            return values.Any(value.EndsWith);
        }

        public static IEnumerable<Type> GetInterfaces(List<string> conventions, List<string> namespaces)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(t => t.GetTypes())
                            .Where(t => t.IsInterface
                                && t.Name.EndsWith(conventions)
                                && namespaces.Contains(t.Namespace));
        }
        public static IEnumerable<Type> GetInterfaces(string namespaceToSearch)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(t => t.GetTypes())
                            .Where(t => t.IsInterface
                                && namespaceToSearch == t.Namespace);
        }


        public static IEnumerable<Type> GetClassImplementations(List<string> conventions, List<string> namespaces)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where(t => t.IsClass
                        && t.Name.EndsWith(conventions)
                        && namespaces.Contains(t.Namespace));
        }

        public static IEnumerable<Type> GetClassImplementations(string namespaceToSearch)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where(t => t.IsClass
                        && namespaceToSearch == t.Namespace);
        }

        public static IEnumerable<Type> GetClassImplementations(string convention, string namespaceToSearch)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where(t => t.IsClass
                        && t.Name.EndsWith(convention)
                        && namespaceToSearch == t.Namespace); 
        }
    }
}
