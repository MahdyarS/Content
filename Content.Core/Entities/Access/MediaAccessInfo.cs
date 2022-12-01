﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Core.Entities.Access
{
    public class MediaAccessInfo : Entity
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int MediaId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AddedDateTime { get; set; }
    }
}
