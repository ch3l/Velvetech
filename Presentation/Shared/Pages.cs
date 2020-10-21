using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Shared
{
	public class Page<T>
	{
		public bool IsLastPage { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public T[] Items { get; set; }

	}
}
