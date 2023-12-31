﻿using off_tone.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities
{
    public class Blog : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
        public string SubName { get; set; }
        public string About { get; set; }

        //-----------------------------------
        //Relationships
        public ICollection<BlogPost> BlogPosts { get; set; } 
    }
}
