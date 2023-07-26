using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.Common;

namespace off_tone.Application.Interfaces.Repositories.TagRepos
{
    public interface ITagWriteRepository : IWriteRepository<TagCreateDto>
    {
    }
}
