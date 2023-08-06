using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
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
        public readonly IBlogPostWriteRepository _blogPostWriteRepository;
        public readonly IMapper _mapper;
        public TagController(ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository, IBlogPostWriteRepository blogPostWriteRepository, IMapper mapper)
        {
            _tagReadRepository = tagReadRepository;
            _tagWriteRepository = tagWriteRepository;
            _blogPostWriteRepository = blogPostWriteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<TagListDto> GetAllMappedTags([FromQuery] int orderBy)
        {
            return _tagReadRepository.GetAllMappedToDto(new QueryOptions { orderBy = orderBy });
        }

        [HttpGet("{id}")]
        public IQueryable<TagListDto> GetMappedTagWithRelations(int id)
        {
            return _tagReadRepository.GetByIdMappedToDto(id).AsNoTracking();
        }

        [HttpPost("add")]
        public async Task<bool> AddTagAsync(TagCreateDto tagCreateDto)
        {
            var fetchedTag = await _tagReadRepository.GetWhere(tag => tag.Name == tagCreateDto.Name).FirstOrDefaultAsync();
            if (fetchedTag != null)
            {
                throw new Exception("Tag Already Exists");
            }
            var tag = _mapper.Map<Tag>(tagCreateDto);
            await _tagWriteRepository.AddAsync(tag);
            return await _tagWriteRepository.SaveAsync();
        }

        [HttpPut("update/{id}")]
        public async Task<bool> UpdateTag(int id, TagUpdateDto tagUpdateDto)
        {
            var tag = await _tagReadRepository.GetByIdAsync(id);
            _mapper.Map(tagUpdateDto, tag);
            return await _tagWriteRepository.SaveAsync();
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> DeleteTag(int id)
        {   // FIX: Not working
            var tag = await _tagReadRepository.GetByIdAsync(id);
            var defaultTag = await _tagReadRepository.GetDefaultTag();

            if (defaultTag == null)
            {
                throw new Exception("Default tag could not be found.");
            }

            if (defaultTag.Name != "Others Tag")
            {
                throw new Exception("Couldn't fetch the default tag.");
            }

            if (tag.TagId == defaultTag.TagId)
            {
                throw new Exception("Cannot delete the others tag.");
            }

            foreach (BlogPost post in tag.Posts)
            {
                post.Tags.Remove(tag);
                _blogPostWriteRepository.Update(post);
                await _blogPostWriteRepository.SaveAsync();
                if (!post.Tags.Contains(defaultTag))
                {
                    post.Tags.Add(defaultTag);
                    
                }
                await _tagWriteRepository.SaveAsync();

            }

            tag.Posts.Clear();
            _tagWriteRepository.Remove(tag);
            //_tagWriteRepository.Update(defaultTag);

            await _blogPostWriteRepository.SaveAsync();
            return await _tagWriteRepository.SaveAsync();
        }
    }
}
