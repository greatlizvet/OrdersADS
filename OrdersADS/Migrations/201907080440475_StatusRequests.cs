namespace OrdersADS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusRequests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Details", "Request_Id", c => c.Int());
            CreateIndex("dbo.Details", "Request_Id");
            AddForeignKey("dbo.Details", "Request_Id", "dbo.Requests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Details", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Details", new[] { "Request_Id" });
            DropColumn("dbo.Details", "Request_Id");
        }
    }
}
