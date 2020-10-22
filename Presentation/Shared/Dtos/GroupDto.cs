using System;

namespace Velvetech.Presentation.Shared.Dtos
{
	public class GroupDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		
		public int StudentsCount { get; set; }
	}
}
