using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Dtos.TagDtos
{
    public class TagListDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public ICollection<BlogPost> blogPosts { get; set; }
    }
}
