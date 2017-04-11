namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingtables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workouts", "exerciseId", "dbo.Exercises");
            DropForeignKey("dbo.DayPlanners", "workoutId", "dbo.Workouts");
            DropIndex("dbo.DayPlanners", new[] { "workoutId" });
            DropIndex("dbo.Workouts", new[] { "exerciseId" });
            DropTable("dbo.DayPlanners");
            DropTable("dbo.Workouts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        workoutId = c.Int(nullable: false, identity: true),
                        workoutName = c.String(),
                        exerciseId = c.Int(nullable: false),
                        sets = c.Int(nullable: false),
                        reps = c.Int(nullable: false),
                        userEmail = c.String(),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.workoutId);
            
            CreateTable(
                "dbo.DayPlanners",
                c => new
                    {
                        dayPlannerId = c.Int(nullable: false, identity: true),
                        workoutId = c.Int(nullable: false),
                        startAt = c.DateTime(nullable: false),
                        endAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.dayPlannerId);
            
            CreateIndex("dbo.Workouts", "exerciseId");
            CreateIndex("dbo.DayPlanners", "workoutId");
            AddForeignKey("dbo.DayPlanners", "workoutId", "dbo.Workouts", "workoutId", cascadeDelete: true);
            AddForeignKey("dbo.Workouts", "exerciseId", "dbo.Exercises", "exerciseId", cascadeDelete: true);
        }
    }
}
