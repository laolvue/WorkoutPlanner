namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstatuscolumntobuddytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buddies", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buddies", "status");
        }
    }
}
