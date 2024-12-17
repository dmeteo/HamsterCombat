using CSharpClicker.UseCases.BuyBoost;
using CSharpClicker.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;


[Route("boost")]
public class BoostController : ControllerBase
{
    private readonly IMediator mediator;
    public BoostController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("buy")]
    public async Task<ScoreDto> Buy(BuyBoostCommand command)
        => await mediator.Send(command);
}
