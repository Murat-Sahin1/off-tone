using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities
{
    public class Tag
    {
        [Key]
        [Required]
        public int TagId { get; set; }
        public string Name { get; set; }
        
        //-----------------------
        //Relationships
        public ICollection<BlogPost> Posts { get; set; }
    }
}
