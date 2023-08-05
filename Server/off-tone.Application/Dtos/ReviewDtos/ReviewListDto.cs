using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Dtos.ReviewDtos
{
    public class ReviewListDto
    {
        public string VoterName { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
