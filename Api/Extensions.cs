using System.Linq;
using Velvetech.Domain.Entities;
using Velvetech.Presentation.Shared.Dtos;

namespace Velvetech.Api
{
	public static class Extensions
	{
		public static StudentDto ToDto(this Student source) =>
			new StudentDto
			{
				Id = source.Id,
				Firstname = source.Firstname,
				Middlename = source.Middlename,
				Lastname = source.Lastname,
				Callsign = source.Callsign,
				SexId = source.SexId,
				Sex = source.Sex?.ToDto(),
				Groups = source.Grouping?.Select(grouping => grouping.Group.Name)
			};

		public static GroupDto ToDto(this Group source) =>
			new GroupDto
			{
				Id = source.Id,
				Name = source.Name,
				StudentsCount = source.Grouping.Count
			};

		public static SexDto ToDto(this Sex source) =>
			new SexDto
			{
				Id = source.Id,
				Name = source.Name
			};
	}
}
