using FluentValidation;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Validator
{
    public class updateRequestRegionValidator : AbstractValidator<updateRegionRequestDto>
    {
        public updateRequestRegionValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
        
    }
}
