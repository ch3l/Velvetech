namespace Velvetech.Web.Requests
{
	public class StudentFilterPagedRequest
	{
		private int? _pageSize;
		private int? _pageIndex;

		public int? PageSize
		{
			get => _pageSize;
			set => _pageSize = value < 10 ? 10 : value;
		}

		public int? PageIndex
		{
			get => _pageIndex;
			set => _pageIndex = value < 0 ? 0 : value;
		}

		public string Sex { get; set; }
		public string Fullname { get; set; }
		public string Callsign { get; set; }
		public string Group { get; set; }
	}
}
