using System;
using System.Collections.Generic;

namespace Velvetech.Server.Models
{
    public partial class Grouping
    {
        public Guid StudentId { get; set; }
        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
