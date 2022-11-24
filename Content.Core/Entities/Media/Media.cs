using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Media
{
    public class Media : Entity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Attributes { get; set; }
        public string? Src { get; set; }
        public DateTime CreatedTime { get; set; }
        public int? PresenterId { get; set; }
        public bool IsActive { get; set; }
        public string Poster { get; set; }
        public int ProductCode { get; set; }

        public Presenter? Presenter { get; set; }
    }
}
