using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class Labor
    {
        public int LaborId { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        // Default trade
        public int? TradeId { get; set; }
        public Trade Trade { get; set; }

        public string Name { get; set; }

        public ICollection<ProjectLabor> ProjectsWorked { get; set; }
        public ICollection<LaborWorkPerformed> LaborWorkPerformed { get; set; }
    }
}
