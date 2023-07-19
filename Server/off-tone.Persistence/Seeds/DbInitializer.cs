using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Repositories.BlogRepos;

namespace off_tone.Persistence.Seeds
{
    public static class DbInitializer
    {
        public static async Task<bool> seedBlogPosts(IBlogPostWriteRepository blogPostWriteRepository, IBlogWriteRepository blogWriteRepository)
        {

            if (blogPostWriteRepository.AnyElements())
            {
                return true;
            }

            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>();
            int flag = 0;
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var blog = new Blog
                {
                    BlogName = "Blog " + i,
                    BlogDescription = "BlogDescription",
                    SubName = "MySubName",
                    About = "AboutMyBlog",
                    BlogPosts = new List<BlogPost>()
                };

                for (int j = 0; j < 10; j++)
                {
                    var blogPost = new BlogPost
                    {
                        BlogPostTitle = "BlogPost " + j + flag,
                        BlogPostText = "BlogDescription" + j + flag,
                        Reviews = new List<Review>(),
                        Tags = new List<Tag>()
                    };
                    blog.BlogPosts.Add(blogPost);
                    blogPosts.Add(blogPost);
                    count = j + flag;
                }
                blogs.Add(blog);
                flag = count + 1;
            }

            for (int i = 0; i < blogs.Count(); i++)
            {
                Console.WriteLine("Blog IDS: ");
                Console.WriteLine(blogs[i].BlogId);
            }
            /* 
            for (int i=0; i<blogPosts.Count(); i++)
            {
                Console.WriteLine("Blog post IDS: ");
                Console.WriteLine(blogPosts[i].BlogId);
            }
            */

            try
            {
                await blogWriteRepository.InsertRangeAsync(blogs);
                await blogPostWriteRepository.InsertRangeAsync(blogPosts);

                await blogWriteRepository.SaveAsync();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error occured while seeding the category data: ", ex.Message);
                throw;
            }
            return true;
        }
    }
}
