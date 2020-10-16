using System;
using System.Collections.Generic;

using Velvetech.Domain.Common;

namespace Velvetech.Presentation.Server.Models
{
    public partial class Sex : Entity<int>
    {
        public Sex()
        {
            Student = new HashSet<Student>();
        }

        public string Name { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
