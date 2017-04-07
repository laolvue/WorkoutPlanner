namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddayplannertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayPlanners",
                c => new
                    {
                        dayPlannerId = c.Int(nullable: false, identity: true),
                        workoutId = c.Int(nullable: false),
                        startAt = c.DateTime(nullable: false),
                        endAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.dayPlannerId)
                .ForeignKey("dbo.Workouts", t => t.workoutId, cascadeDelete: true)
                .Index(t => t.workoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayPlanners", "workoutId", "dbo.Workouts");
            DropIndex("dbo.DayPlanners", new[] { "workoutId" });
            DropTable("dbo.DayPlanners");
        }
    }
}
