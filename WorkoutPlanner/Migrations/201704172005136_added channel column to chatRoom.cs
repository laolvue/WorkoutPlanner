namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedchannelcolumntochatRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatRooms", "channel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatRooms", "channel");
        }
    }
}
