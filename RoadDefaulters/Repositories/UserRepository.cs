
namespace RoadDefaulters.Repositories
{
    public interface IUser
    {
        public Task<IEnumerable<Penalty>> GetPenaltyAsync();
        public Task<IEnumerable<Offence>> GetOffencesAsync();
        public Task<RoadUser> ModifyAsync(Penalty penalty);
        public Task<bool> CreateAsync(UserViewModel model);
        public Task<Penalty> GetPenalty(int penaltyID);
        public Task<bool> DeleteAsync(int id);

    }
    public class UserRepository : IUser
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<RoadUser> ModifyAsync(Penalty penalty)
        {
            if (penalty.User.RoadUserID > 0)
                context.Entry(penalty.User).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return penalty.User;
        }

        public async Task<IEnumerable<Penalty>> GetPenaltyAsync()
        {
            var result = await context.Penalties.Include(u => u.User).Include(o => o.Offence).ToListAsync();
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var itemToDelete = await context.Penalties.FindAsync(id);
            if (itemToDelete != null)
                context.Remove(itemToDelete);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Penalty> GetPenalty(int penaltyID)
        {
            var penalty = await context.Penalties.FindAsync(penaltyID);
            return penalty;
        }

        public async Task<bool> CreateAsync(UserViewModel model)
        {
            await context.Users.AddAsync(model.User);
            await context.SaveChangesAsync();
            var user = await context.Users.OrderByDescending(x=>x.RoadUserID).FirstOrDefaultAsync();
            var penalty = new Penalty
            {
                RoadUserID = user.RoadUserID,
                PenaltyDate = DateTime.UtcNow,
                OffenceID = model.OffenseID,
                Details = model.Details
            };
            await context.Penalties.AddAsync(penalty);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Offence>> GetOffencesAsync()
        {
            var offences = await context.Offences.ToListAsync();
            return offences;
        }
    }
}
