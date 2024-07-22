using FluentValidation;
using TransportationSolution.Command;

namespace TransportationSolution.Validation
{
    public class CreateDriverValidation : AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverValidation()
        {
            RuleFor(x => x.DriverName).NotNull().NotEmpty().WithMessage("Driver Name cannot be null or empty");
            RuleFor(x => x.DriverCode).NotNull().NotEmpty().WithMessage("Driver Code cannot be null or empty");
            RuleFor(x => x.DriverAddress).NotNull().NotEmpty().WithMessage("Driver Address cannot be null or empty");
        }
    }
}
