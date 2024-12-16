using CSharpClicker.UseCases.GetBoosts;
using CSharpClicker.UseCases.GetCurrentUser;

namespace CSharpClicker.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }

    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
