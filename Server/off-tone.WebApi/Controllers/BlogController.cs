using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogDtos;
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
        public BlogController(
            IBlogWriteRepository blogWriteRepository, 
            IBlogReadRepository blogReadRepository,
            IMapper mapper
            )
        {
            _blogWriteRepository = blogWriteRepository;
            _blogReadRepository = blogReadRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<BlogListDto> GetAllMappedBlogs()
        {
            return _blogReadRepository.GetAllMappedToDto().AsNoTracking();
        }

        [HttpGet("{id}")]
        public IQueryable<BlogListDto> GetMappedBlogWithRelations(int id)
        {
            return _blogReadRepository.GetByIdMappedToDto(id);
        }

        [HttpPost("add")]
        public async Task<bool> AddBlogAsync(BlogCreateDto blogCreateDto)
        {
            var blog = _mapper.Map<Blog>(blogCreateDto);
            await _blogWriteRepository.AddAsync(blog);
            return await _blogWriteRepository.SaveAsync();
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
