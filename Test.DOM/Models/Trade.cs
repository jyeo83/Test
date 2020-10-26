using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public string Title { get; set; }

        public ICollection<LaborWorkPerformed> LaborWorkPerformed { get; set; }
    }
}
