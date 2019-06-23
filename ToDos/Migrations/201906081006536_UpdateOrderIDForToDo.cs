namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderIDForToDo : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE ToDoes SET OrderID = ID");
        }
        
        public override void Down()
        {
            Sql("UPDATE ToDoes SET OrderID = NULL");
        }
    }
}
