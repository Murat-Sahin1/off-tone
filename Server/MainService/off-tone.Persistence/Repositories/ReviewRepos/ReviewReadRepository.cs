﻿using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.ReviewDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.ReviewRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.QueryObjects.ReviewQueryObjects;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.ReviewRepos
{
    public class ReviewReadRepository : ReadRepository<Review, ReviewListDto>, IReviewReadRepository
    {
        public ReviewReadRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }

        public override IQueryable<ReviewListDto> GetAllMappedToDto(QueryOptions queryOptions)
        {
            return Table.AsNoTracking().AsQueryable().Select(r => new ReviewListDto
            {
                ReviewId = r.ReviewId,
                VoterName = r.VoterName,
                Stars = r.Stars,
                Comment = r.Comment,
            })
                .OrderReviewsBy((OrderByOptions)queryOptions.orderBy)
                .FilterReviewsBy((FilterByOptions)queryOptions.filterBy,
                queryOptions.filterValue);
        }

        public override IQueryable<ReviewListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsNoTracking().AsQueryable().Where(x => x.ReviewId == id).Select(r => new ReviewListDto
            {
                ReviewId = r.ReviewId,
                VoterName = r.VoterName,
                Stars = r.Stars,
                Comment = r.Comment,
            });
        }
    }
}
