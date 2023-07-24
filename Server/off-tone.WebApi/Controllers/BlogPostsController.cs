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
        public IQueryable<BlogPostListDto> GetAllMappedBlogPosts()
        {
            return _blogPostsReadRepository.GetAllMappedToDto();
        }

        [HttpGet("{id}")]
        public IQueryable<BlogPostListDto> GetMappedBlogPost(int id)
        {
            return _blogPostsReadRepository.GetByIdMappedToDto(id);
        }

        [HttpPost("add")]
        public async Task<bool> AddBlogPostAsync(BlogPostCreateDto blogPostCreateDto)
        {
            // Implement a tag conroller and make a validation here if the given tag exists
            var blogPost = _mapper.Map<BlogPost>(blogPostCreateDto);
            await _blogPostsWriteRepository.AddAsync(blogPost);
            return await _blogPostsWriteRepository.SaveAsync();
        }

        [HttpPut("update/{id}")]
        public async Task<bool> UpdateBlogPost(int id, BlogPostUpdateDto blogPostUpdateDto)
        {
            var blogPost = await _blogPostsReadRepository.GetByIdAsync(id);

            if (blogPost.BlogPostId != id)
            {
                throw new Exception("Ids are not matching.");
            }

            _mapper.Map(blogPostUpdateDto, blogPost);
            await _blogPostsWriteRepository.SaveAsync();

            return true;
        }
    }
}
