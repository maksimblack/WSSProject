using System.Collections.Generic;
using System.Xml.Serialization;
using WSSDepartments.UILayer;

namespace WSSDepartments.Services
{
	[XmlRoot("CompanyList")]
	[XmlInclude(typeof(Company))]
	public class CompanyList
	{
		[XmlArray("CompanyArray")]
		[XmlArrayItem("Company")]
		public List<Company> Companies { get; set; }
	}
}
