using FluentValidation;
using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Application.Validators.BlogPosts
{
    public class UpdateBlogPostValidator : AbstractValidator<BlogPostUpdateDto>
    {
        public UpdateBlogPostValidator()
        {
            RuleFor(bp => bp.BlogPostTitle)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Blog post title cannot be empty.")
                .MaximumLength(70)
                .MinimumLength(10)
                    .WithMessage("Blog post title should be between 10-70 characters.");
            RuleFor(bp => bp.BlogPostText)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Blog post text cannot be empty.")
                .MaximumLength(70)
                .MinimumLength(10)
                    .WithMessage("Blog post text should be between 10-70 characters.");
        }
    }
}
