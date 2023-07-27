using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.TagRepos
{
    public interface ITagReadRepository: IReadRepository<Tag, TagListDto> 
    {
        public List<Tag> FilterTags(List<int> tagIds);
    }
}
