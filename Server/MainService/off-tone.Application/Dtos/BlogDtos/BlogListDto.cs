using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Application.Dtos.BlogDtos
{
    public class BlogListDto
    {
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
        public string SubName { get; set; }
        public IEnumerable<BlogPostListDto> BlogPosts { get; set; }
    }
}
