using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Repositories.TagRepos;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public readonly ITagReadRepository _tagReadRepository;
        public readonly ITagWriteRepository _tagWriteRepository;
        public readonly IMapper _mapper;
        public TagController(ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository, IMapper mapper)
        {
            _tagReadRepository = tagReadRepository;
            _tagWriteRepository = tagWriteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<TagListDto> GetAllMappedTags()
        {
            return _tagReadRepository.GetAllMappedToDto();
        }

        [HttpGet("{id}")]
        public IQueryable<TagListDto> GetMappedTagWithRelations(int id)
        {
            return _tagReadRepository.GetByIdMappedToDto(id).AsNoTracking();
        }

        [HttpPost("add")]
        public async Task<bool> AddTagAsync(TagCreateDto tagCreateDto)
        {
            var tag = _mapper.Map<Tag>(tagCreateDto);
            await _tagWriteRepository.AddAsync(tag);
            return true;
        }

        [HttpPut("update/{id}")]
        public async Task<bool> UpdateTag(TagUpdateDto tagUpdateDto, int id)
        {
            var tag = _tagReadRepository.GetByIdAsync(id);
            await _mapper.Map(tagUpdateDto, tag);
            await _tagWriteRepository.SaveAsync();

            return true;
        }
    }
}
