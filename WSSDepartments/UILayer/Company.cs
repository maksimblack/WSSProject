using System.Collections.Generic;
using System.Xml.Serialization;

namespace WSSDepartments.UILayer
{
	public class Company
	{
		public int CompanyId { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }

		[XmlArray("Departments")]
		[XmlArrayItem("Department")]
		public List<Department> Departments { get; set; }
	}
}
