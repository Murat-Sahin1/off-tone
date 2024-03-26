using FluentValidation;
using off_tone.Application.Dtos.BlogDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Validators.Blogs
{
    public class UpdateBlogValidator : AbstractValidator<BlogUpdateDto>
    {
        public UpdateBlogValidator()
        {
            RuleFor(b => b.BlogName)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Blog title cannot be empty.")
                .MaximumLength(50)
                .MinimumLength(3)
                    .WithMessage("Blog adı 3-50 karakter arasında olmalıdır.");
            RuleFor(b => b.BlogDescription)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Blog tanımı boş olmamalıdır.");
        }
    }
}
