namespace Budgeting.Entity
{
    using Budgeting.Entity.Interfaces;
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq;

    //TODO: Build a repository interface for the DB Context to implement
    //TODO: Build a unit of work interface for it to use to get the data from somewhere

    public class ModelContext : DbContext //, IUnitOfWork
    {
        // Your context has been configured to use a 'ModelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Budgeting' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Item> Items { get; set; }

        public void Commit()
        {
            SaveChanges();
        }
    }
    
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual List<Period> Periods {get; set;}
    }

    public class Period
    {
        public int PeriodId { get; set; }
        public int UserAccountId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public Decimal StartingBalance { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public int PeriodId { get; set; }
        public string ItemDate { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
    }
}