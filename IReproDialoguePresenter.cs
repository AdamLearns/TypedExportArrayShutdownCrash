using System.Threading;
using System.Threading.Tasks;

namespace YarnSpinnerGodot;

public interface IReproDialoguePresenter
{
    Task RunPresenterAsync(CancellationToken cancellationToken);
}
