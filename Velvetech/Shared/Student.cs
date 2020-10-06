using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Velvetech.Server.Models
{
    public partial class Student
    {
        public Student()
        {
            Grouping = new HashSet<Grouping>();
        }

        [Key]
        public Guid Id { get; set; }
        public int SexId { get; set; }

        [Required]
        [StringLength(40)]
        public string Surname { get; set; }
        
		[Required]
        [StringLength(40)]
        public string FirstName { get; set; }
        
		[StringLength(60)]
        public string MiddleName { get; set; }
        
		[StringLength(16, MinimumLength = 6)]
        public string Callsign { get; set; }

        [ForeignKey(nameof(SexId))]
        [InverseProperty("Student")]          
		public virtual Sex Sex { get; set; }
        
		[InverseProperty("Student")]
        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
