using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Media
{
    public class Presenter:Entity
    {
        public Presenter(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public int? ProviderCode { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
