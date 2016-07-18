﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeting.Core.Interfaces
{
    public interface ISave
    {
        DateTime CreatedDate { get; }
        DateTime? LastModifiedByDate { get; }
        void Save();
    }
}
