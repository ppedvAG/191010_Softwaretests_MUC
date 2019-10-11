using System;
using System.Collections.Generic;

namespace ppedv.ThirstyPerson.Domain
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public virtual HashSet<Department> Departments { get; set; } = new HashSet<Department>();
    }
}
