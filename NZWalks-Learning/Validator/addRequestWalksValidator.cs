using FluentValidation;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Validator
{
    public class addRequestWalksValidator : AbstractValidator<AddWalksRequestDto>
    {
        public addRequestWalksValidator()
        {
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Length must be greater than 0");
            //RuleFor(RuleFor => RuleFor.RegionId).NotEmpty().WithMessage("RegionId is required");
            //RuleFor(RuleFor => RuleFor.WalkDifficultyId).NotEmpty().WithMessage("WalkDifficultyId is required");

        }
    }
}
