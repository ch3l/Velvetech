using System;
using System.Collections.Generic;

namespace Velvetech.Server.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
