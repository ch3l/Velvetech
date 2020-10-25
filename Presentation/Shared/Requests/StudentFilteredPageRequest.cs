namespace Velvetech.Presentation.Shared.Requests
{
	public class StudentFilteredPageRequest
	{	   
		public string Sex { get; set; }
		public string Fullname { get; set; }
		public string Callsign { get; set; }
		public string Group { get; set; }
		public int PageSize { get; set; }
		public int PageIndex { get; set; }
	}
}
