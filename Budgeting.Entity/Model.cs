namespace Budgeting.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Linq;
    
    public class ModelContext : DbContext
    {
        // Your context has been configured to use a 'ModelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Budgeting' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.

        //http://www.codeproject.com/Articles/813912/Create-Primary-Key-using-Entity-Framework-Code-Fir

        public ModelContext()
            : base("name=ModelContext")
        {
            //Database.SetInitializer<ModelContext>(new CreateDatabaseIfNotExists<ModelContext>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        
    }
    
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual List<Period> Periods {get; set;}
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? ArchivedDate { get; set; }
    }

    public class Period
    {
        public int PeriodId { get; set; }
        public int UserAccountId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public Decimal StartingBalance { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? ArchivedDate { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public int PeriodId { get; set; }
        public string ItemDate { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public Period Period { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? ArchivedDate { get; set; }
    }
}