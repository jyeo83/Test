using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class WorkPerformed
    {
        public int WorkPerformedId { get; set; }
        public ICollection<LaborWorkPerformed> LaborWorkPerformed { get; set; }
    }
}
