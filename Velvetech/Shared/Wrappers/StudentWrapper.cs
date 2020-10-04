using System;
using System.Collections.Generic;


namespace Velvetech.Shared.Wrappers
{
	public class StudentWrapper 
	{
		public Guid Id { get; set; }
		public string FullName { get; set; }
		public string Callsign { get; set; }
		public string Sex { get; set; }
		public IEnumerable<string> Groups { get; set; }
	}
}
