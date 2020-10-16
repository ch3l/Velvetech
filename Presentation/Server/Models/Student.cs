using System;
using System.Collections.Generic;

using Velvetech.Domain.Common;

namespace Velvetech.Presentation.Server.Models
{
    public partial class Student : Entity<Guid>
    {
        public Student()
        {
            Grouping = new HashSet<Grouping>();
        }

        //public Guid Id { get; set; }
        public int SexId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Callsign { get; set; }

        public Sex Sex { get; set; }
        public ICollection<Grouping> Grouping { get; set; }
    }
}
