using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string VoterName { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        //----------------------
        //Relationships
        public int BlogPostId { get; set; }
    }
}
