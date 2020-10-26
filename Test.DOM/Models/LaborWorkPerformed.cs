using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.DOM.Models
{
    public class LaborWorkPerformed
    {
        public int WorkPerformedId { get; set; }
        public WorkPerformed WorkPerformed { get; set; }
        public int LaborId { get; set; }
        public Labor Labor { get; set; }
        public int TradeId { get; set; }
        public Trade Trade { get; set; }


        [Column(TypeName = "decimal(3,1)")]
        public decimal RegHours { get; set; }
        [Column(TypeName = "decimal(3,1)")]
        public decimal OtHours { get; set; }
    }
}
