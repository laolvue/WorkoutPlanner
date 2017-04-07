namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeginkeytoexercisetableremovedforeignkeyfromworkouttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workouts", "muscleId", "dbo.Muscles");
            DropIndex("dbo.Workouts", new[] { "muscleId" });
            AddColumn("dbo.Exercises", "muscleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exercises", "muscleId");
            AddForeignKey("dbo.Exercises", "muscleId", "dbo.Muscles", "muscleId", cascadeDelete: true);
            DropColumn("dbo.Workouts", "muscleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workouts", "muscleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Exercises", "muscleId", "dbo.Muscles");
            DropIndex("dbo.Exercises", new[] { "muscleId" });
            DropColumn("dbo.Exercises", "muscleId");
            CreateIndex("dbo.Workouts", "muscleId");
            AddForeignKey("dbo.Workouts", "muscleId", "dbo.Muscles", "muscleId", cascadeDelete: true);
        }
    }
}
