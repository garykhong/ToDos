namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderIDToDo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "OrderID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "OrderID");
        }
    }
}
