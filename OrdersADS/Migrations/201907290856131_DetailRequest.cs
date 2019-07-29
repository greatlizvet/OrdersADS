namespace OrdersADS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Details", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Details", new[] { "Request_Id" });
            CreateTable(
                "dbo.RequestDetails",
                c => new
                    {
                        Request_Id = c.Int(nullable: false),
                        Detail_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.Detail_Id })
                .ForeignKey("dbo.Requests", t => t.Request_Id, cascadeDelete: true)
                .ForeignKey("dbo.Details", t => t.Detail_Id, cascadeDelete: true)
                .Index(t => t.Request_Id)
                .Index(t => t.Detail_Id);
            
            DropColumn("dbo.Details", "Request_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Details", "Request_Id", c => c.Int());
            DropForeignKey("dbo.RequestDetails", "Detail_Id", "dbo.Details");
            DropForeignKey("dbo.RequestDetails", "Request_Id", "dbo.Requests");
            DropIndex("dbo.RequestDetails", new[] { "Detail_Id" });
            DropIndex("dbo.RequestDetails", new[] { "Request_Id" });
            DropTable("dbo.RequestDetails");
            CreateIndex("dbo.Details", "Request_Id");
            AddForeignKey("dbo.Details", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
