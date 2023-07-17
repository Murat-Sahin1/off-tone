namespace off_tone.Application.Dtos
{
    public class BlogPostListDto
    {
        public int BlogPostId { get; set; }
        public string BlogName { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostText { get; set; }
        public string[] TagStrings { get; set; }
    }
}
