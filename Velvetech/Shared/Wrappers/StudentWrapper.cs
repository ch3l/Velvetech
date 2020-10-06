using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Velvetech.Shared.Wrappers
{
	public class StudentCommon
	{
		[Key]
		public Guid Id { get; set; }

		public string FullName { get; set; }
		
		public string Callsign { get; set; }

		public IEnumerable<string> Groups { get; set; }
	}
}
