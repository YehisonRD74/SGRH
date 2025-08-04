using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SGRH.Application.DTO.dbo;

namespace SGRH.Application.Validators
{
    public class CreateFloorValidator : AbstractValidator<CreateFloorDto>
    {
        public CreateFloorValidator()
        {
            RuleFor(floor => floor.FloorNumber)
                .NotEmpty().WithMessage("El número de piso es requerido.")
                .GreaterThan(0).WithMessage("El número de piso debe ser un valor positivo.");

            RuleFor(floor => floor.CreatedBy)
                .NotEmpty().WithMessage("El usuario que crea el piso es requerido.")
                .MaximumLength(50).WithMessage("El nombre del creador no puede exceder los 50 caracteres.");
        }
    }
}