using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostReadRepository _blogPostsReadRepository;
        private readonly IBlogPostWriteRepository _blogPostsWriteRepository;
        private readonly IMapper _mapper;
        public BlogPostsController(IBlogPostReadRepository blogPostReadRepository, IBlogPostWriteRepository blogPostWriteRepository, IMapper mapper)
        {
            _blogPostsReadRepository = blogPostReadRepository;
            _blogPostsWriteRepository = blogPostWriteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<BlogPostListDto> GetListedBlogPosts()
        {
            return _blogPostsReadRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IQueryable<BlogPostListDto> GetListedBlogPost(int id)
        {
            return _blogPostsReadRepository.GetById(id);
        }

        [HttpPost("add")]
        public async Task<bool> AddBlogPostAsync(BlogPostCreateDto blogPostCreateDto)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostCreateDto);
            await _blogPostsWriteRepository.AddAsync(blogPost);
            return await _blogPostsWriteRepository.SaveAsync();
        }
    }
}
