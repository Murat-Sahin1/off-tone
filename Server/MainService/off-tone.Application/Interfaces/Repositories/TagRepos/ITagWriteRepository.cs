using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;

namespace off_tone.Application.Interfaces.Repositories.TagRepos
{
    public interface ITagWriteRepository : IWriteRepository<Tag>
    {
    }
}
