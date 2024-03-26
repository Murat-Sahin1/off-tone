using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Services.BlogPostServices
{
    public class BlogPostFilterMenu
    {
        private readonly BlogDbContext _blogDbContext;
        public BlogPostFilterMenu(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public IEnumerable<DropdownTuple> GetFilteredBlogPostMenu(FilterByOptions filterOptions)
        {
            switch(filterOptions)
            {
                case FilterByOptions.ByTags:
                    return _blogDbContext.Tags
                        .Select(t => new DropdownTuple
                        {
                            Value = t.TagId.ToString(),
                            Text = t.Name
                        }).ToList();
                case FilterByOptions.ByPublicationYear:
                    return _blogDbContext.BlogPosts
                        .Select(bp => bp.CreationDate.Year)
                        .Distinct()
                        .OrderByDescending(x => x)
                        .Select(x => new DropdownTuple
                        {
                            Value = x.ToString(),
                            Text = x.ToString()
                        }).ToList();
                default:
                    return new List<DropdownTuple>();
            }
        }
    }
}
