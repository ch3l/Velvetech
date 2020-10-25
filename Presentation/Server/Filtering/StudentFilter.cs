using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Presentation.Shared.Requests;

namespace Velvetech.Presentation.Server.Filtering
{
	public class StudentFilter : FilterExpressions<Student, StudentFilteredPageRequest>
	{
		public StudentFilter(StudentFilteredPageRequest request) : base(request)
		{
		}

		public override IEnumerator<Expression<Func<Student, bool>>> GetEnumerator()
		{
			if (!string.IsNullOrEmpty(Request.Sex))
				yield return (student) => student.Sex.Name.Equals(Request.Sex);

			if (!string.IsNullOrEmpty(Request.Fullname))
				yield return (student) =>
					(student.Firstname + " " +
					 student.Middlename + " " +
					 student.Lastname).Contains(Request.Fullname);

			if (!string.IsNullOrEmpty(Request.Callsign))
				yield return (student) => student.Sex.Name.Equals(Request.Callsign);

			if (!string.IsNullOrEmpty(Request.Group))
				yield return (student) => student.Grouping.Any(grouping => grouping.Group.Name.Contains(Request.Group));
		}
	}
}
