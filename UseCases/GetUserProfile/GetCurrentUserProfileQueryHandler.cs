using AutoMapper;
using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.UseCases.GetLeaderboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.GetUserProfile
{
	public class GetCurrentUserProfileQueryHandler : IRequestHandler<GetCurrentUserProfileQuery, UserProfileDto>
	{
		private readonly IMapper mapper;
		private readonly IAppDbContext appDbContext;

		public GetCurrentUserProfileQueryHandler(IMapper mapper, IAppDbContext appDbContext)
		{
			this.mapper = mapper;
			this.appDbContext = appDbContext;
		}

		public async Task<UserProfileDto> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
		{
			var userId = request.id;

			var user = await appDbContext.ApplicationUsers
				.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

			if (user == null)
			{
				throw new Exception($"User with ID {userId} not found");
			}
			return new UserProfileDto()
			{
				UserName = user.UserName,
				Avatar = user.Avatar,
				RecordScore = user.RecordScore,
			};
		}
	}
}
