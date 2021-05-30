namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsApprovedByAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsApprovedByAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsApprovedByAdmin");
        }
    }
}
