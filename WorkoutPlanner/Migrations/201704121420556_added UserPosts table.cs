namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserPoststable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        time = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosts", "userId", "dbo.UserInfoes");
            DropIndex("dbo.UserPosts", new[] { "userId" });
            DropTable("dbo.UserPosts");
        }
    }
}
