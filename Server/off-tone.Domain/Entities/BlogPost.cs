using off_tone.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [AllowNull]
        public int BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostText { get; set; }
        public int BlogId { get; set; }

        //--------------------------------
        //Relationships
        public Blog Blog { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
