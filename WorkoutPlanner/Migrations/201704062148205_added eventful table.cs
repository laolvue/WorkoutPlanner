namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedeventfultable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eventfuls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartAt = c.DateTime(nullable: false),
                        EndAt = c.DateTime(nullable: false),
                        IsFullDay = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Eventfuls");
        }
    }
}
