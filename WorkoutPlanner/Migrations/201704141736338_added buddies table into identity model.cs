namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbuddiestableintoidentitymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buddies",
                c => new
                    {
                        buddyId = c.Int(nullable: false, identity: true),
                        buddyEmail = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.buddyId)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buddies", "userId", "dbo.UserInfoes");
            DropIndex("dbo.Buddies", new[] { "userId" });
            DropTable("dbo.Buddies");
        }
    }
}
