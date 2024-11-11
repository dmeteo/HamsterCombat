using CSharpClicker.UseCases.GetBoosts;

namespace CSharpClicker.ViewModels;

public class IndexViewModel
{
    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
