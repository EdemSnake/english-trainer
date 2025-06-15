using App.Application.Interfaces;
using App.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPronunciationAssessmentService, PronunciationAssessmentService>();
            return services;
        }
    }
}