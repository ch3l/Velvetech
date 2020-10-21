using System.Linq;

using Castle.DynamicProxy.Generators;

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
				Firstname = source.Firstname,
				Middlename = source.Middlename,
				Lastname = source.Lastname,
				Callsign = source.Callsign,
				SexId = source.SexId,
				//Groups = source.Grouping.Select(grouping => grouping.Group.ToDto())
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

		public static Student FromDto(this StudentDto source) =>
			new Student(
				source.Id,
				source.SexId,
				source.Firstname,
				source.Middlename,
				source.Lastname,
				source.Callsign);
	}
}
