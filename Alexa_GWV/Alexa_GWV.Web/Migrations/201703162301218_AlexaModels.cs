namespace Alexa_GWV.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlexaModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlexaUserId = c.String(maxLength: 500),
                        RequestCount = c.Int(nullable: false),
                        LastRequestDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        SessionId = c.String(maxLength: 500),
                        AppId = c.String(maxLength: 500),
                        RequestId = c.String(maxLength: 500),
                        UserId = c.String(maxLength: 500),
                        Timestamp = c.DateTime(nullable: false),
                        Intent = c.String(maxLength: 500),
                        Slots = c.String(maxLength: 50),
                        IsNew = c.Boolean(nullable: false),
                        Version = c.String(maxLength: 5),
                        Type = c.String(maxLength: 50),
                        Reason = c.String(maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "MemberId", "dbo.Members");
            DropIndex("dbo.Requests", new[] { "MemberId" });
            DropTable("dbo.Requests");
            DropTable("dbo.Members");
        }
    }
}
