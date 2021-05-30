namespace FreelancingSystemProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPoostsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobBudget = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        JobDescription = c.String(nullable: false),
                        JobTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobTypes", t => t.JobTypeId, cascadeDelete: true)
                .Index(t => t.JobTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "JobTypeId", "dbo.JobTypes");
            DropIndex("dbo.Posts", new[] { "JobTypeId" });
            DropTable("dbo.Posts");
        }
    }
}
