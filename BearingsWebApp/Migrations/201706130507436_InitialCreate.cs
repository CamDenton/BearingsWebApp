namespace BearingsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeebaInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        itemName = c.String(),
                        category = c.String(),
                        pull = c.String(),
                        apptInt = c.Int(nullable: false),
                        workInt = c.Int(nullable: false),
                        socInt = c.Int(nullable: false),
                        evtInt = c.Int(nullable: false),
                        persInt = c.Int(nullable: false),
                        otherInt = c.Int(nullable: false),
                        innerInt = c.Int(nullable: false),
                        OuterInt = c.Int(nullable: false),
                        userID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MeebaInfoes");
        }
    }
}
