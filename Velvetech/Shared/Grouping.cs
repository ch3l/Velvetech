using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Velvetech.Server.Models
{
    public partial class Grouping
    {
        [Key]
        public Guid StudentId { get; set; }

        [Key]
        public Guid GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("Grouping")]
        public virtual Group Group { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Grouping")]
        public virtual Student Student { get; set; }
    }
}
