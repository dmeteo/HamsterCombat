using CSharpClicker.UseCases.Common;
using MediatR;

namespace CSharpClicker.UseCases.BuyBoost;

public record BuyBoostCommand(int BoostId) : IRequest<ScoreBoostDto>;
