using System;
using System.Collections.Generic;

namespace Velvetech.Presentation.Server.Models
{
    public partial class Grouping
    {
        public Guid StudentId { get; set; }
        public Guid GroupId { get; set; }

        public Group Group { get; set; }
        public Student Student { get; set; }
    }
}
