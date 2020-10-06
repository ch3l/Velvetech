using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Velvetech.Server.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Student = new HashSet<Student>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [InverseProperty("Sex")]
        public virtual ICollection<Student> Student { get; set; }
    }
}
