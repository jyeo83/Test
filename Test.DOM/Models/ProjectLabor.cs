using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class ProjectLabor
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int LaborId { get; set; }
        public Labor Labor { get; set; }
    }
}
