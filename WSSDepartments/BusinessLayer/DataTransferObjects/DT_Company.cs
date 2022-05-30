using System.Collections.Generic;

namespace WSSDepartments.BusinessLayer.DataTransferObjects
{
	public class DT_Company
	{
		public int CompanyId { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }
		
		public List<DT_Department> Departments { get; set; }
	}
}
