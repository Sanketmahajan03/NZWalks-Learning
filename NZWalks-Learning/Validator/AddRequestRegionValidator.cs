using FluentValidation;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Validator
{
    public class AddRequestRegionValidator : AbstractValidator<AddRegionRequestDto>
    {
        public AddRequestRegionValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
