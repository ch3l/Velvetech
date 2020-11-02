using System.Threading.Tasks;

namespace Velvetech.Domain.Services.Internal.Interfaces
{
	public interface IStudentValidationService
	{
		bool CallsignExists(string value);
	}
}