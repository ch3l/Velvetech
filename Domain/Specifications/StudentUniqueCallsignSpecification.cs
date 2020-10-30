using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class StudentUniqueCallsignSpecification : Specification<Student>
	{
		public StudentUniqueCallsignSpecification(string callsign)
		{
			Query.Where(student => student.Callsign == callsign);
		}
	}
}