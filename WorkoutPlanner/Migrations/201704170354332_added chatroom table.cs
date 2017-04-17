namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedchatroomtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        chatroom = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatRooms", "userId", "dbo.UserInfoes");
            DropIndex("dbo.ChatRooms", new[] { "userId" });
            DropTable("dbo.ChatRooms");
        }
    }
}
