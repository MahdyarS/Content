using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Media
{
    internal class Tag : Entity
    {
        public Tag(int propertyId, string value)
        {
            PropertyId = propertyId;
            Value = value;
        }

        public Tag(Property property, string value)
        {
            Property = property;
            Value = value;
        }

        public int PropertyId { get; set; }
        public string Value { get; set; }

        public Property Property { get; set; }
    }
}
