﻿using System;
using System.Collections.Generic;

namespace Velvetech.Shared.Dtos
{
	public class StudentDto
	{
		public Guid Id { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public string Lastname { get; set; }
		public string Callsign { get; set; }

		public int SexId { get; set; }
		public SexDto Sex { get; set; }
		public IEnumerable<string> Groups { get; set; }

		public string FullName => Firstname + " " + Middlename + " " + Lastname;
	}
}
