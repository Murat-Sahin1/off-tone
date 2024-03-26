using FluentValidation;
using off_tone.Application.Dtos.BlogDtos;

namespace off_tone.Application.Validators.Blogs
{
    public class CreateBlogValidator: AbstractValidator<BlogCreateDto>
    {
        public CreateBlogValidator()
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
