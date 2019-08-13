namespace OrdersADS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pgsql : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orderes", "dateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.Orderes", "dateTime", c => c.DateTime(nullable: false));
        }
    }
}
