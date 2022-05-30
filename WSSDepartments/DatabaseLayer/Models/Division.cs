using System;
using System.Collections.Generic;

#nullable disable

namespace WSSDepartments.DatabaseLayer.Models
{
    public partial class Division
    {
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
