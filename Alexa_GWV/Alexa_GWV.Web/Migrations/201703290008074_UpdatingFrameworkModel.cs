namespace Alexa_GWV.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingFrameworkModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Courses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Author = c.String(maxLength: 50),
                        Content = c.String(storeType: "ntext"),
                        Votes = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
