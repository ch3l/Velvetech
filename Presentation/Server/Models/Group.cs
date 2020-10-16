using System;
using System.Collections.Generic;

using Velvetech.Domain.Common;

namespace Velvetech.Presentation.Server.Models
{
	public partial class Group : Entity<Guid>
	{
        public Group()
        {
            Grouping = new HashSet<Grouping>();
        }

        //public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Grouping> Grouping { get; set; }
    }
}
