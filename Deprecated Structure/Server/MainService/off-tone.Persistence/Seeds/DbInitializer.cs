using Microsoft.EntityFrameworkCore;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Application.Interfaces.Repositories.ReviewRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.BlogRepos;
using System.Reflection.Metadata;

namespace off_tone.Persistence.Seeds
{
    public static class DbInitializer
    {
        public static async Task<bool> MigrateDatabase(BlogDbContext context)
        {
            
            if (context == null)
            {
                return false;
            }

            try
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                await context.Database.MigrateAsync();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
        public static async Task<bool> SeedBlogPosts(IBlogPostWriteRepository blogPostWriteRepository, IBlogWriteRepository blogWriteRepository, ITagWriteRepository tagWriteRepository, IReviewWriteRepository reviewWriteRepository)
        {

            if (blogPostWriteRepository.AnyElements())
            {
                return true;
            }

            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>();
            var tags = new List<Tag>();
            var totalReviews = new List<Review>();

            int flag = 0;
            int count = 0;
            var random = new Random();

            tags.Add(new Tag
            {
                Name = "Others Tag",
            });

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Name = "Tag " + i,
                };
                tags.Add(tag);
            }

            for (int i = 0; i < 10; i++)
            {
                var blog = new Blog
                {
                    BlogName = "Blog " + i,
                    BlogDescription = "BlogDescription",
                    SubName = "MySubName",
                    About = "AboutMyBlog",
                    BlogPosts = new List<BlogPost>(),
                    CreationDate = DateTime.UtcNow
                };

                for (int j = 0; j < 10; j++)
                {
                    int hasTwoTags = random.Next(2);
                    int tagOne = random.Next(0, tags.Count);
                    int tagTwo = random.Next(0, tags.Count);

                    var reviews = new List<Review>();

                    for (int r = 0; r < 5; r++)
                    {
                        int givenStars = random.Next(0, 6);

                        reviews.Add(new Review
                        {
                            VoterName = "Voter " + r,
                            Stars = givenStars,
                            Comment = "Lorem ipsum " + r,
                            CreationDate = DateTime.UtcNow
                        });
                    }

                    var blogPost = new BlogPost
                    {
                        BlogPostTitle = "BlogPost " + j + flag,
                        BlogPostText = "BlogDescription" + j + flag,
                        Reviews = reviews,
                        Tags = hasTwoTags > 0 ? new List<Tag>() { tags.ElementAt(tagOne), tags.ElementAt(tagTwo) } : new List<Tag>() { tags.ElementAt(tagOne) },
                        Blog = blog,
                        CreationDate = DateTime.UtcNow,
                    };

                    blog.BlogPosts.Add(blogPost);
                    blogPosts.Add(blogPost);
                    count = j + flag;
                    totalReviews = totalReviews.Concat(reviews).ToList();
                }
                blogs.Add(blog);
                flag = count + 1;
            }

            try
            {
                await tagWriteRepository.InsertRangeAsync(tags);
                await reviewWriteRepository.InsertRangeAsync(totalReviews);
                await blogWriteRepository.InsertRangeAsync(blogs);
                await blogPostWriteRepository.InsertRangeAsync(blogPosts);

                await blogWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while seeding the category data: ", ex.Message);
                throw;
            }
            return true;
        }
    }
}