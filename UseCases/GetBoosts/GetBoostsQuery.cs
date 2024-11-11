using MediatR;

namespace CSharpClicker.UseCases.GetBoosts;

public class GetBoostsQuery : IRequest<IReadOnlyCollection<BoostDto>>;
