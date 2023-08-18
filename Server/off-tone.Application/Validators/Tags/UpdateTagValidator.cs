using FluentValidation;
using off_tone.Application.Dtos.TagDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Validators.Tags
{
    public class UpdateTagValidator : AbstractValidator<TagUpdateDto>
    {
        public UpdateTagValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Tag name cannot be empty.");
        }
    }
}
