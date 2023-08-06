using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Services.BlogPostServices
{
    public class DropdownTuple
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Text)}: {Text}";
        }
    }
}
