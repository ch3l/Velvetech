using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Shared.Requests
{
	public class StudentByIdRequest	
	{
		public Guid Id { get; set; }

		public StudentByIdRequest(Guid id)
		{
			Id = id;
		}
	}
}
