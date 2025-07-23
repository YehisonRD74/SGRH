using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Services;
using SGRH.Persistences.Repositories;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using SRH.Application.DTO.dbo;
using SGRH.Application.DTO.dbo;
using SGRH.Application.Validators;
using FluentValidation;
using SGRH.Application.Contracts.Repositories.dbo;
using SGRH.Application.Services;


namespace SGRH.IOC.Dependencies
{
    public static class FloorDependencies
    {
        public static void AddFloorDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IFloorRepository, FloorRepository>();

           services.AddScoped<IValidator<CreateFloorDto>, CreateFloorValidator>();
          
        }
    }
}