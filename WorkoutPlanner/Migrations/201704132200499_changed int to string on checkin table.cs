namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedinttostringoncheckintable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CheckIns", "fourSquareUser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CheckIns", "fourSquareUser", c => c.Int(nullable: false));
        }
    }
}
