using Microsoft.Extensions.DependencyInjection;
using SRH.Application.Contracts.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRH.Application.Contracts.Repositories.Services;
using SGRH.Application.Services;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.Services;
using SRH.Application.Contracts.Repositories.dbo;
using SGRH.Persistences.Repositories;

namespace SGRH.IOC.Dependencies
{

    public static class ReservationDependencies
    {
        public static void AddReservationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService > ();
            services.AddScoped<IReservationRepository, ReservationRepository > ();

        }
    }
}
