using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostText { get; set; }

        //--------------------------------
        //Relationships
        
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
