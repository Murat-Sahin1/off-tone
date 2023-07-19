using Microsoft.AspNetCore.Mvc;
using off_tone.Application.Dtos;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;

namespace off_tone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostReadRepository _blogPostsReadRepository;
        private readonly IBlogPostWriteRepository _blogPostsWriteRepository;
        public BlogPostsController(IBlogPostReadRepository blogPostReadRepository, IBlogPostWriteRepository blogPostWriteRepository)
        {
            _blogPostsReadRepository = blogPostReadRepository;
            _blogPostsWriteRepository = blogPostWriteRepository;
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
    }
}
