namespace RoadDefaulters.Repositories
{
	public interface IAccount
	{
		public Task<bool> LoginUser(AppUserDto user);
		public Task<bool> RegisterUser(AppUserDto user);
	}
	public class AccountRepository : IAccount
	{
		private readonly AppDBContext context;
		public readonly UserManager<AppUser> userManager;

		public AccountRepository(AppDBContext context, UserManager<AppUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public async Task<bool> LoginUser(AppUserDto user)
		{
			var login_user = await userManager.FindByEmailAsync(user.Email);
			if (login_user is not null)
				if (await userManager.CheckPasswordAsync(login_user, user.Password))
					return true;
			return false;
		}

		public async Task<bool> RegisterUser(AppUserDto user)
		{
			var new_user = new AppUser
			{
				Email = user.Email,
				UserName = user.Email,
				FullNames = user.FullNames,
			};
			var result = await userManager.CreateAsync(new_user,user.Password);
			if (result.Succeeded)
				return true;
			return false;
		}
	}
}
