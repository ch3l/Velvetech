﻿using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities.StudentAggregate
{
	public class Sex : Entity<int>
	{
		public string Name { get; set; }

		public List<Student> Student { get; set; } = new List<Student>();
	}
}
