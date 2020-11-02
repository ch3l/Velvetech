using System.Threading.Tasks;

namespace Velvetech.Domain.Services.Internal.Interfaces
{
	public interface IStudentValidationService
	{
		Task<bool> CallsignExistsAsync(string value);
	}
}