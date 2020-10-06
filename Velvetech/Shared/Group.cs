using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Velvetech.Server.Models
{
    public partial class Group
    {
        public Group()
        {
            Grouping = new HashSet<Grouping>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
