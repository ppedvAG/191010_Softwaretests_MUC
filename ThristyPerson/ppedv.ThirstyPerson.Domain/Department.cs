using System.Collections.Generic;

namespace ppedv.ThirstyPerson.Domain
{
    public class Department : Entity
    {
        public string Name { get; set; }
        public virtual Person Head { get; set; }
        public virtual HashSet<Person> Members { get; set; } = new HashSet<Person>();
    }
}
