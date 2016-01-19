using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Orleans;

namespace HelloWorldNugetGrains
{
    public class ConfigureService
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var microservicesAssemblyClasses =
                Assembly.Load("HelloWorldServices").GetTypes().Where(x => x.IsClass && !x.IsAbstract).ToList();

            if (!microservicesAssemblyClasses.Any())
                throw new ApplicationException(
                    "The microservices assembly contains no microservices. Has the assembly been renamed?");

            foreach (var implementationType in microservicesAssemblyClasses)
            {
                var microserviceInterface = implementationType.GetInterfaces()
                    .FirstOrDefault(x =>
                        x.Name.StartsWith("I") &&
                        x.Name.Substring(1, x.Name.Length - 1).Equals(implementationType.Name));
                if (microserviceInterface != null)
                    services.AddSingleton(microserviceInterface, implementationType);
            }

            var grainClasses = Assembly.GetAssembly(GetType())
                .GetTypes().Where(x => x.IsClass && !x.IsAbstract && typeof(Grain).IsAssignableFrom(x));

            foreach (var grainClass in grainClasses)
            {
                services.AddTransient(grainClass);
            }

            return services.BuildServiceProvider();
        }
    }
}
