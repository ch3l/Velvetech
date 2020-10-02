using System;
using System.Collections.Generic;

using System.Text.Json.Serialization;


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

		[JsonIgnore]
        public virtual ICollection<Student> Student { get; set; }
    }
}
