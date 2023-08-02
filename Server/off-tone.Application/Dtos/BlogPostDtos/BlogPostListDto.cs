namespace off_tone.Application.Dtos.BlogPostDtos
{
    public class BlogPostListDto
    {
        public int BlogPostId { get; set; }
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostText { get; set; }
        public string[] TagStrings { get; set; }
        public double? AvarageReviewsVote { get; set; }
        public int ReviewCount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
