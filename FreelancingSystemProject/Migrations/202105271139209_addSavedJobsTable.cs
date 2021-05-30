namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSavedJobsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
            DropForeignKey("dbo.SavedJobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SavedJobs", "PostId", "dbo.Posts");
            DropIndex("dbo.SavedJobs", new[] { "UserId" });
            DropIndex("dbo.SavedJobs", new[] { "PostId" });
            DropTable("dbo.SavedJobs");
        }
    }
}
