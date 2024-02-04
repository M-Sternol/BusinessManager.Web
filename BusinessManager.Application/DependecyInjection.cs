using BusinessManager.Application.Interfaces.HR.Employee;
using BusinessManager.Application.Mapper;
using BusinessManager.Application.Services.HR.Employee;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using BusinessManager.Application.Interfaces.Settings;
using System.Net.Mail;
using BusinessManager.Application.Services.Settings;
using FluentValidation.AspNetCore;
using BusinessManager.Application.FluentValidation.HR.Employee.Work.MedicalLeave;
using BusinessManager.Application.ViewModel.HR.Employee.EmployeeDetails;

namespace BusinessManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Service registration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IRoleInitializer, RoleInitializer>();
            services.AddTransient<IGenerateStrongPassword, GenerateStrongPassword>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddTransient<ILeaveRequestService, LeaveRequestService>();
            services.AddTransient<IMedicalLeaveService, MedicalLeaveService>();

            // Validation registration
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmployeeViewModel>());
            return services;
        }
    }
}
