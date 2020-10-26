using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class ProjectContractor
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public bool IsPrime { get; set; }
    }
}
