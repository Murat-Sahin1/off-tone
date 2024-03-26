using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Services.BlogPostServices;
using System.Diagnostics.CodeAnalysis;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostReadRepository _blogPostsReadRepository;
        private readonly IBlogPostWriteRepository _blogPostsWriteRepository;
        private readonly ITagReadRepository _tagReadRepository;
        private readonly BlogPostFilterMenu _blogPostFilterMenu;
        private readonly IMapper _mapper;
        private readonly IValidator<BlogPostCreateDto> _createBlogPostValidator;
        private readonly IValidator<BlogPostUpdateDto> _updateBlogPostValidator;

        public BlogPostsController(IBlogPostReadRepository blogPostReadRepository, IBlogPostWriteRepository blogPostWriteRepository, ITagReadRepository tagReadRepository, IMapper mapper, BlogPostFilterMenu blogPostFilterMenu, IValidator<BlogPostCreateDto> createBlogPostValidator,
            IValidator<BlogPostUpdateDto> updateBlogPostValidator)
        {
            _blogPostsReadRepository = blogPostReadRepository;
            _blogPostsWriteRepository = blogPostWriteRepository;
            _tagReadRepository = tagReadRepository;
            _mapper = mapper;
            _blogPostFilterMenu = blogPostFilterMenu;
            _createBlogPostValidator = createBlogPostValidator;
            _updateBlogPostValidator = updateBlogPostValidator;
        }

        [HttpGet]
        public IQueryable<BlogPostListDto> GetAllMappedBlogPosts([FromQuery] int orderBy, [FromQuery] int filterBy, [FromQuery][AllowNull] string? filterValue, [FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            return _blogPostsReadRepository.GetAllMappedToDto(new QueryOptions { orderBy = orderBy, filterBy = filterBy, filterValue = filterValue, PageNum = pageNum, PageSize = pageSize});
        }

        [HttpGet("{id}")]
        public IQueryable<BlogPostListDto> GetMappedBlogPost(int id)
        {
            return _blogPostsReadRepository.GetByIdMappedToDto(id);
        }

        [HttpGet("filterMenu")]
        public IEnumerable<DropdownTuple> GetBlogPostFilterMenu([FromQuery] int filterBy)
        {
            return _blogPostFilterMenu.GetFilteredBlogPostMenu((FilterByOptions)filterBy);
        }

        [HttpPost("add")]
        public async Task<IResult> AddBlogPostAsync(BlogPostCreateDto blogPostCreateDto)
        {
            ValidationResult result = await _createBlogPostValidator.ValidateAsync(blogPostCreateDto);

            if(!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            var returnedTags = _tagReadRepository.FilterTags(blogPostCreateDto.TagIds);
            if (returnedTags.Count > 0)
            {
                var blogPost = _mapper.Map<BlogPost>(blogPostCreateDto);
                blogPost.Tags = returnedTags;
                blogPost.CreationDate = DateTime.UtcNow;
                await _blogPostsWriteRepository.AddAsync(blogPost);
                await _blogPostsWriteRepository.SaveAsync();
                return Results.Ok();
            } else
            {
                throw new Exception("Tags do not exist.");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IResult> UpdateBlogPost(int id, BlogPostUpdateDto blogPostUpdateDto)
        {
            ValidationResult result = await _updateBlogPostValidator.ValidateAsync(blogPostUpdateDto);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            var blogPost = await _blogPostsReadRepository.GetByIdAsync(id);

            if (blogPost.BlogPostId != id)
            {
                throw new Exception("Ids are not matching.");
            }

            _mapper.Map(blogPostUpdateDto, blogPost);
            await _blogPostsWriteRepository.SaveAsync();
            return Results.Ok();
        }
    }
}
