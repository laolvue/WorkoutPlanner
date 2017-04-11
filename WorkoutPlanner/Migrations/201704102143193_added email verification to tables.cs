namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedemailverificationtotables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "userEmail", c => c.String());
            AddColumn("dbo.Exercises", "userEmail", c => c.String());
            AddColumn("dbo.Muscles", "userEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Muscles", "userEmail");
            DropColumn("dbo.Exercises", "userEmail");
            DropColumn("dbo.Workouts", "userEmail");
        }
    }
}
