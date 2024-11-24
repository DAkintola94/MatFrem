using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
	public interface IAdviceRepository
	{
		Task<AdviceModel> GetAdviceById(int id);
		Task<IEnumerable<AdviceModel>> GetAllAdvice();
		Task<AdviceModel> AddAdvice(AdviceModel adviceModel);
		Task<AdviceModel> DeleteAdvice(int id);


	}
}
