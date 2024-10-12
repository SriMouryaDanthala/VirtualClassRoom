using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomMediator.Extensions
{
    public static class MediatorRegistrationExtensions
    {
        public static void RegisterMediators(this IServiceCollection services, Assembly assembly)
        {
            var mediatorAssembly = Assembly.Load("VirtualClassRoomMediator");
            var mediators = mediatorAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == "VirtualClassRoomMediator.Mediators" && !t.IsAbstract)
                .ToList();

            foreach (var mediator in mediators)
            {
                services.AddScoped(mediator);
            }
        }
    }
}
