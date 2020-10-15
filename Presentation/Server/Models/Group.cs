using System;
using System.Collections.Generic;

namespace Velvetech.Presentation.Server.Models
{
    public partial class Group
    {
        public Group()
        {
            Grouping = new HashSet<Grouping>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
