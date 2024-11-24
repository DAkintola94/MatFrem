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

        /// <summary>
        /// Retrieves up to 50 advice records from the database.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of <see cref="AdviceModel"/> objects representing the retrieved advice records.
        /// </returns>
        public async Task<IEnumerable<AdviceModel>> GetAllAdvice()
		{
			var getAllData = await _context.Advice.Take(50).ToListAsync();
			return getAllData;
		}

        /// <summary>
        /// Retrieves a specific advice record by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the advice record to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="AdviceModel"/> object representing the advice record, 
        /// or <c>null</c> if no record with the given ID exists in the database.
        /// </returns>
        public async Task<AdviceModel> GetAdviceById(int id)
		{
			return await _context.Advice.Where(x => x.PostId == id).FirstOrDefaultAsync();
		}

        /// <summary>
        /// Deletes a specific advice record from the database by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the advice record to delete.
        /// </param>
        /// <returns>
        /// The <see cref="AdviceModel"/> object representing the deleted advice record, 
        /// or <c>null</c> if no record with the given ID was found.
        /// </returns>
        public async Task<AdviceModel> DeleteAdvice(int id)
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

        /// <summary>
        /// Adds a new advice record to the database.
        /// </summary>
        /// <param name="adviceModel">
        /// The <see cref="AdviceModel"/> object containing the details of the advice to add.
        /// </param>
        /// <returns>
        /// The <see cref="AdviceModel"/> object that was added, with updated information such as a generated ID.
        /// </returns>
        public async Task<AdviceModel> AddAdvice(AdviceModel adviceModel)
		{
			await _context.Advice.AddAsync(adviceModel);
			await _context.SaveChangesAsync();
			return adviceModel;
		}

	}
}
