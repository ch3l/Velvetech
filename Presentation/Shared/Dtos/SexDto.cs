using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Shared.Dtos
{
	public class SexDto
	{
		public SexDto()
		{
		}

		public SexDto(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int Id { get; set; }
		public string Name { get; set; }
	}
}
