namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makePhotoRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "imageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "imageUrl", c => c.String());
        }
    }
}
