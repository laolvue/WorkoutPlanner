namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedworkoutNametoworkouttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "workoutName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "workoutName");
        }
    }
}
