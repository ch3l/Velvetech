using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Velvetech.Data.Seeds.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;

namespace Velvetech.Data.Seeds
{
	public class SexSeed : Seed<Sex, int>
	{
		public SexSeed(ICrudService<Sex, int> crudService) 
			: base(crudService)
		{
		}

		protected override IEnumerable<Sex> Enumerate()
		{
			yield return new Sex(1, "Female");
			yield return new Sex(2, "Male");
		}
	}
}
