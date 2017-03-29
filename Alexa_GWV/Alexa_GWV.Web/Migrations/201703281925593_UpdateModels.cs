namespace Alexa_GWV.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "City", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "City");
        }
    }
}
