using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSDepartments.BusinessLayer.DataTransferObjects
{
	public class DT_Division
	{
		public int DivisionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DT_Department Department { get; set; }
	}
}
