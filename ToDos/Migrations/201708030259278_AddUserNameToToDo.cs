namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameToToDo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "UserName");
        }
    }
}
