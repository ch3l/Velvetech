using System.Linq;

using Presentation.Shared.Dtos;

using Velvetech.Domain.Entities;

namespace Velvetech.Presentation.Server
{
	public static class Extensions
	{
		public static StudentDto ToDto(this Student source) =>
			new StudentDto
			{
				Id = source.Id,
				FirstName = source.FirstName,
				MiddleName = source.MiddleName,
				LastName = source.LastName,
				Callsign = source.Callsign,
				Sex = source.Sex.ToDto(),
				Groups = source.Grouping.Select(grouping => grouping.Group.ToDto())
			};

		public static GroupDto ToDto(this Group source) =>
			new GroupDto
			{
				Id = source.Id,
				Name = source.Name
			};

		public static SexDto ToDto(this Sex source) =>
			new SexDto
			{
				Id = source.Id,
				Name = source.Name
			};
	}
}
