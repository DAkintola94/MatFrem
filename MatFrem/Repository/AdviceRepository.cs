using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.Repository
{
	public class AdviceRepository : IAdviceRepository
	{
		private readonly AppDBContext _context;
		public AdviceRepository(AppDBContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AdviceModel>> GetAllAdvice()
		{
			var getAllData = await _context.Advice.Take(50).ToListAsync();
			return getAllData;
		}

		public async Task<AdviceModel?> GetAdviceById(int id)
		{
			return await _context.Advice.Where(x => x.PostId == id).FirstOrDefaultAsync();
		}

		public async Task<AdviceModel?> DeleteAdvice(int id)
		{
			var findIdAndDelete = await _context.Advice.FindAsync(id);
			if(findIdAndDelete != null)
			{
			 	_context.Advice.Remove(findIdAndDelete);
				await _context.SaveChangesAsync();
				return findIdAndDelete;
			}
			return null;
		}

		public async Task<AdviceModel?> AddAdvice(AdviceModel adviceModel)
		{
			await _context.Advice.AddAsync(adviceModel);
			await _context.SaveChangesAsync();
			return adviceModel;
		}

	}
}
