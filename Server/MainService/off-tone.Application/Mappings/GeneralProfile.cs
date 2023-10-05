using AutoMapper;
using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Dtos.TagDtos;
using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Source --> Target
            // BlogPost ------------------
            CreateMap<BlogPostCreateDto, BlogPost>();
            CreateMap<BlogPostUpdateDto, BlogPost>();

            // Blog ---------------------
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();

            // Tag ----------------------
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();
        }
    }
}
