using System;
using System.Collections.Generic;

#nullable disable

namespace WSSDepartments.DatabaseLayer.Models
{
    public partial class Department
    {
        public Department()
        {
            Divisions = new HashSet<Division>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
    }
}
