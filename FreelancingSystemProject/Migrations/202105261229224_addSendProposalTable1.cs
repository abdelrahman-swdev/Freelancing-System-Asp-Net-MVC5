namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSendProposalTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendProposals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProposalDate = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendProposals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SendProposals", "PostId", "dbo.Posts");
            DropIndex("dbo.SendProposals", new[] { "UserId" });
            DropIndex("dbo.SendProposals", new[] { "PostId" });
            DropTable("dbo.SendProposals");
        }
    }
}
