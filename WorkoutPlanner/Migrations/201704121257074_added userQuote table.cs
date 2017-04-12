namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserQuotetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        quote = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuotes", "userId", "dbo.UserInfoes");
            DropIndex("dbo.UserQuotes", new[] { "userId" });
            DropTable("dbo.UserQuotes");
        }
    }
}
