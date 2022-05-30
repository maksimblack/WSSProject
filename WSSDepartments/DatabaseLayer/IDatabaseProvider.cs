using System.Collections.Generic;
using System.Threading.Tasks;
using WSSDepartments.BusinessLayer.DataTransferObjects;

namespace WSSDepartments.DatabaseLayer
{
	public interface IDatabaseProvider
	{
		Task<List<DT_Company>> GetCompaniesAsync();
		Task<List<DT_Company>> SaveCompaniesAsync(List<DT_Company> dtCompanies);
		Task<List<DT_Company>> SaveCompanyAsync(DT_Company company);
	}
}
