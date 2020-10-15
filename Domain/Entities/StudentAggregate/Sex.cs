﻿using System;
using System.Collections.Generic;
using System.Text;

using Domain.Common;

namespace Domain.Entities.StudentAggregate
{
	class Sex : Entity<int>
	{
		public string Name { get; private set; }
	}
}
