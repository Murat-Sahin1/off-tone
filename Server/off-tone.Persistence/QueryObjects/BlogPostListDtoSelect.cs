using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Persistence.QueryObjects
{
    public static class BlogPostListDtoSelect
    {
        public static IQueryable<BlogPostListDto> MapBlogPostListDto(this IQueryable<BlogPost> blogPosts)
        {
            return blogPosts.Select(blogPost => new BlogPostListDto
            {
                BlogPostId = blogPost.BlogPostId,
                BlogPostTitle = blogPost.BlogPostTitle,
                BlogPostText = blogPost.BlogPostText,
                TagStrings = blogPost.Tags.Select(t => t.Name).ToArray(),
            });
        }
    }
}
