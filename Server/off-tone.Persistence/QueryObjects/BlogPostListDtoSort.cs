using off_tone.Application.Dtos.BlogPostDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects
{
    public enum OrderByOptions
    {
        [Display(Name = "Sort by...")] DefaultOrder = 0,
        [Display(Name = "Highest Votes")] ByVotes,
        [Display(Name = "Publication Date")] ByPublicationDate
    }

    public static class BlogPostListDtoSort
    {
        public static IQueryable<BlogPostListDto> OrderBlogPostsBy(this IQueryable<BlogPostListDto> blogPosts, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.DefaultOrder:
                    return blogPosts.OrderByDescending(x => x.BlogPostId);
                case OrderByOptions.ByVotes:
                    return blogPosts.OrderByDescending(x => x.AvarageReviewsVote);
                case OrderByOptions.ByPublicationDate:
                    return blogPosts.OrderByDescending(x => x.CreationDate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}