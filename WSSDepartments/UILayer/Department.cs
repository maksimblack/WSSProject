using System.Collections.Generic;
using System.Xml.Serialization;

namespace WSSDepartments.UILayer
{
	public class Department
	{
		public int DepartmentId { get; set; }
		
		public string Name { get; set; }

		public string Description { get; set; }

		public int CompanyId { get; set; }

		[XmlArray("Divisions")]
		[XmlArrayItem("Division")]
		public List<Division> Divisions { get; set; }
	}
}
