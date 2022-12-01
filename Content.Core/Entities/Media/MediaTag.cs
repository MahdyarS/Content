using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Media
{
    public class MediaTag : Entity
    {
        public int MediaId { get; set; }
        public Media Media { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
