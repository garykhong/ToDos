namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeWhatToDoRequiredToBeFilledIn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "WhatToDo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "WhatToDo", c => c.String());
        }
    }
}
