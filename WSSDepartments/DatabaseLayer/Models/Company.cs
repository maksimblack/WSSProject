using System;
using System.Collections.Generic;

namespace WSSDepartments.DatabaseLayer.Models
{
    public partial class Company
    {
        public Company()
        {
            Departments = new HashSet<Department>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
