using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Application.Validators.Tags;
using off_tone.Domain.Entities;


namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagReadRepository _tagReadRepository;
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly IBlogPostWriteRepository _blogPostWriteRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<TagCreateDto> _createTagValidator;
        private readonly IValidator<TagUpdateDto> _updateTagValidator;
        public TagController(ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository, IBlogPostWriteRepository blogPostWriteRepository, IMapper mapper, CreateTagValidator createTagValidator, IValidator<TagUpdateDto> updateTagValidator)
        {
            _tagReadRepository = tagReadRepository;
            _tagWriteRepository = tagWriteRepository;
            _blogPostWriteRepository = blogPostWriteRepository;
            _mapper = mapper;
            _createTagValidator = createTagValidator;
            _updateTagValidator = updateTagValidator;
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
        public async Task<IResult> AddTagAsync(TagCreateDto tagCreateDto)
        {
            ValidationResult result = await _createTagValidator.ValidateAsync(tagCreateDto);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            var fetchedTag = await _tagReadRepository.GetWhere(tag => tag.Name == tagCreateDto.Name).FirstOrDefaultAsync();
            if (fetchedTag != null)
            {
                throw new Exception("Tag Already Exists");
            }
            var tag = _mapper.Map<Tag>(tagCreateDto);
            await _tagWriteRepository.AddAsync(tag);
            await _tagWriteRepository.SaveAsync();
            return Results.Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<IResult> UpdateTag(int id, TagUpdateDto tagUpdateDto)
        {
            ValidationResult result = await _updateTagValidator.ValidateAsync(tagUpdateDto);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            var tag = await _tagReadRepository.GetByIdAsync(id);

            if (tag.TagId != id)
            {
                throw new Exception("Ids are not matching.");
            }

            _mapper.Map(tagUpdateDto, tag);
            await _tagWriteRepository.SaveAsync();
            return Results.Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> DeleteTag(int id)
        {
            var tag = await _tagReadRepository.GetByIdWithPostsAsync(id);
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
            
            foreach(var post in tag.Posts.ToList())
            {
                if (post.Tags.Contains(defaultTag))
                {
                    post.Tags.Remove(tag);
                }
                else
                {
                    post.Tags.Remove(tag);
                    post.Tags.Add(defaultTag);
                }
            }
            _tagWriteRepository.Remove(tag);
            await _tagWriteRepository.SaveAsync();

            return true;
        }
    }
}