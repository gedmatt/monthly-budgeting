using Budgeting.Core.Exceptions;
using Budgeting.Core.Interfaces;
using Budgeting.Entity;
using Budgeting.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Budgeting.Bus
{
    public class PeriodBO : ISave
    {
        public int PeriodId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public decimal StartingBalance { get; set; }

        public PeriodBO(ModelContext context, int periodId)
        {
            var period = context.Periods.SingleOrDefault(x => x.PeriodId == periodId);
            if (period == null)
            {
                throw new ObjectNotFoundException("Id not found in the database");
            }
            PeriodId = period.PeriodId;
            Title = period.Title;
            StartDate = period.StartDate;
            StartingBalance = period.StartingBalance;
        }

        public void Save(int userId)
        {
            //TODO: Stuff here!
        }
    }
}
