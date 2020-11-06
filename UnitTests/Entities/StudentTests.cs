using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;
using Velvetech.UnitTests.Helpers;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class StudentTests
	{
		private const string ClassName = nameof(Student);

		private const string SexId = nameof(Student.SexId);
		private const int SexIdLowerBoundary = 1;
		private const int SexIdUpperBoundary = 2;

		private const string Firstname = nameof(Student.Firstname);
		private const int FirstnameLengthUpperBoundary = 40;

		private const string Middlename = nameof(Student.Middlename);
		private const int MiddlenameLengthUpperBoundary = 60;

		private const string Lastname = nameof(Student.Lastname);
		private const int LastnameLengthUpperBoundary = 40;

		private const string Callsign = nameof(Student.Callsign);
		private const int CallsignLengthLowerBoundary = 6;
		private const int CallsignLengthUpperBoundary = 16;

		[TestMethod]
		public void SetSexIdTest()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void SetFirstnameTest()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void SetMiddlenameTest()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void SetLastnameTest()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void SetCallsignTest()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void ExcludeFromAllGroupsTest()
		{
			Assert.Fail();
		}
	}
}