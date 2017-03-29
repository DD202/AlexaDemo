namespace Alexa_GWV.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillsFramework : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InformationalFacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fact = c.String(maxLength: 500),
                        Votes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 500),
                        SmallImageUrl = c.String(maxLength: 500),
                        LargeImageUrl = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pictures");
            DropTable("dbo.InformationalFacts");
        }
    }
}
