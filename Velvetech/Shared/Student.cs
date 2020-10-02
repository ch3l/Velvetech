using System;
using System.Collections.Generic;


namespace Velvetech.Server.Models
{
	public partial class Student 
	{
		public Guid Id { get; set; }
		public int SexId { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public string Patronymic { get; set; }
		public string Callsign { get; set; }

		public virtual Sex Sex { get; set; }
	}
}
