﻿using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.TagRepos
{
    public class TagReadRepository : ReadRepository<Tag, TagListDto>, ITagReadRepository
    {
        public TagReadRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }

        public override IQueryable<TagListDto> GetAllMappedToDto(QueryOptions queryOptions)
        {
            return Table.AsNoTracking().AsQueryable().Select(t => new TagListDto
            {
                TagId = t.TagId,
                TagName = t.Name,
            }).OrderByDescending(x => x.TagId);        
        }

        public override IQueryable<TagListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsNoTracking().AsQueryable().Where(t => t.TagId == id).Select(t => new TagListDto
            {
                TagId = t.TagId,
                TagName = t.Name,
                blogPosts = t.Posts.Select(p => new BlogPostListDto
                {
                    BlogPostId = p.BlogPostId,
                    BlogId = p.BlogId,
                    BlogName = p.Blog.BlogName,
                    BlogPostText = p.BlogPostText,
                    BlogPostTitle = p.BlogPostTitle,
                    TagStrings = p.Tags.Select(t => t.Name).ToArray(),
                }).ToList(),
            });
        }
        // TO-DO: Make this generic
        public List<Tag> FilterTags(List<int> tagIds)
        {
            var filteredTags = Table.Where(tag => tagIds.Contains(tag.TagId)).ToList();
            return filteredTags;
        }

        public async Task<Tag> GetDefaultTag()
        {
            return await Table.SingleOrDefaultAsync(t => t.TagId == 1);
        }

        public async Task<Tag> GetByIdWithPostsAsync(int id)
        {
            return await Table
                .Include(t => t.Posts)
                    .ThenInclude(t => t.Tags)
                .SingleOrDefaultAsync(t => t.TagId == id);
        }

    }
}
/*
 * 
 * 
 *
 *
 *.Select(t => new Tag
            {
                TagId = t.TagId,
                Name = t.Name,
                Posts = (ICollection<BlogPost>)t.Posts.Select(bp => new BlogPost
                {
                    BlogPostId = bp.BlogPostId,
                    BlogId = bp.BlogId,
                    BlogPostText = bp.BlogPostText,
                    BlogPostTitle = bp.BlogPostTitle,
                    Tags = bp.Tags,
                }),
            })
 */