namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcheckintable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckIns",
                c => new
                    {
                        checkInId = c.Int(nullable: false, identity: true),
                        fourSquareUser = c.Int(nullable: false),
                        checkInPlace = c.String(),
                        checkInAddress = c.String(),
                        checkInTime = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.checkInId)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckIns", "userId", "dbo.UserInfoes");
            DropIndex("dbo.CheckIns", new[] { "userId" });
            DropTable("dbo.CheckIns");
        }
    }
}
