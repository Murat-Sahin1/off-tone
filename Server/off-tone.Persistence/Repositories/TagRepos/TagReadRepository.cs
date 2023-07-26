﻿using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.TagRepos
{
    public class TagReadRepository : ReadRepository<Tag, TagListDto>, ITagReadRepository
    {
        public TagReadRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }

        public override IQueryable<TagListDto> GetAllMappedToDto()
        {
            return Table.AsNoTracking().AsQueryable().Select(t => new TagListDto
            {
                TagId = t.TagId,
                TagName = t.Name,
            });        
        }

        public override IQueryable<TagListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsQueryable().Where(t => t.TagId == id).Select(t => new TagListDto
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
    }
}
