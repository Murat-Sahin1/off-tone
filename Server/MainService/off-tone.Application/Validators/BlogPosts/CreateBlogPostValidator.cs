using FluentValidation;
using off_tone.Application.Dtos.BlogPostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Validators.BlogPosts
{
    public class CreateBlogPostValidator : AbstractValidator<BlogPostCreateDto>
    {
        public CreateBlogPostValidator()
        {
            // Make this first rule user specific
            RuleFor(bp => bp.BlogId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("A blog post should belong to a blog.");
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
            RuleFor(bp => bp.TagIds)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Blog post tags cannot be empty.");
        }
    }
}
