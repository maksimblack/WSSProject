using System.Collections.Generic;

namespace WSSDepartments.BusinessLayer.DataTransferObjects
{
	public class DT_Department
	{
		public int DepartmentId { get; set; }
		
		public string Name { get; set; }

		public string Description { get; set; }

		public DT_Company Company { get; set; }
		
		public List<DT_Division> Divisions { get; set; }
	}
}
