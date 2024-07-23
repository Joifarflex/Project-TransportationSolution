using FluentValidation;
using TransportationSolution.Command;

namespace TransportationSolution.Validation
{
    public class UpdateDriverValidation : AbstractValidator<UpdateDriverCommand>
    {
        public UpdateDriverValidation()
        {
            RuleFor(x => x.DriverId).GreaterThan(0).WithMessage("Driver Id cannot be 0")
                                    .NotNull().NotEmpty().WithMessage("Driver Id cannot be null or empty");
            RuleFor(x => x.DriverName).NotNull().NotEmpty().WithMessage("Driver Name cannot be null or empty");
            RuleFor(x => x.DriverCode).NotNull().NotEmpty().WithMessage("Driver Code cannot be null or empty");
            RuleFor(x => x.DriverAddress).NotNull().NotEmpty().WithMessage("Driver Address cannot be null or empty");
        }
    }
}
