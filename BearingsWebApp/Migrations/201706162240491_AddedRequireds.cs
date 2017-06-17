namespace BearingsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequireds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeebaInfoes", "itemName", c => c.String(nullable: false));
            AlterColumn("dbo.MeebaInfoes", "category", c => c.String(nullable: false));
            AlterColumn("dbo.MeebaInfoes", "pull", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeebaInfoes", "pull", c => c.String());
            AlterColumn("dbo.MeebaInfoes", "category", c => c.String());
            AlterColumn("dbo.MeebaInfoes", "itemName", c => c.String());
        }
    }
}
