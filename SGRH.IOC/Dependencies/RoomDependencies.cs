using Microsoft.Extensions.DependencyInjection;
using SGRH.Persistences.Repositories;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;
using SRH.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGRH.IOC.Dependencies
{

    public static class RoomDependencies
    {
        public static void AddReservationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
        }
    }
}
