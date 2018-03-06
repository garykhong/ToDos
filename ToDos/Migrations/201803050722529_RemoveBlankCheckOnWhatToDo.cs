namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBlankCheckOnWhatToDo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "WhatToDo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "WhatToDo", c => c.String(nullable: false));
        }
    }
}
