using FluentValidation;
using TransportationSolution.Command;

namespace TransportationSolution.Validation.VehicleDriverMatrix
{
    public class UpdateVehicleDriverMatrixValidation : AbstractValidator<UpdateVehicleDriverMatrixCommand>
    {
        public UpdateVehicleDriverMatrixValidation()
        {
            RuleFor(x => x.VehicleDriverMatrixId).NotNull().NotEmpty().WithMessage("Vehicle Driver Matrix Id cannot be null or empty");
            RuleFor(x => x.VehicleId).NotNull().NotEmpty().WithMessage("Vehicle Id cannot be null or empty");
            RuleFor(x => x.VehicleTypeCode).NotNull().NotEmpty().WithMessage("Vehicle Type Code cannot be null or empty");
            RuleFor(x => x.LicenseNumber).NotNull().NotEmpty().WithMessage("License Number cannot be null or empty");
            RuleFor(x => x.DriverId).NotNull().NotEmpty().WithMessage("Driver Id cannot be null or empty");
            RuleFor(x => x.DriverCode).NotNull().NotEmpty().WithMessage("Driver Id cannot be null or empty");
            RuleFor(x => x.IsActive).NotNull().NotEmpty().WithMessage("Is Active cannot be null or empty");
            RuleFor(x => x.DurationStart).NotNull().NotEmpty().WithMessage("Duration Start cannot be null or empty")
                                         .LessThan(x => x.DurationEnd)
                                         .WithMessage("Duration Start cannot be less than duration End");
            RuleFor(x => x.DurationEnd).NotNull().NotEmpty().WithMessage("Duration End cannot be null or empty")
                                        .GreaterThan(x => x.DurationStart)
                                        .WithMessage("Duration End cannot be less than duration start");
        }
    }
}
