using off_tone.Domain.Entities;
using off_tone.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects
{
    public static class BlogPostListDtoSelect
    {
        public static IQueryable<BlogPostListDto> MapBlogPostListDto(this IQueryable<BlogPost> blogPosts)
        {
            return blogPosts.Select(blogPost => new BlogPostListDto
            {
                BlogPostId = blogPost.BlogPostId,
                BlogName = blogPost.Blog.BlogName,
                BlogPostTitle = blogPost.BlogPostTitle,
                BlogPostText = blogPost.BlogPostText,
                TagStrings = blogPost.Tags.Select(t => t.Name).ToArray(),
            });
        }
    }
}
