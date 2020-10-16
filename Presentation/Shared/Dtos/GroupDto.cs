using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Presentation.Shared.Dtos
{
	public class GroupDto
	{
		public GroupDto()
		{
		}

		public GroupDto(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
