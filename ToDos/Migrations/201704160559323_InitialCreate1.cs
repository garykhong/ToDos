namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "WhenItWasDone", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "WhenItWasDone");
        }
    }
}
