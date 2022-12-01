using Content.Core.Entities.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Interfaces
{
    internal interface IRegexIndexGegenator
    {
        string Generate(List<Tag> tags);
    }
}
