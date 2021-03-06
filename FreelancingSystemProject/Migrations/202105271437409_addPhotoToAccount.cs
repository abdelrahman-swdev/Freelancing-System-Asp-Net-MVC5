namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhotoToAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "imageUrl");
        }
    }
}
