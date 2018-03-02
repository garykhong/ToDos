namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDoFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToDoID = c.Int(nullable: false),
                        Name = c.String(),
                        ContentType = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ToDoes", t => t.ToDoID, cascadeDelete: true)
                .Index(t => t.ToDoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoFiles", "ToDoID", "dbo.ToDoes");
            DropIndex("dbo.ToDoFiles", new[] { "ToDoID" });
            DropTable("dbo.ToDoFiles");
        }
    }
}
