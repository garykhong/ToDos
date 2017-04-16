namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WhatToDo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoes");
        }
    }
}
