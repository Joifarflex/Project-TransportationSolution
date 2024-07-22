using FluentValidation;
using TransportationSolution.Command;

namespace TransportationSolution.Validation
{
    public class UpdateVehicleValidation : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleValidation()
        {
            RuleFor(x => x.VehicleId).NotNull().NotEmpty().WithMessage("Vehicle Id cannot be null or empty");
            RuleFor(x => x.VehicleTypeCode).NotNull().NotEmpty().WithMessage("Vehicle Type Code cannot be null or empty");
            RuleFor(x => x.VehicleTypeName).NotNull().NotEmpty().WithMessage("Vehicle Type Name cannot be null or empty");
            RuleFor(x => x.LicenseNumber).NotNull().NotEmpty().WithMessage("License Number cannot be null or empty");
            RuleFor(x => x.Year).NotNull().NotEmpty().WithMessage("Year cannot be null or empty");
            RuleFor(x => x.IsVendor).NotNull().NotEmpty().WithMessage("Is vendor null or empty");
        }
    }
}
