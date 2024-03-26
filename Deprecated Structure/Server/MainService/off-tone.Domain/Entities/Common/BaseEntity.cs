using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Domain.Entities.Common
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
