using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeting.Core.Interfaces
{
    public interface IArchive
    {
        DateTime? ArchivedDate { get; }
        void Archive();
        void Unarchive();
    }
}
