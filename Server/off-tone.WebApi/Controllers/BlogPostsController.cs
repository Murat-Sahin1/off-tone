using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostReadRepository _blogPostsReadRepository;
        private readonly IBlogPostWriteRepository _blogPostsWriteRepository;
        private readonly ITagReadRepository _tagReadRepository;
        private readonly IMapper _mapper;
        public BlogPostsController(IBlogPostReadRepository blogPostReadRepository, IBlogPostWriteRepository blogPostWriteRepository, ITagReadRepository tagReadRepository, IMapper mapper)
        {
            _blogPostsReadRepository = blogPostReadRepository;
            _blogPostsWriteRepository = blogPostWriteRepository;
            _tagReadRepository = tagReadRepository;
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
            var returnedTags = _tagReadRepository.FilterTags(blogPostCreateDto.TagIds);
            if (returnedTags.Count == blogPostCreateDto.TagIds.Count)
            {
                var blogPost = _mapper.Map<BlogPost>(blogPostCreateDto);
                blogPost.Tags = returnedTags;
                await _blogPostsWriteRepository.AddAsync(blogPost);
                return await _blogPostsWriteRepository.SaveAsync();
            } else
            {
                throw new Exception("Tags do not exist.");
            }
            
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
            return await _blogPostsWriteRepository.SaveAsync();
        }
    }
}
