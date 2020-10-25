using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Presentation.Shared.Requests;

namespace Velvetech.Presentation.Server.Filtering
{
	public class GroupFilter : FilterExpressions<Group, string>
	{
		public GroupFilter(string groupName) : base(groupName)
		{
		}

		public override IEnumerator<Expression<Func<Group, bool>>> GetEnumerator()
		{
			if (!string.IsNullOrEmpty(Request))
				yield return (group) => group.Name.Contains(Request);
		}
	}
}
