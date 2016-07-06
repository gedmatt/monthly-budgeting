using Budgeting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeting.Bus
{
    public class UserAccount
    {
        protected int _userId;
        protected int _username;
        protected int _accountId;

        public UserAccount(ModelContext context, string username, string password)
        {

        }
    }
}
