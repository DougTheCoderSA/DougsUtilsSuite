namespace SQLPivot
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelDBSchema : DbContext
    {
        // Your context has been configured to use a 'ModelDBSchema' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SQLPivot.ModelDBSchema' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelDBSchema' 
        // connection string in the application configuration file.
        public ModelDBSchema()
            : base("name=ModelDBSchema")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Table> Table { get; set; }
    }

    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Column
    {

    }
}