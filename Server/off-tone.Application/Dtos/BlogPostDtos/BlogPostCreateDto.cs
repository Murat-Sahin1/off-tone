﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Dtos.BlogPostDtos
{
    public class BlogPostCreateDto
    {
        public string BlogId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogPostText { get; set; }
        // public string[] TagStrings { get; set; }
    }
}