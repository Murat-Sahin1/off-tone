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
        public BlogPostsController(IBlogPostReadRepository blogPostReadRepository)
        {
            _blogPostsReadRepository =  blogPostReadRepository;
        }
        [HttpGet]
        public List<BlogPostListDto> GetBlogPosts()
        {
            return _blogPostsReadRepository.GetAll().ToList();
        }
    }
}
