using FluentValidation;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Validator
{
    public class updateRequestWalkDifficultyValidator : AbstractValidator<UpdateWalkDifficultyDto>
    {
        public updateRequestWalkDifficultyValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
