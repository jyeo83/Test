using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        public string Name { get; set; }

        public ICollection<Labor> Labors { get; set; }
        public ICollection<ProjectContractor> Projects { get; set; }
    }
}
