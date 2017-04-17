namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredchatroomtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatRooms", "userId", "dbo.UserInfoes");
            DropIndex("dbo.ChatRooms", new[] { "userId" });
            AddColumn("dbo.ChatRooms", "buddyOne", c => c.String());
            AddColumn("dbo.ChatRooms", "buddyTwo", c => c.String());
            AddColumn("dbo.ChatRooms", "message", c => c.String());
            AddColumn("dbo.ChatRooms", "timeSent", c => c.DateTime(nullable: false));
            DropColumn("dbo.ChatRooms", "chatroom");
            DropColumn("dbo.ChatRooms", "userId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatRooms", "userId", c => c.Int(nullable: false));
            AddColumn("dbo.ChatRooms", "chatroom", c => c.Int(nullable: false));
            DropColumn("dbo.ChatRooms", "timeSent");
            DropColumn("dbo.ChatRooms", "message");
            DropColumn("dbo.ChatRooms", "buddyTwo");
            DropColumn("dbo.ChatRooms", "buddyOne");
            CreateIndex("dbo.ChatRooms", "userId");
            AddForeignKey("dbo.ChatRooms", "userId", "dbo.UserInfoes", "userId", cascadeDelete: true);
        }
    }
}
