using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Application.Dtos.TagDtos
{
    public class TagListDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public ICollection<BlogPostListDto> blogPosts { get; set; }
    }
}
