namespace CSharpClicker.UseCases.GetUserProfile;


public class UserProfileDto
{
	public string UserName { get; init; }

	public long RecordScore { get; init; }

	public byte[] Avatar { get; init; }
}
