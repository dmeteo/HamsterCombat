using AutoMapper;
using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CSharpClicker.UseCases.GetLeaderboard;

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, LeaderboardDto>
{
	private readonly IAppDbContext appDbContext;
	private readonly IMapper mapper;

	public GetLeaderboardQueryHandler(IAppDbContext appDbContext, IMapper mapper)
	{
		this.appDbContext = appDbContext;
		this.mapper = mapper;
	}

	public async Task<LeaderboardDto> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
	{
		var offset = request.Page - 1 * Constants.PageSize;
		var usersByRecordScore = await mapper.ProjectTo<LeaderboardUserDto>(appDbContext
				.ApplicationUsers.OrderByDescending(user => user.RecordScore)
				.Skip(offset)
				.Take(Constants.PageSize))
			.ToArrayAsync();

		var usersTotal = appDbContext.ApplicationUsers.Count() / Constants.PageSize;			
		var pagesTotal = usersTotal % Constants.PageSize == 0
			? usersTotal / Constants.PageSize
			: usersTotal / Constants.PageSize + 1; 

		return new LeaderboardDto()
		{
			Users = usersByRecordScore,
			PageInfo = new PageInfoDto
			{
				Page = request.Page,
				Total = pagesTotal,
			}
		};
	}
}
