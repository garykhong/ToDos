namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDoReminder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoReminders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToDoID = c.Int(nullable: false),
                        FirstReminderDate = c.DateTime(nullable: false),
                        FrequencyType = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ToDoes", t => t.ToDoID, cascadeDelete: true)
                .Index(t => t.ToDoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoReminders", "ToDoID", "dbo.ToDoes");
            DropIndex("dbo.ToDoReminders", new[] { "ToDoID" });
            DropTable("dbo.ToDoReminders");
        }
    }
}
