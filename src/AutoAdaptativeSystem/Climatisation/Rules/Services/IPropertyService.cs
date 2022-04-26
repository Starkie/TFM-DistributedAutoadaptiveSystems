namespace Climatisation.Rules.Service.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IPropertyService
{
    Task<T> GetProperty<T>(string propertyName, CancellationToken cancellationToken);
}
