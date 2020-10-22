using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Shared.Requests
{
	public class StudentGroupRequest
	{
		public Guid StudentId { get; set; }
		public Guid GroupId { get; set; }
	}
}
