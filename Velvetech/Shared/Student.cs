using System;
using System.Collections.Generic;

namespace Velvetech.Server.Models
{
    public partial class Student
    {
        public Student()
        {
            Grouping = new HashSet<Grouping>();
        }

        public Guid Id { get; set; }
        public int SexId { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Callsign { get; set; }

        public virtual Sex Sex { get; set; }
        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
