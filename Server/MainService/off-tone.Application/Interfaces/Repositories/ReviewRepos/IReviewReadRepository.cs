using off_tone.Application.Dtos.ReviewDtos;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.ReviewRepos
{
    public interface IReviewReadRepository: IReadRepository<Review, ReviewListDto>
    {
    }
}
