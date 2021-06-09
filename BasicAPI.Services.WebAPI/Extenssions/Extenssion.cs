using BasicApi.Data.DataAccess.Repositories;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BasicAPI.Services.WebAPI.Extenssions
{
    public static class Extenssion
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Author>, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
