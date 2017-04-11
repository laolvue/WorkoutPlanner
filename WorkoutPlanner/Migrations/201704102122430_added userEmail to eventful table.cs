namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserEmailtoeventfultable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventfuls", "userEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventfuls", "userEmail");
        }
    }
}
