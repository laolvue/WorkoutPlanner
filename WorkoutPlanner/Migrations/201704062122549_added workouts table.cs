namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedworkoutstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        workoutId = c.Int(nullable: false, identity: true),
                        muscleId = c.Int(nullable: false),
                        exerciseId = c.Int(nullable: false),
                        sets = c.Int(nullable: false),
                        reps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.workoutId)
                .ForeignKey("dbo.Exercises", t => t.exerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Muscles", t => t.muscleId, cascadeDelete: true)
                .Index(t => t.muscleId)
                .Index(t => t.exerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "muscleId", "dbo.Muscles");
            DropForeignKey("dbo.Workouts", "exerciseId", "dbo.Exercises");
            DropIndex("dbo.Workouts", new[] { "exerciseId" });
            DropIndex("dbo.Workouts", new[] { "muscleId" });
            DropTable("dbo.Workouts");
        }
    }
}
