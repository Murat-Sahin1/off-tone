using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Domain.Entities;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public readonly IBlogReadRepository _blogReadRepository;
        public readonly IBlogWriteRepository _blogWriteRepository;
        public readonly IMapper _mapper;
        public readonly IValidator<BlogCreateDto> _createBlogValidator;
        public BlogController(
            IBlogWriteRepository blogWriteRepository,
            IBlogReadRepository blogReadRepository,
            IMapper mapper,
            IValidator<BlogCreateDto> createBlogValidator
            )
        {
            _blogWriteRepository = blogWriteRepository;
            _blogReadRepository = blogReadRepository;
            _createBlogValidator = createBlogValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<BlogListDto> GetAllMappedBlogs([FromQuery] int orderBy, [FromQuery] int filterBy, [FromQuery] string? filterValue, [FromQuery] int pageSize, [FromQuery] int pageNum)
        {
            return _blogReadRepository.GetAllMappedToDto(new QueryOptions { orderBy = orderBy, filterBy = filterBy, filterValue = filterValue, PageNum = pageNum, PageSize = pageSize });
        }

        [HttpGet("{id}")]
        public IQueryable<BlogListDto> GetMappedBlogWithRelations(int id)
        {
            return _blogReadRepository.GetByIdMappedToDto(id);
        }

        [HttpPost("add")]
        public async Task<IResult> AddBlogAsync(BlogCreateDto blogCreateDto)
        {
            ValidationResult result = await _createBlogValidator.ValidateAsync(blogCreateDto);
            
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }
            var blog = _mapper.Map<Blog>(blogCreateDto);
            await _blogWriteRepository.AddAsync(blog);
            await _blogWriteRepository.SaveAsync();

            return Results.Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<bool> UpdateBlog(int id, BlogUpdateDto blogUpdateDto)
        {
            var blog = await _blogReadRepository.GetByIdAsync(id);
            
            if(blog.BlogId != id)
            {
                throw new Exception("Ids are not matching.");
            }

            _mapper.Map(blogUpdateDto, blog);
            await _blogWriteRepository.SaveAsync();

            return true;
        }
    }
}
