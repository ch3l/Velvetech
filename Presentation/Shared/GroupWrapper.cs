using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
//using System.Web.;

namespace Velvetech.Presentation.Shared
{
	public class GroupWrapper 
	{					 		
		public Guid Id { get; set; }			
		public string Name{ get; set; }
		public int Count { get; set; }
	}
}
