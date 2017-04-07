namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedprofilepicturetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfilePictures",
                c => new
                    {
                        profilePictureId = c.Int(nullable: false, identity: true),
                        profileImage = c.Binary(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.profilePictureId)
                .ForeignKey("dbo.UserInfoes", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfilePictures", "userId", "dbo.UserInfoes");
            DropIndex("dbo.ProfilePictures", new[] { "userId" });
            DropTable("dbo.ProfilePictures");
        }
    }
}
