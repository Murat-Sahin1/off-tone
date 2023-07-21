using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Dtos.BlogDtos
{
    public class BlogCreateDto
    {
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
        public string SubName { get; set; }
        public string About { get; set; }
    }
}
