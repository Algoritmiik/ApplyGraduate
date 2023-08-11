using ApplyGraduate.Data.Abstract;
using ApplyGraduate.Data.Concrete.EntityFramework.Contexts;
using ApplyGraduate.Data.Concrete.EntityFramework.Repositories;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.CompanionDtos;
using ApplyGraduate.Entities.Dtos.StudentDtos;
using ApplyGraduate.Services.Abstract;
using ApplyGraduate.Services.Concrete;
using ApplyGraduate.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApplyGraduate.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ApplyGraduateContext>();
            serviceCollection.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplyGraduateContext>();

            serviceCollection.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            serviceCollection.AddScoped<IEmailSenderService, EmailSenderManager>();

            serviceCollection.AddScoped<IValidator<AddStudentDto>, AddStudentDtoValidator>();
            serviceCollection.AddScoped<IValidator<AddCompanionDto>, AddCompanionDtoValidator>();

            serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));

            return serviceCollection;
        }
    }
}