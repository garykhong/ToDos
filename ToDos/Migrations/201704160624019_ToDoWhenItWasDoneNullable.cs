namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoWhenItWasDoneNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "WhenItWasDone", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "WhenItWasDone", c => c.DateTime(nullable: false));
        }
    }
}
