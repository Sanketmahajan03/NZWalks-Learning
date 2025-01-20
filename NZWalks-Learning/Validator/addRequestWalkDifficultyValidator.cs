using FluentValidation;
using Microsoft.EntityFrameworkCore.Update.Internal;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Validator
{
    public class addRequestWalkDifficultyValidator : AbstractValidator<AddWalkDifficultyDto>
    {
        public addRequestWalkDifficultyValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
