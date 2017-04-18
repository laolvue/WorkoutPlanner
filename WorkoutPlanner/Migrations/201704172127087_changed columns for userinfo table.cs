namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedcolumnsforuserinfotable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "height", c => c.String());
            AlterColumn("dbo.UserInfoes", "weight", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "weight", c => c.Int(nullable: false));
            AlterColumn("dbo.UserInfoes", "height", c => c.Int(nullable: false));
        }
    }
}
