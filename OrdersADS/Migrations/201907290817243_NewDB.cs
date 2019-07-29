namespace OrdersADS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Orders", newName: "Orderes");
            DropForeignKey("dbo.OrderDetails", "DetailId", "dbo.Details");
            DropForeignKey("dbo.Details", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.Details", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "DetailId" });
            CreateTable(
                "dbo.OrdereDetails",
                c => new
                    {
                        Ordere_Id = c.Int(nullable: false),
                        Detail_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ordere_Id, t.Detail_Id })
                .ForeignKey("dbo.Orderes", t => t.Ordere_Id, cascadeDelete: true)
                .ForeignKey("dbo.Details", t => t.Detail_Id, cascadeDelete: true)
                .Index(t => t.Ordere_Id)
                .Index(t => t.Detail_Id);
            
            DropColumn("dbo.Details", "Order_Id");
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Details", "Order_Id", c => c.Int());
            DropForeignKey("dbo.OrdereDetails", "Detail_Id", "dbo.Details");
            DropForeignKey("dbo.OrdereDetails", "Ordere_Id", "dbo.Orderes");
            DropIndex("dbo.OrdereDetails", new[] { "Detail_Id" });
            DropIndex("dbo.OrdereDetails", new[] { "Ordere_Id" });
            DropTable("dbo.OrdereDetails");
            CreateIndex("dbo.OrderDetails", "DetailId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.Details", "Order_Id");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Details", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.OrderDetails", "DetailId", "dbo.Details", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Orderes", newName: "Orders");
        }
    }
}
