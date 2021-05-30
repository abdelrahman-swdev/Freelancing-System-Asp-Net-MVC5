namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessageToProposalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendProposals", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendProposals", "Message");
        }
    }
}
