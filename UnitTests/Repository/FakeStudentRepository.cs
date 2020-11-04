using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using Velvetech.Domain.Entities;
using Velvetech.UnitTests.Repository.Base;

namespace Velvetech.UnitTests.Repository
{
	class FakeStudentRepository	: FakeRepository<Student, Guid>
	{
		protected override Guid NewKey()
		{
			return Guid.NewGuid();
		}
	}
}
