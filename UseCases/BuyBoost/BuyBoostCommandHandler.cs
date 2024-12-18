using CSharpClicker.Domain;
using CSharpClicker.DomainServices;
using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.BuyBoost;

public class BuyBoostCommandHandler : IRequestHandler<BuyBoostCommand, ScoreBoostDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public BuyBoostCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<ScoreBoostDto> Handle(BuyBoostCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
            .Include(user => user.UserBoosts)
            .ThenInclude(ub => ub.Boost)
            .FirstAsync(user => user.Id == userId);

        var boost = await appDbContext.Boosts
            .FirstAsync(b => b.Id == request.BoostId);

        var existingUserBoost = user.UserBoosts.FirstOrDefault(ub => ub.BoostId == request.BoostId);


        var price = 0L;

        UserBoost userBoost = existingUserBoost!;
        if (existingUserBoost != null)
        {
            price = existingUserBoost.CurrentPrice;
            existingUserBoost.Quantity++;
            existingUserBoost.CurrentPrice = Convert.ToInt64(existingUserBoost.CurrentPrice * Constants.BoostCostModifier);
        }
        else
        {
            price = boost.Price;
            var newUserBoost = new UserBoost()
            {
                Boost = boost,
                CurrentPrice = Convert.ToInt64(boost.Price * Constants.BoostCostModifier),
                Quantity = 1,
                User = user,
            };

            userBoost = newUserBoost;
            await appDbContext.UserBoosts.AddAsync(newUserBoost);
        }

        if (price > user.CurrentScore)
        {
            throw new InvalidCastException("Not enough score to buy a boost");
        }

        user.CurrentScore -= price;

        await appDbContext.SaveChangesAsync();

        return new ScoreBoostDto
        {
            Score = new ScoreDto
            {
                CurrentScore = user.CurrentScore,
                RecordScore = user.RecordScore,
                ProfitPerClick = user.UserBoosts.GetProfit(),
                ProfitPerSecond = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: true)
            },
            Price = userBoost.CurrentPrice,
            Quantity = userBoost.Quantity,

        };
    }
}
