using System.Threading;
using System.Threading.Tasks;

namespace YarnSpinnerGodot;

public interface IActionMarkupHandler
{
    Task RunReproAsync(CancellationToken cancellationToken);
}
