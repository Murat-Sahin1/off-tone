using off_tone.Application.Dtos.BlogDtos;
using off_tone.Domain.Entities;
using off_tone.Persistence.QueryObjects.BlogPostQueryObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace off_tone.Persistence.QueryObjects.BlogQueryObjects
{
    public enum OrderBlogsByOptions
    {
        DefaultOrder = 0,
        ByName,
        ByPublicationDate
    }
    public static class BlogListDtoSort
    {
        public static IQueryable<BlogListDto> OrderBlogsBy(this IQueryable<BlogListDto> query, OrderBlogsByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderBlogsByOptions.DefaultOrder:
                    return query.OrderByDescending(x => x.BlogId);
                case OrderBlogsByOptions.ByName:
                    return query.OrderByDescending(x => x.BlogName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}