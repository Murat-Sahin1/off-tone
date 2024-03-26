using FluentValidation;
using off_tone.Application.Dtos.TagDtos;

namespace off_tone.Application.Validators.Tags
{
    public class CreateTagValidator : AbstractValidator<TagCreateDto>
    {
        public CreateTagValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Tag name cannot be empty.");
        }
    }
}
