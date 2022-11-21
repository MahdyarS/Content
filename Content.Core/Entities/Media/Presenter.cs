using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Media
{
    public class Presenter:Entity
    {
        public string Name { get; set; }
        public int? PresenterCode { get; set; }
    }
}
