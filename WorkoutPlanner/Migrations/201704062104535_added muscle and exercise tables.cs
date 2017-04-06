namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmuscleandexercisetables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        exerciseId = c.Int(nullable: false, identity: true),
                        exerciseName = c.String(),
                    })
                .PrimaryKey(t => t.exerciseId);
            
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        muscleId = c.Int(nullable: false, identity: true),
                        muscleName = c.String(),
                    })
                .PrimaryKey(t => t.muscleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Muscles");
            DropTable("dbo.Exercises");
        }
    }
}
