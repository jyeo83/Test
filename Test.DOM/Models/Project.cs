using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int District { get; set; }
        public string EA { get; set; }

        public ICollection<ProjectLabor> Labors { get; set; }
        public ICollection<ProjectContractor> Contractors { get; set; }
    }
}
