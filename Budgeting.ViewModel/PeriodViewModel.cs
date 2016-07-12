using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeting.ViewModel
{
    public class PeriodViewModel
    {
        public int PeriodId { get; set; }
        public int UserAccountId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public decimal StartingBalance { get; set; }

    }
}
