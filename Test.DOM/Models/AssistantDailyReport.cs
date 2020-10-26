using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class AssistantDailyReport
    {
        public int AssistantDailyReportId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime Date { get; set; }
        public string Narrative { get; set; }

        public ICollection<WorkPerformed> WorkPerformed { get; set; }
    }
}
