namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedworkouttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        workoutId = c.Int(nullable: false, identity: true),
                        workoutName = c.String(),
                        notes = c.String(),
                        exerciseID = c.Int(nullable: false),
                        set = c.Int(nullable: false),
                        rep = c.Int(nullable: false),
                        userEmail = c.String(),
                        day = c.String(),
                        workoutImage = c.Binary(),
                    })
                .PrimaryKey(t => t.workoutId)
                .ForeignKey("dbo.Exercises", t => t.exerciseID, cascadeDelete: true)
                .Index(t => t.exerciseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "exerciseID", "dbo.Exercises");
            DropIndex("dbo.Workouts", new[] { "exerciseID" });
            DropTable("dbo.Workouts");
        }
    }
}
