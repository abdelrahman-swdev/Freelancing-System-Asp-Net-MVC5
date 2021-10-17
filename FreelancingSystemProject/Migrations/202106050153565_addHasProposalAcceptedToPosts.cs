namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHasProposalAcceptedToPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "HasProposalAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "HasProposalAccepted");
        }
    }
}
