using System.Data.Entity;

//Do this repository pattern: http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
//Or this one: http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/advanced-entity-framework-scenarios-for-an-mvc-web-application
//And look at this: http://thedatafarm.com/data-access/how-ef6-enables-mocking-dbsets-more-easily/
//And this too: https://msdn.microsoft.com/en-us/data/dn314429

namespace Budgeting.Entity.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();

        IDbSet<T> UserAccounts<T>() where T : class;
        IDbSet<T> Periods<T>() where T : class;
        IDbSet<T> Items<T>() where T : class;
    }
}
