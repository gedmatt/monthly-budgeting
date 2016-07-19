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
    public class PeriodBO : ISave, IArchive
    {
        protected Period _periodEntity;
        public int PeriodId
        {
            get { return _periodEntity.PeriodId; }
        }
        public string Title
        {
            get { return _periodEntity.Title; }
        }
        public DateTime StartDate
        {
            get { return _periodEntity.StartDate; }
        }
        public decimal StartingBalance
        {
            get { return _periodEntity.StartingBalance; }
        }
        public decimal UserAccountId
        {
            get { return _periodEntity.UserAccountId; }
        }

        public DateTime CreatedDate
        {
            get { return _periodEntity.CreatedDate; }
        }
        public DateTime? LastModifiedByDate
        {
            get { return _periodEntity.LastModifiedDate; }
        }
        public DateTime? ArchivedDate
        {
            get { return _periodEntity.ArchivedDate; }
        }

        public PeriodBO(ModelContext context, int periodId)
        {
            _periodEntity = context.Periods.SingleOrDefault(x => x.PeriodId == periodId);
            if (_periodEntity == null)
            {
                throw new ObjectNotFoundException("Id not found in the database");
            }
        }

        public PeriodBO(ModelContext context, PeriodViewModel periodVM)
        {
            if (periodVM.PeriodId > 0)
            {
                _periodEntity = context.Periods.SingleOrDefault(x => x.PeriodId == periodVM.PeriodId);

                if (_periodEntity == null)
                {
                    throw new ObjectNotFoundException("Id not found in the database");
                }
            }else
            {
                _periodEntity = new Period();
            }
            _periodEntity.Title = periodVM.Title;
            _periodEntity.StartDate = periodVM.StartDate;
            _periodEntity.StartingBalance = periodVM.StartingBalance;
            _periodEntity.UserAccountId = periodVM.UserAccountId;
        }

        public void Save()
        {
            if(_periodEntity.CreatedDate == DateTime.MinValue)
            {
                _periodEntity.CreatedDate = DateTime.Now;
            }
            else
            {
                _periodEntity.LastModifiedDate = DateTime.Now;
            }
        }

        public void Archive()
        {
            if (!_periodEntity.ArchivedDate.HasValue)
            {
                _periodEntity.ArchivedDate = DateTime.Now;
            }
        }

        public void Unarchive()
        {
            if (_periodEntity.ArchivedDate.HasValue)
            {
                _periodEntity.ArchivedDate = null;
            }
        }

        public static IQueryable<PeriodViewModel> GetPeriodList(ModelContext context, int userAccountId)
        {
            var periods = context.Periods.Where(x => x.UserAccountId == userAccountId && !x.ArchivedDate.HasValue);
            return (from p in periods
                    select new PeriodViewModel
                    {
                        PeriodId = p.PeriodId,
                        Title = p.Title,
                        StartDate = p.StartDate,
                        StartingBalance = p.StartingBalance,
                        UserAccountId = p.UserAccountId
                    });
        }
    }
}
